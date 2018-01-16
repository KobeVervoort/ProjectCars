using System;
using System.Collections.Generic;

namespace ProjectCars.Models
{
    public class CarListViewModel
    {
        public List<CarDetailViewModel> Cars { get; set; }
        public DateTime DatePurchased => DateTime.Now;
    }

}
