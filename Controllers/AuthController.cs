using SecretSantaBackend.Models;
using SecretSantaBackend.Models.Requests;
using SecretSantaBackend.Services;
using SecretSantaBackend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    private readonly AppDbContext _context;

    public AuthController(UserManager<User> userManager, IJwtService jwtService, AppDbContext context)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized();
        }

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = _jwtService.GenerateToken(claims);

        return Ok(new { Token = token, UserRole = roles.FirstOrDefault() });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        string role = request.Role ?? "Uposlenik";

        if (role == "Uposlenik")
        {
            var newEmployee = new Employee
            {
                Surname = user.LastName,
                Name = user.FirstName,
                Email = request.Email,
                UserId = user.Id 
            };

            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync(); 

            user.EmployeeId = newEmployee.Id;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return StatusCode(500, $"Korisnik je kreiran, ali nije uspjelo povezivanje s Employee tabelom: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
            }
        }

        await _userManager.AddToRoleAsync(user, role);

        return Ok();
    }
}