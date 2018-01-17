using System;
using System.Collections.Generic;
using ProjectCars.Entities;

namespace ProjectCars.Models
{
    public class OwnerDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public List<Car> Cars { get; set; }
        public int Id { get; set; }
    }
}
