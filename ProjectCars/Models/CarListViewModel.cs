using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectCars.Models
{
    public class CarListViewModel
    {
        public List<CarDetailViewModel> Cars { get; set; }
        public DateTime DatePurchased => DateTime.Now;
    }

}
