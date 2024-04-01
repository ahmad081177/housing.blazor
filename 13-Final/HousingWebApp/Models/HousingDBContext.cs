using Microsoft.EntityFrameworkCore;

namespace HousingWebApp.Models
{
    public class HousingDBContext : DbContext
    {
        public HousingDBContext(DbContextOptions<HousingDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Converts each str in Base64Images to VARCHAR(MAX)
            modelBuilder.Entity<HouseImages>()
            .Property(e => e.Base64Images)
            .HasColumnType("VARCHAR(MAX)")
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

            modelBuilder.Entity<House>()
                .HasOne(h => h.AppUser)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(h => h.AppUser)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(h => h.House)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<HouseImages> HouseImages { get; set; }
        public DbSet<HouseDetails> HousesDetails { get; set; }
    }
}
