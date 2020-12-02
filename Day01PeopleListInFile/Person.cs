using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01PeopleListInFile
{
    class Person
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
                if (value.Length > 100 || value.Length < 2 || value.Contains(";"))
                {
                    throw new ArgumentException("Name must be 2-100 characters long");
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
                    throw new ArgumentException("Age must be 0-150");
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
                    throw new ArgumentException("City must be 2-100 characters long");
                }
                city = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Person name {0} with age {1} in city {2}", Name, Age, City);
        }
    }
}
