using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconBeta.Data;
using ReconBeta.Models;

namespace ReconBeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  [Authorize]
  public class ApiEmergency_NumbersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiEmergency_NumbersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiEmergency_Numbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emergency_Numbers>>> GetEmergency_Numbers()
        {
            return await _context.Emergency_Numbers.ToListAsync();
        }

        // GET: api/ApiEmergency_Numbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emergency_Numbers>> GetEmergency_Numbers(int id)
        {
            var emergency_Numbers = await _context.Emergency_Numbers.FindAsync(id);

            if (emergency_Numbers == null)
            {
                return NotFound();
            }

            return emergency_Numbers;
        }

        // PUT: api/ApiEmergency_Numbers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmergency_Numbers(int id, Emergency_Numbers emergency_Numbers)
        {
            if (id != emergency_Numbers.Id)
            {
                return BadRequest();
            }

            _context.Entry(emergency_Numbers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Emergency_NumbersExists(id))
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

        // POST: api/ApiEmergency_Numbers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Emergency_Numbers>> PostEmergency_Numbers(Emergency_Numbers emergency_Numbers)
        {
            _context.Emergency_Numbers.Add(emergency_Numbers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmergency_Numbers", new { id = emergency_Numbers.Id }, emergency_Numbers);
        }

        // DELETE: api/ApiEmergency_Numbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmergency_Numbers(int id)
        {
            var emergency_Numbers = await _context.Emergency_Numbers.FindAsync(id);
            if (emergency_Numbers == null)
            {
                return NotFound();
            }

            _context.Emergency_Numbers.Remove(emergency_Numbers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Emergency_NumbersExists(int id)
        {
            return _context.Emergency_Numbers.Any(e => e.Id == id);
        }
    }
}
