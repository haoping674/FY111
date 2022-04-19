﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FY111.Models.FY111;

namespace FY111.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaverseController : ControllerBase
    {
        private readonly FY111Context _context;

        public MetaverseController(FY111Context context)
        {
            _context = context;
        }

        // GET: api/Metaverses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Metaverse>>> GetMetaverses()
        {
            return await _context.Metaverses.ToListAsync();
        }

        // GET: api/Metaverses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Metaverse>> GetMetaverse(string id)
        {
            var metaverse = await _context.Metaverses.FindAsync(id);

            if (metaverse == null)
            {
                return NotFound();
            }

            return metaverse;
        }

        // PUT: api/Metaverses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetaverse(string id, Metaverse metaverse)
        {
            if (id != metaverse.Name)
            {
                return BadRequest();
            }

            _context.Entry(metaverse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaverseExists(id))
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

        // POST: api/Metaverses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Metaverse>> PostMetaverse(Metaverse metaverse)
        {
            _context.Metaverses.Add(metaverse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MetaverseExists(metaverse.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMetaverse", new { id = metaverse.Name }, metaverse);
        }

        // DELETE: api/Metaverses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Metaverse>> DeleteMetaverse(string id)
        {
            var metaverse = await _context.Metaverses.FindAsync(id);
            if (metaverse == null)
            {
                return NotFound();
            }

            _context.Metaverses.Remove(metaverse);
            await _context.SaveChangesAsync();

            return metaverse;
        }

        [HttpGet("list_available")]
        public async Task<ActionResult<IEnumerable<Metaverse>>> ListAvailable()
        {
            var metaverse = await _context.Metaverses
                            .Where(b => b.Ip != null && b.Ip != "")
                            .ToListAsync();

            if (metaverse == null)
            {
                return NotFound();
            }

            return metaverse;
        }

        private bool MetaverseExists(string id)
        {
            return _context.Metaverses.Any(e => e.Name == id);
        }
    }
}