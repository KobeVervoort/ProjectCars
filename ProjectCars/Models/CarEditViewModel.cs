using System;
using ProjectCars.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectCars.Models
{
    public class CarEditViewModel
    {
        public string Color { get; set; }
        public DateTime DatePurchased { get; set; }
        public string LicensePlate { get; set; }
        public string Owner { get; set; }
        public int? OwnerId { get; set; }
        public List<SelectListItem> Owners { get; set; }
        public string Version { get; set; }
        public int? VersionId { get; set; }
        public List<SelectListItem> Versions { get; set; }
        public int Id { get; set; }
    }
}
