using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quiz4PersonEF
{
    public class Passport
    {
        [ForeignKey("Person")]
        public int PassportId { get; set; }
        [Required]
        [StringLength(8)]
        [RegularExpression(@"^[A-Z]{2}[0-9]{6}$")]
        public string Number { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        public virtual Person Person { get; set; }

    }
}
