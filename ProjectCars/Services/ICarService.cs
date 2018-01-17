using System.Collections.Generic;
using ProjectCars.Entities;

namespace ProjectCars.Services
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car GetCarById(int id);
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        List<Car> GetAllCarsByOwner(int id);
        List<Version> GetAllVersions();
        Version GetVersionById(int id);
        List<Car> GetAllCarsByVersion(int id);
        void Persist(Car car);
        void OwnerPersist(Owner owner);        
        void Delete(int id);
    }
}
