﻿using System;
using System.Collections.Generic;

namespace ProjectCars.Entities
{
    public class Version
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
