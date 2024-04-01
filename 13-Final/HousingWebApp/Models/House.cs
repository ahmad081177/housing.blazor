using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace HousingWebApp.Models
{
    public class House
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Info { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int SqrRoot { get; set; }
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; } = new();      
        public int AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }
        
        public int HouseDetailsId{ get; set; }
        [ForeignKey("HouseDetailsId")]
        public virtual HouseDetails HouseDetails { get; set; } = new();
        public int HouseImagesId { get; set; }
        [ForeignKey("HouseImagesId")]
        public virtual HouseImages HouseImages { get; set; } = new();
    }
    public class HouseDetails
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool HasSwimmingPool { get; set; } = false;
        public bool HasParking { get; set; }
        public int Floor { get; set; } = 1;
        public int Bathrooms { get; set; } = 1;
        public int Bedrooms { get; set; } = 2;
    }
}
