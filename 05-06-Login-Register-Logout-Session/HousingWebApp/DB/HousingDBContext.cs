using Microsoft.EntityFrameworkCore;

namespace HousingWebApp.DB
{
    public class HousingDBContext : DbContext
    {
        public HousingDBContext(DbContextOptions<HousingDBContext> options): base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
