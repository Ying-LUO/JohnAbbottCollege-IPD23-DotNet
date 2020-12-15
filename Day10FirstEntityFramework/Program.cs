using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10FirstEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SocietyDbContext context = new SocietyDbContext();
                Random random = new Random();
                Person p1 = new Person() { Name = "Jerry", Age = random.Next(100) };

                context.People.Add(p1);  // insert operation is scheduled but NOT executed yet
                context.SaveChanges();  // synchronize objects in memory with database
                Console.WriteLine("Record Added");
            }
            finally
            {
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }

        }
    }
}
