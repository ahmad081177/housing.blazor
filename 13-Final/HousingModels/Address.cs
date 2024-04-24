using System.ComponentModel.DataAnnotations;

namespace HousingModels.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Country { get; set; } = "";
        [Required]
        public string City { get; set; } = "";
        public string? Street { get; set; }
        public double Longt { get; set; }
        public double Lat { get; set; }
        public override string ToString()
        {
            string s = "";
            if (!string.IsNullOrWhiteSpace(Street))
            {
                s += Street + ", ";
            }
            if (!string.IsNullOrWhiteSpace(City))
            {
                s += City + ", ";
            }
            if (!string.IsNullOrWhiteSpace(Country))
            {
                s += Country;
            }
            return s;
        }
    }
}
