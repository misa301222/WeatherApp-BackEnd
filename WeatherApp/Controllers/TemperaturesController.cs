using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApp.EntityFrameworkCore;
using WeatherApp.Models;
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

        [HttpGet("GetTemperatureFromDateToEndLimitNinety/{cityId}/{dateTemperature}")]
        public async Task<ActionResult<IEnumerable<Temperature>>> GetTemperatureFromDateToEndLimitNinety(int cityId, DateTime dateTemperature)
        {
            var temperatures = await _context.Temperature.Where(x => x.CityId == cityId && x.DateTemperature >= dateTemperature).ToListAsync();

            if (temperatures == null)
            {
                return NoContent();
            }

            List<Temperature> temperaturesLimited = temperatures.Take(90).ToList();            

            return temperaturesLimited;
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

        [HttpGet("GetTemperatureMonthAverageYearByCityIdAndYear/{cityId}/{year}")]
        public async Task<object> GetTemperatureMonthAverageYearByCityIdAndYear(int cityId, int year)
        {
            List<double> averageByMonth = new List<double>();
            List<MonthAverageModel> averageMonth = new List<MonthAverageModel>();
            for (int i = 0; i < 12; i++)
            {
                var maxTemperatureSum = 0.0;
                var minTemperatureSum = 0.0;
                var windSum = 0.0;
                var precipitationSum = 0.0;

                var listOfMonth = await _context.Temperature.Where(x => x.DateTemperature.Month == (i + 1) && x.CityId == cityId && x.DateTemperature.Year == year).ToListAsync();
                foreach (var month in listOfMonth)
                {
                    maxTemperatureSum += month.MaxTemperature;
                    minTemperatureSum += month.MinTemperature;
                    windSum += month.WindTemperature;
                    precipitationSum += month.PrecipitationTemperature;
                }
                maxTemperatureSum /= (DateTime.DaysInMonth(year, (i + 1)));
                maxTemperatureSum = Math.Round(maxTemperatureSum, 2);

                minTemperatureSum /= (DateTime.DaysInMonth(year, (i + 1)));
                minTemperatureSum = Math.Round(minTemperatureSum, 2);

                precipitationSum /= (DateTime.DaysInMonth(year, (i + 1)));
                precipitationSum = Math.Round(precipitationSum, 2);

                windSum /= (DateTime.DaysInMonth(year, (i + 1)));
                windSum = Math.Round(windSum, 2);

                averageMonth.Add(new MonthAverageModel(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(i + 1), maxTemperatureSum, minTemperatureSum, precipitationSum, windSum));
                //averageByMonth.Add(sum);
            }

            return averageMonth;
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
