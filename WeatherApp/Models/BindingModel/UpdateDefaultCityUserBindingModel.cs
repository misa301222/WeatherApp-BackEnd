using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.BindingModel
{
    public class UpdateDefaultCityUserBindingModel
    {
        public string Email { get; set; }
        public int CityId { get; set; }
    }
}
