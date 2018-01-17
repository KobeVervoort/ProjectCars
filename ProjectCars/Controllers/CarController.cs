using System.Collections.Generic;
using System.Linq;
using ProjectCars.Entities;
using ProjectCars.Models;
using ProjectCars.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectCars.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public IActionResult Index()
        {
            var model = new CarListViewModel() { Cars = new List<CarDetailViewModel>() };

            var allCars = _carService.GetAllCars();

            model.Cars.AddRange(allCars.Select(ConvertCarToCarDetailViewModel).ToList());

            return View(model);
        }

        protected CarDetailViewModel ConvertCarToCarDetailViewModel(Car car)
        {
            return new CarDetailViewModel()
            {
                Id = car.Id,
                DatePurchased = car.DatePurchased,
                Color = car.Color,
                LicensePlate = car.LicensePlate,
                Owner = string.Join(' ', car.Owner?.FirstName, car.Owner?.LastName),
                Model = car.Version?.Model,
                Brand = car.Version?.Brand
            };
        }

        [HttpGet("/cars/{id}")]
        public IActionResult Edit([FromRoute] int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }

            var model = ConvertCarToCarEditViewModel(car);
            model.Versions = _carService.GetAllVersions().Select(x => new SelectListItem
            {
                Text = x.BrandAndModel,
                Value = x.Id.ToString()
            }).ToList();

            model.Owners = _carService.GetAllOwners().Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }

        public CarEditViewModel ConvertCarToCarEditViewModel(Car car)
        {
            var model = new CarEditViewModel
            {
                Id = car.Id,
                Color = car.Color,
                DatePurchased = car.DatePurchased,
                LicensePlate = car.LicensePlate,
                Version = car.Version?.BrandAndModel,
                VersionId = car.Version?.Id,
                Owner = car.Owner?.FullName,
                OwnerId = car.Owner?.Id
            };
            return model;
        }

        [HttpPost("/cars/id")]
        public IActionResult Persist([FromForm] CarEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var car = model.Id == 0 ? new Car() : _carService.GetCarById(model.Id);
                car.Color = model.Color;
                car.DatePurchased = model.DatePurchased;
                car.Version = model.VersionId.HasValue ? _carService.GetVersionById(model.VersionId.Value) : null;
                car.Owner = model.OwnerId.HasValue ? _carService.GetOwnerById(model.OwnerId.Value) : null;
                car.LicensePlate = model.LicensePlate;
                _carService.Persist(car);

                return Redirect("/");
            }
            return View("Index", model);
        }

        [HttpGet("/cars/create")]
        public IActionResult create()
        {
            var car = new Car();

            var model = ConvertCarToCarEditViewModel(car);
            model.Versions = _carService.GetAllVersions().Select(x => new SelectListItem
            {
                Text = x.BrandAndModel,
                Value = x.Id.ToString()
            }).ToList();

            model.Owners = _carService.GetAllOwners().Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }


        [HttpPost("/cars/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _carService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
