using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRent.WebApi.Database
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual  ICollection<Rental> Rentals { get; set; }

        public virtual int? ScooterId { get; set; }
        public virtual  Scooter Scooter { get; set; }
    }
}
