using System;
using SecretSantaBackend.Data;
using SecretSantaBackend.Models;
using SecretSantaBackend.Models.Responses;
using SecretSantaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace SecretSantaBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // CRUD operacije 

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees(){
            return await _context.Employees.ToListAsync();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee){
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployees), new { id = employee.Id }, employee);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id){
            var employee = await _context.Employees.FindAsync(id);

            if(employee == null){
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // kome kupujem poklon

        [HttpGet("myPair")]
        [Authorize(Roles = "Uposlenik")]
        public async Task<ActionResult<MyPairResponse>> GetMySecretSantaPair()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Unauthorized();
            }

            if (!user.EmployeeId.HasValue)
            {
                return StatusCode(403, "Korisniku nedostaje EmployeeId.");
            }

            int employeeGiverId = user.EmployeeId.Value;

            var latestPair = await _context.Pairs
                .Include(p => p.Receiver)
                .Where(p => p.GiverId == employeeGiverId)
                .OrderByDescending(p => p.ListId)
                .FirstOrDefaultAsync();

            if (latestPair?.Receiver == null)
            {
                return NotFound("Secret Santa lista još nije generisana.");
            }

            var response = new MyPairResponse
            {
                ReceiverName = latestPair.Receiver.Name,
                ReceiverSurname = latestPair.Receiver.Surname,
                ReceiverEmail = latestPair.Receiver.Email
            };

            return Ok(response);
        }

    }
}

