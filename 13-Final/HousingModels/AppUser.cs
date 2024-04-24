using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousingModels.Models
{
    public class AppUser
    {
        [Key] //DB
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }="";
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        [Column(TypeName = "varchar(max)")]
        public string ? ProfileImage { get; set; }
        public char Gender { get; set; } = 'M';
        public string? Phone { get; set; }
        //Prefered to be in separate table
        public bool IsAdmin { get; set; } = false;

        public bool IsBlocked { get; set; } = false;
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; } = new();

        //public virtual ICollection<House> Houses { get; set; } = new List<House>();
        
        //public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
