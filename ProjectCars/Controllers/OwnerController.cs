using System.Collections.Generic;
using System.Linq;
using ProjectCars.Entities;
using ProjectCars.Models;
using ProjectCars.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectCars.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ICarService _carService;

        public OwnerController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("/owners")]
        public IActionResult Index()
        {
            var model = new OwnerListViewModel() { Owners = new List<OwnerDetailViewModel>() };

            var allOwners = _carService.GetAllOwners();

            model.Owners.AddRange(allOwners.Select(ConvertOwnerToOwnerDetailViewModel).ToList());

            return View(model);
        }

        public OwnerDetailViewModel ConvertOwnerToOwnerDetailViewModel(Owner owner)
        {
            return new OwnerDetailViewModel()
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Cars = _carService.GetAllCarsByOwner(owner.Id)
            };
        }

        [HttpGet("/owners/create")]
        public IActionResult create()
        {
            var owner = new Owner();

            var model = ConvertOwnerToOwnerEditViewModel(owner);

            return View(model);
        }

        public OwnerEditViewModel ConvertOwnerToOwnerEditViewModel(Owner owner)
        {
            var model = new OwnerEditViewModel
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName
            };
            return model;
        }

        public IActionResult Persist([FromForm] OwnerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var owner = model.Id == 0 ? new Owner() : _carService.GetOwnerById(model.Id);
                owner.FirstName = model.FirstName;
                owner.LastName = model.LastName;

                _carService.OwnerPersist(owner);

                return Redirect("/owners");
            }
            return View("Index", model);
        }

    }
}
