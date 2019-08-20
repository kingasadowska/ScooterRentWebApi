using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRent.WebApi.Database
{
    public class Scooter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();

        public virtual int? DefectId { get; set; }
        public virtual  Defect Defect { get; set; }

        public virtual  Client Client { get; set; }
    }
}
