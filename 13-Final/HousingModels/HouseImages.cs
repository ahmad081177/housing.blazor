using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousingModels.Models
{
    public class HouseImages
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string MainImage { get; set; } = "";
        //NOTE: Each string is of VARCHAR(MAX). This is done in HousingDBContext.cs
        public ICollection<string> Base64Images { get; set; } = Array.Empty<string>();
    }
}
