using System;
using SecretSantaBackend.Data;
using SecretSantaBackend.Models;
using SecretSantaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SecretSantaBackend.Models.Responses;


namespace SecretSantaBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SecretSantaController : ControllerBase
    {
        private readonly ISecretSantaService _service;
        private readonly AppDbContext _context;

        public SecretSantaController(ISecretSantaService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        // Generisanje liste

        [Authorize(Roles = "Administrator")]
        [HttpPost("generate")]
        public async Task<ActionResult<SecretSantaList>> Generate()
        {
            var result = await _service.GeneratePairsAsync();
            return Ok(result);
        }

        // Sve liste za admina

        [HttpGet("history")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetListsHistory() 
        {
            try
            {
                var lists = await _context.SecretSantaLists
                    .Include(l => l.Pairs)
                        .ThenInclude(p => p.Giver)
                    .Include(l => l.Pairs)
                        .ThenInclude(p => p.Receiver)
                    .Include(l => l.UnpairedEmployee)
                    .OrderByDescending(l => l.CreatedDate)
                    .ToListAsync();

                if (lists == null || !lists.Any())
                {
                    return Ok(new List<SecretSantaListHistory>());
                }

                var history = lists.Select(l => new SecretSantaListHistory
                {
                    ListId = l.Id,
                    CreatedDate = l.CreatedDate, 

                    UnpairedEmployee = l.UnpairedEmployee != null ? new EmployeeHistory
                    {
                        Name = l.UnpairedEmployee.Name ?? "N/A",
                        Surname = l.UnpairedEmployee.Surname ?? "N/A",
                        Email = l.UnpairedEmployee.Email ?? "N/A",
                    } : null,

                    Pairs = l.Pairs.Select(p => new PairHistory
                    {
                        GiverName = p.Giver?.Name ?? "N/A",
                        GiverSurname = p.Giver?.Surname ?? "N/A",
                        GiverEmail = p.Giver?.Email ?? "N/A",

                        ReceiverName = p.Receiver?.Name ?? "N/A",
                        ReceiverSurname = p.Receiver?.Surname ?? "N/A",
                        ReceiverEmail = p.Receiver?.Email ?? "N/A",

                        DateGenerated = l.CreatedDate 
                    }).ToList()

                }).ToList();

                return Ok(history); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Interna serverska greška prilikom dohvata historije.");
            }
        }
    }
}

