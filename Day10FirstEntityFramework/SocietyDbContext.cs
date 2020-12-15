using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10FirstEntityFramework
{
    class SocietyDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}
