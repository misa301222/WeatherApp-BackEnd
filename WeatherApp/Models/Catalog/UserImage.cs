using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class UserImage
    {
        public UserImage(string email, string imageURL)
        {
            Email = email;
            ImageURL = imageURL;
        }

        [Key]
        public string Email { get; set; }
        public string ImageURL { get; set; }
    }
}
