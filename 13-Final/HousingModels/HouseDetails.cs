namespace HousingModels.Models
{
    public class HouseDetails
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool HasSwimmingPool { get; set; } = false;
        public bool HasParking { get; set; } = false;
        public int Floor { get; set; } = 1;
        public int Bathrooms { get; set; } = 1;
        public int Bedrooms { get; set; } = 2;
    }
}
