using System;
using System.Collections.Generic;

namespace Day01PeopleListInFile
{

    public class Person
    {
        //public string Name;
        //public int Age;
        //public string City;

        private string name;
        private int age;
        private string city;

        public Person(string name, int age, string city)
        {
            Name = name;
            Age = age;
            City = city;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(value.Length>100 || value.Length < 2 || value.Contains(";"))
                {
                    throw new InvalidOperationException("Name must be 2-100 characters long");
                }
                name = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value > 150 || value < 0)
                {
                    throw new InvalidOperationException("Age must be 0-150");
                }
                age = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                if (value.Length > 100 || value.Length < 2 || value.Contains(";"))
                {
                    throw new InvalidOperationException("City must be 2-100 characters long");
                }
                city = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Person name {0} with age {1} in city {2}", Name, Age, City);
        }


    }
    class Program      //assemblly default, different with Java public/private
    {
        static List<Person> people = new List<Person>();


        static void AddPersonInfo(string name, int age, string city)
        {
            new Person(name, age, city)
            {
                Name = name,
                Age = age,
                City = city
            };
        }

        static void ListAllPersonInfo()
        {
            foreach (Person p in people)
            {
                Console.WriteLine("Person in People: " + p);
            }
        }

        static void FindPersonByName(string name)
        {
            foreach (Person p in people)
            {
                if (p.Name.Equals(name))
                {
                    Console.WriteLine("Person you find in People is: " + p);
                }
                Console.WriteLine("Cannot find the person of this name: " + name);
            }
        }

        static void FindPersonYoungerThan(int age)
        {
            foreach (Person p in people)
            {
                if (p.Age < age)
                {
                    Console.WriteLine("Person age younger than {0} in People is {} ", age, p);
                }
                Console.WriteLine("Cannot find the person younger than age: " + age);
            }
        }

        static void Main(string[] args)
        {
            



        }
    }
}
