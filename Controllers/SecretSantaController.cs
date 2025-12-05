using System;
using SecretSantaBackend.Data;
using SecretSantaBackend.Models;
using SecretSantaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; 


namespace SecretSantaBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SecretSantaController : ControllerBase
    {
        private readonly ISecretSantaService _service;

        public SecretSantaController(ISecretSantaService service)
        {
            _service = service;
        }

        // Generisanje liste

        [Authorize(Roles = "Administrator")]
        [HttpPost("generate")]
        public async Task<ActionResult<SecretSantaList>> Generate()
        {
            var result = await _service.GeneratePairsAsync();
            return Ok(result);
        }
    }
}

