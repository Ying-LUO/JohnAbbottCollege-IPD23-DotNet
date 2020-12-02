using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01PeopleListInFile
{
    class Program      //assemblly default, different with Java public/private
    {
        //start the name from lowercase which is private
        static List<Person> peopleList = new List<Person>();


        static void AddPersonInfo()
        {

            // didn't check the null input string

            Console.WriteLine("Please enter the person's name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter the person's age: ");
            int age;
            int.TryParse(Console.ReadLine(), out age);
            Console.WriteLine("Please enter the person's age: ");
            string city = Console.ReadLine();

            peopleList.Add(new Person(name, age, city)
            {
                Name = name,
                Age = age,
                City = city
            });
        }

        static void ListAllPersonInfo()
        {
            foreach (Person p in peopleList)
            {
                Console.WriteLine("Person in People: " + p);
            }
        }

        static void FindPersonByName()
        {
            Console.WriteLine("Please enter the person's name: ");
            string name = Console.ReadLine();

            foreach (Person p in peopleList)
            {
                if (p.Name.Equals(name))
                {
                    Console.WriteLine("Person you find in People is: " + p);
                }
                Console.WriteLine("Cannot find the person of this name: " + name);
            }
        }

        static void FindPersonYoungerThan()
        {

            Console.WriteLine("Please enter the person's age: ");
            int age;
            int.TryParse(Console.ReadLine(), out age);

            foreach (Person p in peopleList)
            {
                if (p.Age < age)
                {
                    Console.WriteLine("{0} age younger than {1} ", p, age);
                }
                Console.WriteLine("Cannot find the person younger than age: " + age);
            }
        }

        private static int getMenuChoice()
        {
            Console.Write(@"1. Add person info
                            2. List persons info
                            3. Find a person by name
                            4. Find all persons younger than age
                            0. Exit
                            Enter your choice: ");

            int choice;
            bool parseSuccess = int.TryParse(Console.ReadLine(), out choice);
            while (!parseSuccess || choice!=0 ||choice!=1 ||choice!=2 ||choice!=3 ||choice!=4)
            {
                Console.WriteLine("Please enter a valid choice[0/1/2/3/4]: ");
            }
            return choice;
            // parse it to int and return it
        }

        static void Main(string[] args)
        {
            //LoadDataFromFile();
            while (true)
            {
                // show the menu and ask the user's choice
                int choice = getMenuChoice();

                switch (choice)
                {
                    case 1:
                        AddPersonInfo();
                        break;
                    case 2:
                        ListAllPersonInfo();
                        break;
                    case 3:
                        FindPersonByName();
                        break;
                    case 4:
                        FindPersonYoungerThan();
                        break;
                    case 0:
                        AddPersonInfo();
                        break;
                }

                Console.WriteLine("Press any key to finish");
                Console.ReadKey();

            }
        }

        
    }
}
