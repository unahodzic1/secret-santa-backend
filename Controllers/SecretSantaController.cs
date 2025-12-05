using System;
using SecretSantaBackend.Data;
using SecretSantaBackend.Models;
using SecretSantaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace SecretSantaBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SecretSantaController : ControllerBase
    {
        private readonly SecretSantaService _service;

        public SecretSantaController(SecretSantaService service)
        {
            _service = service;
        }

        [HttpGet("generate")]
        public ActionResult<SecretSantaList> Generate()
        {
            var result = _service.GeneratePairsAsync();
            return Ok(result);
        }
    }
}

