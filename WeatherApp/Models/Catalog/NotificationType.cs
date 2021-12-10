using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class NotificationType
    {
        public NotificationType(int notificationTypeId, string notificationTypeDescription)
        {
            NotificationTypeId = notificationTypeId;
            NotificationTypeDescription = notificationTypeDescription;
        }

        [Key]
        public int NotificationTypeId { get; set; }
        public string NotificationTypeDescription { get; set; }
    }
}
