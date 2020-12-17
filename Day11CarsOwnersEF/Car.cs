using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11CarsOwnersEF
{
    public class Car
    {
        //[Key]
        public int CarId { get; set; }
        [Required]
        [StringLength(50)]
        public string MakeModel { get; set; }

        // fully defined one-to-many relationship at both ends
        // if need nullable OwnerId, set int?
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
