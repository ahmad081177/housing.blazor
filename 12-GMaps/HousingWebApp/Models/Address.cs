using System.ComponentModel.DataAnnotations;

namespace HousingWebApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        public string Street { get; set; }
        public double Longt { get; set; }
        public double Lat { get; set; }
    }
}
