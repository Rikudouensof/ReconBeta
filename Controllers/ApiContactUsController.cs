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
  public class ApiContactUsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiContactUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiContactUs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactUs>>> GetContactUs()
        {
            return await _context.ContactUs.ToListAsync();
        }

        // GET: api/ApiContactUs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactUs>> GetContactUs(int id)
        {
            var contactUs = await _context.ContactUs.FindAsync(id);

            if (contactUs == null)
            {
                return NotFound();
            }

            return contactUs;
        }

        // PUT: api/ApiContactUs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactUs(int id, ContactUs contactUs)
        {
            if (id != contactUs.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactUs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactUsExists(id))
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

        // POST: api/ApiContactUs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactUs>> PostContactUs(ContactUs contactUs)
        {
            _context.ContactUs.Add(contactUs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactUs", new { id = contactUs.Id }, contactUs);
        }

        // DELETE: api/ApiContactUs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactUs(int id)
        {
            var contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs == null)
            {
                return NotFound();
            }

            _context.ContactUs.Remove(contactUs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactUsExists(int id)
        {
            return _context.ContactUs.Any(e => e.Id == id);
        }
    }
}
