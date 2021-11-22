using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class ApplicationStatus
    {
        public ApplicationStatus(int applicationStatusId, string applicationStatusDescription)
        {
            ApplicationStatusId = applicationStatusId;
            ApplicationStatusDescription = applicationStatusDescription;
        }

        [Key]
        public int ApplicationStatusId { get; set; }
        public string ApplicationStatusDescription { get; set; }
    }
}
