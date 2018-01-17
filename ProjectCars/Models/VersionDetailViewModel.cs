using System;
using System.Collections.Generic;
using ProjectCars.Entities;

namespace ProjectCars.Models
{
    public class VersionDetailViewModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public List<Car> Cars { get; set; }
        public int Id { get; set; }
    }
}
