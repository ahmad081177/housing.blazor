using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace HousingWebApp.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Info { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int SqrRoot { get; set; }
        [Required]
        public int Year { get; set; }
        public bool HasSwimmingPool { get; set; }
        [Required]
        [Column(TypeName = "varchar(max)")]
        public string MainImage{ get; set; }
        public string Image2 { get; set; } = "";
        //EF - create relation, FK
        public Address Address { get; set; } = new Address();
    }
}
