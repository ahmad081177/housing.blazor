using Microsoft.EntityFrameworkCore;

namespace HousingWebApp.Models
{
    public class HousingDBContext : DbContext
    {
        public HousingDBContext(DbContextOptions<HousingDBContext> options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
