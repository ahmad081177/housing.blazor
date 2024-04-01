using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousingWebApp.Models
{
    public class Review
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? ReviewText { get; set; } = "";

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public int HouseId { get; set; }
        [ForeignKey("HouseId")]
        public virtual House House { get; set; }
        public int AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public virtual AppUser AppUser { get; set; }
    }
}
