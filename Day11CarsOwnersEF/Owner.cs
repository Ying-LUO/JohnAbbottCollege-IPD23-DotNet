using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11CarsOwnersEF
{
    public class Owner
    {
        public Owner()
        {
            this.CarsInGarage = new HashSet<Car>();
        }

        //[Key]
        public int OwnerId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [NotMapped]
        public int CarsNumber
        {
            get
            {
                if (CarsInGarage == null)
                {
                    return 0;
                }
                return CarsInGarage.Count;
            }
        }
        [Required]
        public byte[] Photo { get; set; }
        public ICollection<Car> CarsInGarage { get; set; }

    }
}
