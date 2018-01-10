using Microsoft.EntityFrameworkCore;
using ProjectCars.Entities;
namespace ProjectCars.Data
{
    public class CarsContext : DbContext
    {
        public CarsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasKey(c => c.Id);
            modelBuilder.Entity<Car>().HasOne(c => c.Owner).WithMany(o => o.Cars);
            modelBuilder.Entity<Car>().HasOne(c => c.Version);

            modelBuilder.Entity<Owner>().HasKey(o => o.Id);

            modelBuilder.Entity<Version>().HasKey(v => v.Id);

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Version> Versions { get; set; }

    }
}
