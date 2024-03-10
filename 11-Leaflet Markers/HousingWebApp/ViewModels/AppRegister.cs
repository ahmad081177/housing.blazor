using HousingWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace HousingWebApp.ViewModels
{
    public class AppRegister
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [EmailAddress]
        [Compare("Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; } = new Address();
    }
}
