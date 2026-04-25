using Microsoft.EntityFrameworkCore;
using TravelPlanner.Domain.Entities;

namespace TravelPlanner.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PopularPlace> PopularPlaces { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.IsoCode).HasMaxLength(10);
                entity.HasMany(e => e.Cities).WithOne(c => c.Country).HasForeignKey(c => c.CountryId);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasMany(e => e.PopularPlaces).WithOne(p => p.City).HasForeignKey(p => p.CityId);
            });

            modelBuilder.Entity<PopularPlace>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(2000);
            });
        }
    }
}
