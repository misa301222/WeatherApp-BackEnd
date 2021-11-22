using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApp.EntityFrameworkCore;
using WeatherApp.Models.Catalog;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesiredPositionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DesiredPositionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DesiredPositions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DesiredPosition>>> GetDesiredPosition()
        {
            return await _context.DesiredPosition.ToListAsync();
        }

        // GET: api/DesiredPositions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DesiredPosition>> GetDesiredPosition(int id)
        {
            var desiredPosition = await _context.DesiredPosition.FindAsync(id);

            if (desiredPosition == null)
            {
                return NotFound();
            }

            return desiredPosition;
        }

        // PUT: api/DesiredPositions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesiredPosition(int id, DesiredPosition desiredPosition)
        {
            if (id != desiredPosition.DesiredPositionId)
            {
                return BadRequest();
            }

            _context.Entry(desiredPosition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesiredPositionExists(id))
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

        // POST: api/DesiredPositions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DesiredPosition>> PostDesiredPosition(DesiredPosition desiredPosition)
        {
            _context.DesiredPosition.Add(desiredPosition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDesiredPosition", new { id = desiredPosition.DesiredPositionId }, desiredPosition);
        }

        // DELETE: api/DesiredPositions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesiredPosition(int id)
        {
            var desiredPosition = await _context.DesiredPosition.FindAsync(id);
            if (desiredPosition == null)
            {
                return NotFound();
            }

            _context.DesiredPosition.Remove(desiredPosition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DesiredPositionExists(int id)
        {
            return _context.DesiredPosition.Any(e => e.DesiredPositionId == id);
        }
    }
}
