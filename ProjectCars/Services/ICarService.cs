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
        List<Version> GetAllVersions();
        Version GetVersionById(int id);
        void Persist(Car car);
        void Delete(int id);
    }
}
