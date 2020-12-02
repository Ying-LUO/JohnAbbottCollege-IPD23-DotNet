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
            Boolean ageParse = int.TryParse(Console.ReadLine(), out age);
            if (!ageParse)
            {
                Console.WriteLine("Age must be numbers");
                return;
            }
            Console.WriteLine("Please enter the person's city: ");
            string city = Console.ReadLine();
            try
            {
                peopleList.Add(new Person(name, age, city)
                {
                    Name = name,
                    Age = age,
                    City = city
                });
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
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
                else
                {
                    Console.WriteLine("Cannot find the person of this name: " + name);
                }
                
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
                    Console.WriteLine("{0} is younger than {1} ", p, age);
                }
                else
                {
                    Console.WriteLine("Cannot find the person younger than age: " + age);
                }
                
            }
        }

        private static void SaveDataToFile()
        {

        }

        private static int getMenuChoice()
        {
            int choice;
            while (true)
            {
                Console.Write(@"1. Add person info
                            2. List persons info
                            3. Find a person by name
                            4. Find all persons younger than age
                            0. Exit
                            Enter your choice: ");


                bool parseSuccess = int.TryParse(Console.ReadLine(), out choice);
                if (!parseSuccess || choice <0 || choice >4)
                {
                    Console.WriteLine("Please enter a valid choice[0-4]: ");
                    continue;
                }
                return choice;
                // parse it to int and return it
            }
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
                        //Environment.Exit(1);
                        SaveDataToFile();
                        return;
                    default:   // default MUST ALWAYS had in the switch
                        Console.WriteLine("Internal Error: Invalid control flow in menu");
                        break;
                }

            }

            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }

        
    }
}
