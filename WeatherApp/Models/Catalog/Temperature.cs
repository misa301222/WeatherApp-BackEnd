using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class Temperature
    {
        public Temperature(int cityId, DateTime dateTemperature, int minTemperature, int maxTemperature, int descriptionTemperature, int windTemperature, int precipitationTemperature)
        {
            CityId = cityId;
            DateTemperature = dateTemperature;
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
            DescriptionTemperature = descriptionTemperature;
            WindTemperature = windTemperature;
            PrecipitationTemperature = precipitationTemperature;
        }

        [Key]
        public int CityId { get; set; }
        [Key]
        [Column(TypeName = "Date")]
        public DateTime DateTemperature { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
        public int DescriptionTemperature { get; set; }
        public int WindTemperature { get; set; }
        public int PrecipitationTemperature { get; set; }

    }

    
}
