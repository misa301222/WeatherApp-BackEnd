using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class TemperatureNotification
    {
        public TemperatureNotification(int temperatureNotificationId, int cityId, DateTime dateTemperature, int notificationTypeId, string temperatureNotificationHeader, string temperatureNotificationDescription)
        {
            TemperatureNotificationId = temperatureNotificationId;
            CityId = cityId;
            DateTemperature = dateTemperature;
            NotificationTypeId = notificationTypeId;
            TemperatureNotificationHeader = temperatureNotificationHeader;
            TemperatureNotificationDescription = temperatureNotificationDescription;
        }

        [Key]
        public int TemperatureNotificationId { get; set; }
        public int CityId { get; set; }
        public DateTime DateTemperature { get; set; }
        public int NotificationTypeId { get; set; }
        public string TemperatureNotificationHeader { get; set; }
        public string TemperatureNotificationDescription { get; set; }
    }
}
