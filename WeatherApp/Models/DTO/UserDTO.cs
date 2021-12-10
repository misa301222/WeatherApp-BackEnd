using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.DTO
{
    public class UserDTO
    {
        public UserDTO(string fullname, string email, string username, DateTime datecreated, List<String> roles, int defaultCity, int defaultTheme)
        {
            this.FullName = fullname;
            this.Email = email;
            this.UserName = username;
            this.DateCreated = datecreated;
            this.Roles = roles;
            this.DefaultCity = defaultCity;
            this.DefaultTheme = defaultTheme;
        }

        public String FullName { get; set; }
        public String Email { get; set; }
        public String UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public String Token { get; set; }
        public List<String> Roles { get; set; }
        public int DefaultCity { get; set; }
        public int DefaultTheme { get; set; }
    }
}
