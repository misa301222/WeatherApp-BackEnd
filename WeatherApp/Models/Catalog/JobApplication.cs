using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models.Catalog
{
    public class JobApplication
    {
        public JobApplication(string email, string fullName, int desiredPosition, DateTime applicationDate, string address, string city, string phoneNumber, string education, bool degree, float payment, string description, int applicationStatus)
        {
            Email = email;
            FullName = fullName;
            DesiredPosition = desiredPosition;
            ApplicationDate = applicationDate;
            Address = address;
            City = city;
            PhoneNumber = phoneNumber;
            Education = education;
            Degree = degree;
            Payment = payment;
            Description = description;
            ApplicationStatus = applicationStatus;
        }

        [Key]
        public string Email { get; set; }
        public string FullName { get; set; }
        public int DesiredPosition { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Education { get; set; }
        public bool Degree { get; set; }
        public float Payment { get; set; }
        public string Description { get; set; }
        public int ApplicationStatus { get; set; }
    }
}
