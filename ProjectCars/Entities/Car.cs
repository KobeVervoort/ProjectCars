using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectCars.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public DateTime DatePurchased { get; set; }
        public string LicensePlate { get; set; }
        public int? OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
        public int VersionId { get; set; }
        public virtual Version Version { get; set; }
    }
}
