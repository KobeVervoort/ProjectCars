using System.Collections.Generic;
using System.Linq;
using ProjectCars.Data;
using ProjectCars.Entities;
using ProjectCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ProjectCars.Controllers
{
    public class CarController : Controller
    {
        private readonly CarsContext _carsContext;

        public CarController(CarsContext carsContext)
        {
            _carsContext = carsContext;
        }

        public IActionResult Index()
        {
            var model = new CarListViewModel() {Cars = new List<CarDetailViewModel>()};
            model.Cars = new List<CarDetailViewModel>();
            var allCars = _carsContext.Cars.Include(c => c.Owner)
                                      .Include(c => c.Version)
                                      .OrderBy(c => c.Id).ToList();

            model.Cars.AddRange(allCars.Select(car => new CarDetailViewModel()
            {
                Id = car.Id,
                Color = car.Color,
                LicensePlate = car.LicensePlate,
                Owner = string.Join(' ', car.Owner?.FirstName, car.Owner?.LastName),
                Model = car.Version?.Model,
                Brand = car.Version?.Brand
            }).ToList());
                                
            return View(model);
        }

    }
}
