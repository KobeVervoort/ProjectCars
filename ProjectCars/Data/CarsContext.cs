using Microsoft.EntityFrameworkCore;
using ProjectCars.Entities;
namespace ProjectCars.Data
{
    public interface ICarsContext
    {
        DbSet<Car> Cars { get; set; }
        DbSet<Owner> Owners { get; set; }
        DbSet<Version> Versions { get; set; }
    }

    public class CarsContext : DbContext, ICarsContext
    {
        public CarsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasKey(c => c.Id);
            modelBuilder.Entity<Car>().HasOne(c => c.Owner).WithMany(o => o.Cars);
            modelBuilder.Entity<Car>().HasOne(c => c.Version).WithMany(v => v.Cars);

            modelBuilder.Entity<Owner>().HasKey(o => o.Id);
            modelBuilder.Entity<Owner>().HasMany(o => o.Cars).WithOne(c => c.Owner);

            modelBuilder.Entity<Version>().HasKey(v => v.Id);
            modelBuilder.Entity<Version>().HasMany(v => v.Cars).WithOne(c => c.Version);

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Version> Versions { get; set; }

    }
}
