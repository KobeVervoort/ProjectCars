using System;
using System.Collections.Generic;

namespace ProjectCars.Entities
{
    public class Version
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string BrandAndModel => $"{Brand} - {Model}";
        public IEnumerable<Car> Cars { get; set; }
    }
}
