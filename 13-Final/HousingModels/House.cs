using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousingModels.Models
{
    public class House
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Info { get; set; }
        [Required]
        public int Price { get; set; }

        //NOTE: Each string is of VARCHAR(MAX). This is done in HousingDBContext.cs
        public ICollection<string> Tags { get; set; } = Array.Empty<string>();

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
}
