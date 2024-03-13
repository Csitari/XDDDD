using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EsemenyInterakcioController : ControllerBase
    {
        private readonly EsemenytarContext _dbContext;

        public EsemenyInterakcioController(EsemenytarContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/EsemenyInterakcio
        [HttpGet]
        public IActionResult GetEsemenyInterakciok()
        {
            var esemenyInterakciok = _dbContext.EsemenyInterakcios.ToList();
            return Ok(esemenyInterakciok);
        }

        // POST: api/EsemenyInterakcio
        [HttpPost]
        public IActionResult CreateEsemenyInterakcio(EsemenyInterakcio esemenyInterakcio)
        {
            if (ModelState.IsValid)
            {
                esemenyInterakcio.JelentkezesDatum = DateTime.Now;
                _dbContext.EsemenyInterakcios.Add(esemenyInterakcio);
                _dbContext.SaveChanges();
                return CreatedAtAction(nameof(GetEsemenyInterakciok), new { id = esemenyInterakcio.InterakcioId }, esemenyInterakcio);
            }
            return BadRequest(ModelState);
        }
    }
}

