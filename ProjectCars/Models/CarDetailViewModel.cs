using System;
using ProjectCars.Entities;

namespace ProjectCars.Models
{
    public class CarDetailViewModel
    {
        public string Color { get; set; }
        public DateTime DatePurchased { get; set; }
        public string LicensePlate { get; set; }
        public string Owner { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Id { get; set; }
    }
}
