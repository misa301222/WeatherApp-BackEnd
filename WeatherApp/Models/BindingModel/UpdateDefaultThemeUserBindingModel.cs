using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.BindingModel
{
    public class UpdateDefaultThemeUserBindingModel
    {
        public UpdateDefaultThemeUserBindingModel(string email, int defaultTheme)
        {
            Email = email;
            DefaultTheme = defaultTheme;
        }

        public string Email { get; set; }
        public int DefaultTheme { get; set; }
    }
}
