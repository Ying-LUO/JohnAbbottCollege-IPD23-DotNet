using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz4PersonEF
{
    public class Person
    {
        public int PersonId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }

        public virtual Passport Passport { get; set; }
    }
}
