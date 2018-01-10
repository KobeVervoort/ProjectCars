using System;
using System.Collections.Generic;

namespace ProjectCars.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
