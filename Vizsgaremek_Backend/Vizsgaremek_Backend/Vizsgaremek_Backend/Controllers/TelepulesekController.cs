﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelepulesekController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public TelepulesekController(EsemenytarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Telepulesek>> GetAll()
        {
            return _context.Telepuleseks.ToList();
        }
        [HttpGet("{id}"), Authorize("Admin")]
        public ActionResult<Telepulesek> GetById(int id)
        {
            var item = _context.Telepuleseks.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        [HttpPost, Authorize("Admin")]
        public IActionResult Create(Telepulesek item)
        {
            _context.Telepuleseks.Add(item);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = item.TelepulesId }, item);
        }

        [HttpPut("{id}"), Authorize("Admin")]
        public IActionResult Update(int id, Telepulesek item)
        {
            if (id != item.TelepulesId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}"), Authorize("Admin")]
        public IActionResult Delete(int id)
        {
            var item = _context.Telepuleseks.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Telepuleseks.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
