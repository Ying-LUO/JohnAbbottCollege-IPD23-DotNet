using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02PeopleAgain
{
    class Student : Person
    {
        private string program; // 1-50 characters, no semicolons
        private double gpa; // 0-4.3

        public Student(string name, int age, string program, double gpa) : base(name, age)
        {
            Program = program;
            GPA = gpa;
        }


        public Student(string dataLine)
        {
            string[] strList = dataLine.Split(';');

            try
            {
                Name = strList[0];
                Age = int.Parse(strList[1]);
                Program = strList[2];
                GPA = double.Parse(strList[3]);

                new Student(Name, Age, Program, GPA);
            }
            catch (InvalidParameterException ex)
            {
                Console.WriteLine("Error in parsing data line: " + ex.Message);
            }

        }

        public string Program
        {
            get
            {
                return program;
            }
            set
            {
                if (value.Length > 50 || value.Length < 1 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Program must be 1-50 characters, no semicolon");
                }
                program = value;
            }
        }

        public double GPA
        {
            get
            {
                return gpa;
            }
            set
            {
                if (value > 4.3 || value < 0)
                {
                    throw new InvalidParameterException("GPA must be 0-4.3");
                }
                gpa = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Teacher name {0} is {1} y/o, studies {2} with {3} GPA", Name, Age, Program, GPA);
        }

        public override string ToDataString()
        {
            return string.Format("Student;{0};{1};{2};{3}", Name, Age, Program, GPA);   // string.join can be used as well
        }

    }
}
