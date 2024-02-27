using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HousingWebApp.Models
{
    public class AppLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
    }
}
