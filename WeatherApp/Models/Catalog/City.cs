using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class City
    {
        public City(int cityId, string cityName, int countryId)
        {
            CityId = cityId;
            CityName = cityName;
            CountryId = countryId;
        }

        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
    }
}
