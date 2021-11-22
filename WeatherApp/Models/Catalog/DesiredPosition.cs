using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class DesiredPosition
    {
        public DesiredPosition(int desiredPositionId, string desiredPositionDescription)
        {
            DesiredPositionId = desiredPositionId;
            DesiredPositionDescription = desiredPositionDescription;
        }

        [Key]
        public int DesiredPositionId { get; set; }
        public string DesiredPositionDescription { get; set; }
    }
}
