using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data;
using WeatherApp.Models.Catalog;

namespace WeatherApp.EntityFrameworkCore
{
    public class AppDbContext: IdentityDbContext<AppUser, IdentityRole, string>
    {
       
        public AppDbContext(DbContextOptions options): base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Temperature>().HasKey(table => new {
                table.CityId,
                table.DateTemperature
            });

            builder.Entity<Reaction>().HasKey(table => new
            {
                table.Email,
                table.NewsId
            });
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<UserImage> UserImage { get; set; }
        public DbSet<Temperature> Temperature { get; set; }
        public DbSet<DescriptionTemperature> DescriptionTemperature { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public DbSet<DesiredPosition> DesiredPosition { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }
        public DbSet<NotificationType> NotificationType { get; set; }
        public DbSet<TemperatureNotification> TemperatureNotification { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<WeatherApp.Models.Catalog.Reaction> Reaction { get; set; }

    }
}
