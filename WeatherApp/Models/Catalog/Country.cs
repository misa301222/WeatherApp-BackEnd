using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class Country
    {
        public Country(int countryId, string countryName)
        {
            CountryId = countryId;
            CountryName = countryName;
        }
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
