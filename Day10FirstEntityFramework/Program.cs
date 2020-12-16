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
                Person p1 = new Person { Name = "Jerry", Age = random.Next(100) };
                 p1 = new Person { Name = "Brown", Age = random.Next(100) };
                 p1 = new Person { Name = "Tim", Age = random.Next(100) };

                context.People.Add(p1);  // insert operation is scheduled but NOT executed yet
                context.SaveChanges();  // synchronize objects in memory with database
                Console.WriteLine("Record Added");

                // equivalent of update - fetch then modify, save changes
                Person p2 = (from p in context.People where p.Id == 2 select p).FirstOrDefault<Person>();
                if (p2 != null)
                {// found the record to update
                    p2.Name = "Alabama " + random.Next(10000) + 10000;
                    context.SaveChanges(); //update the database to synchronize entities in memory with the database
                    Console.WriteLine("Record Updated");
                }
                else
                {
                    Console.WriteLine("Record to update not found");
                }

                // delete - fectch then schedule for deletion, then save changes
                Person p3 = (from p in context.People where p.Id == 3 select p).FirstOrDefault<Person>();
                if (p3 !=null)
                {// found the record to delete
                    context.People.Remove(p3);
                    context.SaveChanges();
                    Console.WriteLine("Reocrd deleted");
                }
                else
                {
                    Console.WriteLine("Record to delete not found");
                }

                // fetch all records
                List<Person> peopleList = (from p in context.People select p).ToList<Person>();
                foreach (Person p in peopleList)
                {
                    Console.WriteLine($"{p.Id}: {p.Name} is {p.Age} y/o");
                }
            }
            catch (SystemException ex) // catch-all for Entity Framework, SQL and many other exceptions
            {
                Console.WriteLine("Database operation failed: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }

        }
    }
}
