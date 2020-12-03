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

        // DONT set it as public since it may create null object when initiate new by outside, better to set as protected to just be used by child class
        protected Person()
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

            if (strList.Length!=3)
            {
                throw new InvalidParameterException("Line has invalid number of fields:\n" + dataLine);
            }
            if (!strList[0].Equals("Person"))
            {
                throw new InvalidParameterException("Line does not define Person:\n" + dataLine);
            }
            
            Name = strList[0];
            int age;
            if(int.TryParse(strList[1], out age))
            {
                throw new InvalidParameterException("Line age must be integer:\n" + dataLine);
            }
            Age = age;

        }


        //modifier can be changed to internal --can be access within a project/ protected  -- cannot be access without a project
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
                    Program.LogFailSet?.Invoke("Name set invalid");
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


        // intend for child class to override method, has to add modifier virtual onece in the parent class
        public virtual string ToDataString()
        {
            return string.Format("Person;{0};{1}", Name, Age);
        }

    }
}
