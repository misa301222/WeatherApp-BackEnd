using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Data
{
    public class AppUser : IdentityUser
    {
        public string FullName { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int DefaultCity { get; set; }
        public int DefaultTheme { get; set; }
    }
}
