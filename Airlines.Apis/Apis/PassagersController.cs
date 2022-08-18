﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirLines.Infrastructure.Data;
using AirLines.Core.Models;

namespace Airlines.Apis.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassagersController : ControllerBase
    {
        private readonly AirlinesContext _context;

        public PassagersController(AirlinesContext context)
        {
            _context = context;
        }

        // GET: api/Passagers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passager>>> GetPassagers()
        {
          if (_context.Passagers == null)
          {
              return NotFound();
          }
            return await _context.Passagers.ToListAsync();
        }

        // GET: api/Passagers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passager>> GetPassager(int id)
        {
          if (_context.Passagers == null)
          {
              return NotFound();
          }
            var passager = await _context.Passagers.FindAsync(id);

            if (passager == null)
            {
                return NotFound();
            }

            return passager;
        }

        // PUT: api/Passagers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassager(int id, Passager passager)
        {
            if (id != passager.Id)
            {
                return BadRequest();
            }

            _context.Entry(passager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassagerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Passagers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passager>> PostPassager(Passager passager)
        {
          if (_context.Passagers == null)
          {
              return Problem("Entity set 'AirlinesContext.Passagers'  is null.");
          }
            _context.Passagers.Add(passager);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassager", new { id = passager.Id }, passager);
        }

        // DELETE: api/Passagers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassager(int id)
        {
            if (_context.Passagers == null)
            {
                return NotFound();
            }
            var passager = await _context.Passagers.FindAsync(id);
            if (passager == null)
            {
                return NotFound();
            }

            _context.Passagers.Remove(passager);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassagerExists(int id)
        {
            return (_context.Passagers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}