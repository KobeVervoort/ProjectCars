using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectCars.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ProjectCars.Data
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(CarsContext carsContext)
        {
            if (((carsContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)?.Exists()).GetValueOrDefault(false))
            {
                return;
            }
            var owners = new List<Owner>();
            for (var i = 0; i < 20; i++)
            {
                owners.Add(new Owner { FirstName = $"Owner First Name {i}", LastName = $"Owner Last Name {i}" });
            }

            var versions = new List<Entities.Version>();
            for (var i = 0; i < 20; i++)
            {
                versions.Add(new Entities.Version { Brand = $"Version Brand {i}", Model = $"Version Model {i}" });
            }

            var cars = new List<Car>();
            for (var i = 0; i < 20; i++)
            {
                cars.Add(new Car { Color = $"color {i}", DatePurchased = new DateTime(2002, 10, 18), LicensePlate = $"123456-{i}", Owner = owners[i], Version = versions[i] });
            }

            carsContext.Database.EnsureCreated();
            carsContext.Owners.AddRange(owners);
            carsContext.Versions.AddRange(versions);
            carsContext.Cars.AddRange(cars);
            carsContext.SaveChanges();
        }
    }
}
