using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02PeopleAgain
{
    class Person
    {
        private string name; // 1-50 characters, no semicolons
        private int age; // 0-150

        public Person()
        {
            Name = "";
            Age = 0;
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }


        public Person(string dataLine)
        {
            string[] strList = dataLine.Split(';');

            try
            {
                Name = strList[0];
                Age = int.Parse(strList[1]);
            }catch(InvalidParameterException ex)
            {
                Console.WriteLine("Error in parsing data line: " + ex.Message);
            }

        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length > 50 || value.Length < 1 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Name must be 1-50 characters, no semicolon");
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
                    throw new InvalidParameterException("Age must be 0-150");
                }
                age = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Person name {0} is {1} y/o", Name, Age);
        }

        string ToDataString()
        {
            return string.Format("{0};{1}", Name, Age);
        }

    }
}
