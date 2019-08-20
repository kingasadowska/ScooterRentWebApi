using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScooterRent.WebApi.Database
{
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime BeginRentTime { get; set; }
        public DateTime EndRentTime { get; set; }

        public virtual int? ScooterId { get; set; }
        public virtual  Scooter Scooter { get; set; }

        public virtual int? ClientId { get; set; }
        public virtual  Client Client { get; set; }
    }
}