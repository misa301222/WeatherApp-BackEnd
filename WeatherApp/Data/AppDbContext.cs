﻿using Microsoft.AspNetCore.Identity;
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
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<UserImage> UserImage { get; set; }
        public DbSet<Temperature> Temperature { get; set; }
        public DbSet<DescriptionTemperature> DescriptionTemperature { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public DbSet<WeatherApp.Models.Catalog.DesiredPosition> DesiredPosition { get; set; }
        public DbSet<WeatherApp.Models.Catalog.JobApplication> JobApplication { get; set; }

    }
}