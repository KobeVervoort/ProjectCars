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
    public class OwnerController : Controller
    {
        private readonly CarsContext _carsContext;

        public OwnerController(CarsContext carsContext)
        {
            _carsContext = carsContext;
        }

        public IActionResult Index()
        {
            var model = new OwnerListViewModel() { Owners = new List<OwnerDetailViewModel>() };
            model.Owners = new List<OwnerDetailViewModel>();
            var allOwners = _carsContext.Owners.Include(o => o.Cars).ThenInclude(o => o.Version).OrderBy(o => o.Id).ToList();

            foreach (var owner in allOwners)
            {
                var vm = new OwnerDetailViewModel
                {
                    Id = owner.Id,
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Cars = ""
                };

                var i = 1;

                foreach (var car in owner.Cars)
                {
                    if(i == owner.Cars.Count)
                    {
                        vm.Cars += $"{car.Version.Brand} {car.Version.Model} ";
                    }
                    else
                    {
                        vm.Cars += $"{car.Version.Brand} {car.Version.Model}, ";
                    }
                    i++;
                }
                model.Owners.Add(vm);
            }

            return View(model);
        }

    }
}
