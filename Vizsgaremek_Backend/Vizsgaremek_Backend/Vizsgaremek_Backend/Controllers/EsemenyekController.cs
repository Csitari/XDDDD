using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EsemenyekController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public EsemenyekController(EsemenytarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Esemenyek>> GetEsemenyek()
        {
            return _context.Esemenyeks.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Esemenyek> GetEsemeny(Guid id)
        {
            var esemeny = _context.Esemenyeks.Find(id);

            if (esemeny == null)
            {
                return NotFound();
            }

            return esemeny;
        }

        [HttpPost]
        public ActionResult<Esemenyek> CreateEsemeny(Esemenyek esemeny)
        {
            _context.Esemenyeks.Add(esemeny);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEsemeny), new { id = esemeny.EsemenyId }, esemeny);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEsemeny(Guid id, Esemenyek updatedEsemeny)
        {
            var esemeny = _context.Esemenyeks.Find(id);

            if (esemeny == null)
            {
                return NotFound();
            }

            esemeny.Cim = updatedEsemeny.Cim;
            esemeny.Leiras = updatedEsemeny.Leiras;
            //esemenyek.stb
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEsemeny(Guid id)
        {
            var esemeny = _context.Esemenyeks.Find(id);

            if (esemeny == null)
            {
                return NotFound();
            }

            _context.Esemenyeks.Remove(esemeny);
            _context.SaveChanges();

            return NoContent();
        }
    }
}