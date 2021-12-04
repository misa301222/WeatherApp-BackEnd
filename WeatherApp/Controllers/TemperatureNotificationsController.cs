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
    public class TemperatureNotificationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TemperatureNotificationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TemperatureNotifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemperatureNotification>>> GetTemperatureNotification()
        {
            return await _context.TemperatureNotification.ToListAsync();
        }

        [HttpGet("GetTemperatureNotificationsByCityIdAndDateTemperature/{cityId}/{dateTemperature}")]
        public async Task<ActionResult<IEnumerable<TemperatureNotification>>> GetTemperatureNotificationsByCityIdAndDateTemperature(int cityId, DateTime dateTemperature)
        {
            return await _context.TemperatureNotification.Where(x => x.CityId == cityId && x.DateTemperature == dateTemperature).ToListAsync();
        }

        // GET: api/TemperatureNotifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemperatureNotification>> GetTemperatureNotification(int id)
        {
            var temperatureNotification = await _context.TemperatureNotification.FindAsync(id);

            if (temperatureNotification == null)
            {
                return NotFound();
            }

            return temperatureNotification;
        }

        // PUT: api/TemperatureNotifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemperatureNotification(int id, TemperatureNotification temperatureNotification)
        {
            if (id != temperatureNotification.TemperatureNotificationId)
            {
                return BadRequest();
            }

            _context.Entry(temperatureNotification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureNotificationExists(id))
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

        // POST: api/TemperatureNotifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TemperatureNotification>> PostTemperatureNotification(TemperatureNotification temperatureNotification)
        {
            _context.TemperatureNotification.Add(temperatureNotification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemperatureNotification", new { id = temperatureNotification.TemperatureNotificationId }, temperatureNotification);
        }

        // DELETE: api/TemperatureNotifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemperatureNotification(int id)
        {
            var temperatureNotification = await _context.TemperatureNotification.FindAsync(id);
            if (temperatureNotification == null)
            {
                return NotFound();
            }

            _context.TemperatureNotification.Remove(temperatureNotification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TemperatureNotificationExists(int id)
        {
            return _context.TemperatureNotification.Any(e => e.TemperatureNotificationId == id);
        }
    }
}
