using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace HousingWebApp.Models
{
    public class AppUser
    {
        [Key] //DB
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; } = new Address();
        public bool IsAdmin { get; set; } //Prefered to be in separate table
    }
}
