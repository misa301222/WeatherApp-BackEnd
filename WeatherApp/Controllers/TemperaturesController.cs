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
    public class TemperaturesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TemperaturesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Temperatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Temperature>>> GetTemperature()
        {
            return await _context.Temperature.ToListAsync();
        }

        // GET: api/Temperatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Temperature>> GetTemperature(int id)
        {
            var temperature = await _context.Temperature.FindAsync(id);

            if (temperature == null)
            {
                return NotFound();
            }

            return temperature;
        }

        [HttpGet("GetTemperatureByCityId/{cityId}")]
        public async Task<ActionResult<IEnumerable<Temperature>>> GetTemperatureByCityId(int cityId)
        {
            return await _context.Temperature.Where(x => x.CityId == cityId).ToListAsync();
        }

        [HttpGet("GetAllTemperaturesFromDateToEnd/{cityId}/{dateTemperature}")]
        public async Task<ActionResult<IEnumerable<Temperature>>> GetAllTemperaturesFromDateToEnd(int cityId, DateTime dateTemperature)
        {
            return await _context.Temperature.Where(x => x.CityId == cityId && x.DateTemperature >= dateTemperature).ToListAsync();
        }


        [HttpGet("GetTemperatureByCityIdAndDateTemperature/{cityId}/{dateTemperature}")]
        public async Task<ActionResult<Temperature>> GetTemperatureByCityIdAndDateTemperature(int cityId, DateTime dateTemperature)
        {
            var temperature = await _context.Temperature.Where(x => x.CityId == cityId && x.DateTemperature == dateTemperature).SingleAsync();

            if (temperature == null)
            {
                return NotFound();
            }

            return temperature;
        }


        [HttpGet("GetTemperatureByCityIdAndDateTemperatureNextFive/{cityId}/{dateTemperature}")]
        public async Task<ActionResult<IEnumerable<Temperature>>> GetTemperatureByCityIdAndDateTemperatureNextFive(int cityId, DateTime dateTemperature)
        {
            var temperature = await _context.Temperature.Where(x => x.CityId == cityId && x.DateTemperature == dateTemperature).SingleAsync();

            var dateTemperatureAux = dateTemperature;
            var nextFiveTemperatures = new List<Temperature>();
            for (int i = 1; i < 6; i++)
            {
                //dateTemperatureAux.AddDays(i);
                try
                {
                    var result = await _context.Temperature.Where(x => x.CityId == cityId && x.DateTemperature == dateTemperatureAux.AddDays(i)).SingleAsync();
                    if (result != null)
                    {
                        nextFiveTemperatures.Add(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (temperature == null)
            {
                return NotFound();
            }

            return nextFiveTemperatures;
        }


        [HttpGet("GetTemperatureByCityIdAndDateTemperatureNextFourAndToday/{cityId}/{dateTemperature}")]
        public async Task<ActionResult<IEnumerable<Temperature>>> GetTemperatureByCityIdAndDateTemperatureNextFourAndToday(int cityId, DateTime dateTemperature)
        {
            var temperature = await _context.Temperature.Where(x => x.CityId == cityId && x.DateTemperature == dateTemperature).SingleAsync();
            var dateTemperatureAux = dateTemperature;
            var nextFiveTemperatures = new List<Temperature>();

            nextFiveTemperatures.Add(temperature);
            for (int i = 1; i < 5; i++)
            {
                try
                {
                    var result = await _context.Temperature.Where(x => x.CityId == cityId && x.DateTemperature == dateTemperatureAux.AddDays(i)).SingleAsync();
                    if (result != null)
                    {
                        nextFiveTemperatures.Add(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (temperature == null)
            {
                return NotFound();
            }

            return nextFiveTemperatures;
        }



        // PUT: api/Temperatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemperature(int id, Temperature temperature)
        {
            if (id != temperature.CityId)
            {
                return BadRequest();
            }

            _context.Entry(temperature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureExists(id))
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

        // POST: api/Temperatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Temperature>> PostTemperature(Temperature temperature)
        {
            _context.Temperature.Add(temperature);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TemperatureExists(temperature.CityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTemperature", new { id = temperature.CityId }, temperature);
        }

        // DELETE: api/Temperatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemperature(int id)
        {
            var temperature = await _context.Temperature.FindAsync(id);
            if (temperature == null)
            {
                return NotFound();
            }

            _context.Temperature.Remove(temperature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteAllTemperaturesByCityId/{cityId}")]
        public async Task<IActionResult> DeleteAllTemperaturesByCityId(int cityId)
        {
            var temperatures = await _context.Temperature.Where(x => x.CityId == cityId).ToListAsync();
            foreach (var temperature in temperatures)
            {
                _context.Temperature.Remove(temperature);
                await _context.SaveChangesAsync();
            }


            return NoContent();
        }

        private bool TemperatureExists(int id)
        {
            return _context.Temperature.Any(e => e.CityId == id);
        }
    }
}
