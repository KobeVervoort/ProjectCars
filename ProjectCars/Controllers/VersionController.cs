using System.Collections.Generic;
using System.Linq;
using ProjectCars.Data;
using ProjectCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProjectCars.Services;
using ProjectCars.Entities;

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

    }
}
