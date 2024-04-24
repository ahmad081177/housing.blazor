using HousingModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HousingWebApp.DB
{
    internal class StringCollectionComparer : ValueComparer<ICollection<string>>
    {
        public StringCollectionComparer() : base(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList())
        {
        }
    }
    public class HousingDBContext : DbContext
    {
        public HousingDBContext(DbContextOptions<HousingDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Converts each str in Tags to VARCHAR(MAX)
            modelBuilder.Entity<House>()
            .Property(e => e.Tags)
            .HasColumnType("VARCHAR(MAX)")
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList()
            )
            .Metadata.SetValueComparer(new StringCollectionComparer());

            //Converts each str in Base64Images to VARCHAR(MAX)
            modelBuilder.Entity<HouseImages>()
            .Property(e => e.Base64Images)
            .HasColumnType("VARCHAR(MAX)")
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList()
            )
            .Metadata.SetValueComparer(new StringCollectionComparer());

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
