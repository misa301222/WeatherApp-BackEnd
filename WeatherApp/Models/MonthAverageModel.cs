using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class MonthAverageModel
    {
        public MonthAverageModel(string month, double averageMax, double averageMin, double averagePrecipitation, double averageWind)
        {
            this.month = month;
            this.averageMax = averageMax;
            this.averageMin = averageMin;
            this.averagePrecipitation = averagePrecipitation;
            this.averageWind = averageWind;
        }

        public string month { get; set; }
        public double averageMax { get; set; }
        public double averageMin { get; set; }
        public double averagePrecipitation { get; set; }
        public double averageWind { get; set; }
    }
}
