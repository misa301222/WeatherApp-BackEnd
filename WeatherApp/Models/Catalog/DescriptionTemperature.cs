using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class DescriptionTemperature
    {
        public DescriptionTemperature(int descriptionTemperatureId, string descriptionTemperatureDescription)
        {
            DescriptionTemperatureId = descriptionTemperatureId;
            DescriptionTemperatureDescription = descriptionTemperatureDescription;
        }

        [Key]
        public int DescriptionTemperatureId { get; set; }
        public string DescriptionTemperatureDescription { get; set; }
    }
}
