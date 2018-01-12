using System.Collections.Generic;
using System.Linq;
using ProjectCars.Data;
using ProjectCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ProjectCars.Controllers
{
    public class VersionController : Controller
    {
        private readonly CarsContext _carsContext;

        public VersionController(CarsContext carsContext)
        {
            _carsContext = carsContext;
        }

        public IActionResult Index()
        {
            var model = new VersionListViewModel() { Versions = new List<VersionDetailViewModel>() };
            model.Versions = new List<VersionDetailViewModel>();
            var allVersions = _carsContext.Versions.Include(v => v.Cars).ThenInclude(v => v.Owner).OrderBy(o => o.Id).ToList();

            foreach (var version in allVersions)
            {
                var vm = new VersionDetailViewModel
                {
                    Id = version.Id,
                    Brand = version.Brand,
                    Model = version.Model,
                    Cars = ""
                };

                var i = 1;

                foreach (var car in version.Cars)
                {
                    if (i == version.Cars.Count)
                    {
                        vm.Cars += $"{car.LicensePlate}: {car.Owner.FirstName} {car.Owner.LastName} ";
                    }
                    else
                    {
                        vm.Cars += $"{car.LicensePlate}: {car.Owner.FirstName} {car.Owner.LastName}, ";
                    }
                    i++;
                }
                model.Versions.Add(vm);
            }

            return View(model);
        }

    }
}
