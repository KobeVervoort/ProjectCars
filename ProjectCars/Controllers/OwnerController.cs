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

    }
}
