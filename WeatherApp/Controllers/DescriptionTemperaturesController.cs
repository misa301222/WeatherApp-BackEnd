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
    public class DescriptionTemperaturesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DescriptionTemperaturesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DescriptionTemperatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DescriptionTemperature>>> GetDescriptionTemperature()
        {
            return await _context.DescriptionTemperature.ToListAsync();
        }

        // GET: api/DescriptionTemperatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DescriptionTemperature>> GetDescriptionTemperature(int id)
        {
            var descriptionTemperature = await _context.DescriptionTemperature.FindAsync(id);

            if (descriptionTemperature == null)
            {
                return NotFound();
            }

            return descriptionTemperature;
        }

        // PUT: api/DescriptionTemperatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDescriptionTemperature(int id, DescriptionTemperature descriptionTemperature)
        {
            if (id != descriptionTemperature.DescriptionTemperatureId)
            {
                return BadRequest();
            }

            _context.Entry(descriptionTemperature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DescriptionTemperatureExists(id))
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

        // POST: api/DescriptionTemperatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DescriptionTemperature>> PostDescriptionTemperature(DescriptionTemperature descriptionTemperature)
        {
            _context.DescriptionTemperature.Add(descriptionTemperature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDescriptionTemperature", new { id = descriptionTemperature.DescriptionTemperatureId }, descriptionTemperature);
        }

        // DELETE: api/DescriptionTemperatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDescriptionTemperature(int id)
        {
            var descriptionTemperature = await _context.DescriptionTemperature.FindAsync(id);
            if (descriptionTemperature == null)
            {
                return NotFound();
            }

            _context.DescriptionTemperature.Remove(descriptionTemperature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DescriptionTemperatureExists(int id)
        {
            return _context.DescriptionTemperature.Any(e => e.DescriptionTemperatureId == id);
        }
    }
}
