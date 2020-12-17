using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11CarsOwnersEF
{
    public class CarsOwnerDbContext : DbContext
    {
        public CarsOwnerDbContext() : base("Day11CarsOwnersEFDB")
        {
            Database.SetInitializer<CarsOwnerDbContext>(new DropCreateDatabaseIfModelChanges<CarsOwnerDbContext>());
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
