﻿using System.Collections.Generic;
using System.Linq;
using ProjectCars.Data;
using ProjectCars.Entities;
using ProjectCars.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;


namespace ProjectCars.Services
{
    public class CarService : ICarService
    {
        private readonly CarsContext _carsContext;

        public CarService(CarsContext carsContext)
        {
            _carsContext = carsContext;
        }

        private IIncludableQueryable<Car, Owner> GetFullGraph()
        {
            return _carsContext.Cars.Include(c => c.Version).Include(c => c.Owner);
        }

        public List<Car> GetAllCars()
        {
            return GetFullGraph().OrderBy(c => c.DatePurchased)
                .ToList();
        }

        public Car GetCarById(int id)
        {
            return GetFullGraph()
                .FirstOrDefault(c => c.Id == id);
        }

        public List<Owner> GetAllOwners()
        {
            return _carsContext.Owners.ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return _carsContext.Owners.Find(id);
        }

        public List<Car> GetAllCarsByOwner(int id)
        {
            return _carsContext.Cars.Where(c => c.OwnerId == id).Include(c => c.Version).ToList();
        }

        public List<Version> GetAllVersions()
        {
            return _carsContext.Versions.ToList();
        }

        public Version GetVersionById(int id)
        {
            return _carsContext.Versions.Find(id);
        }

        public List<Car> GetAllCarsByVersion(int id)
        {
            return _carsContext.Cars.Where(c => c.VersionId == id).Include(c => c.Owner).ToList();
        }

        public void Persist(Car car)
        {
            if (car.Id == 0)
                _carsContext.Cars.Add(car);
            else
                _carsContext.Cars.Update(car);
            _carsContext.SaveChanges();
        }

        public void OwnerPersist(Owner owner)
        {
            if (owner.Id == 0)
                _carsContext.Owners.Add(owner);
            else
                _carsContext.Owners.Update(owner);
            _carsContext.SaveChanges();
        }

        public void VersionPersist(Version version)
        {
            if (version.Id == 0)
                _carsContext.Versions.Add(version);
            else
                _carsContext.Versions.Update(version);
            _carsContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var toDelete = GetCarById(id);
            if (toDelete != null)
            {
                _carsContext.Cars.Remove(toDelete);
                _carsContext.SaveChanges();
            }
        }
    }
}