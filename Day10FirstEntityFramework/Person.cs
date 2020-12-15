using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10FirstEntityFramework
{
    public class Person
    {
        public int Id { get; set; }

        [Required] // means non-null
        [StringLength(50)] //nvarchar(50)
        public string Name { get; set; }

        [Index] // not unique, speeds up lookup operations
        public int Age { get; set; }
    }
}
