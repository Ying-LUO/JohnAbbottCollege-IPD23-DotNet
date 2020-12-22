using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz4PersonEF
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext() : base("Quiz4PersonPassportEFDB")
        {
           // Database.SetInitializer<PersonDbContext>(new DropCreateDatabaseIfModelChanges<PersonDbContext>());
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Passport> Passports { get; set; }
    }
}
