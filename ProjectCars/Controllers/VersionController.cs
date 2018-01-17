using System.Collections.Generic;
using System.Linq;
using ProjectCars.Entities;
using ProjectCars.Models;
using ProjectCars.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectCars.Controllers
{
    public class VersionController : Controller
    {
        private readonly ICarService _carService;

        public VersionController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("/types")]
        public IActionResult Index()
        {
            var model = new VersionListViewModel() { Versions = new List<VersionDetailViewModel>() };

            var allVersions = _carService.GetAllVersions();

            model.Versions.AddRange(allVersions.Select(ConvertVersionToVersionDetailViewModel).ToList());

            return View(model);
        }

        public VersionDetailViewModel ConvertVersionToVersionDetailViewModel(Version version)
        {
            return new VersionDetailViewModel()
            {
                Id = version.Id,
                Brand = version.Brand,
                Model = version.Model,
                Cars = _carService.GetAllCarsByVersion(version.Id)
            };
        }

        [HttpGet("/types/create")]
        public IActionResult Create()
        {
            var version = new Version();

            var model = ConvertVersionToVersionEditViewModel(version);

            return View(model);
        }

        public VersionEditViewModel ConvertVersionToVersionEditViewModel(Version version)
        {
            var vm = new VersionEditViewModel
            {
                Id = version.Id,
                Brand = version.Brand,
                Model = version.Model
            };

            return vm;
        }

        public IActionResult Persist([FromForm] VersionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var version = new Version();
                version.Brand = model.Brand;
                version.Model = model.Model;

                _carService.VersionPersist(version);

                return Redirect("/types");
            }
            return View("Index", model);
        }

    }
}
