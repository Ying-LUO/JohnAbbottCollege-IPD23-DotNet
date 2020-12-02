using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02PeopleAgain
{
    class Teacher : Person
    {
        private string subject; // 1-50 characters, no semicolons
        private int yearsOfExperience; // 0-100

        public Teacher(string name, int age, string subject, int yoe) : base(name, age)
        {
            Subject = subject;
            YearsOfExperience = yoe;
        }


        public Teacher(string dataLine)
        {
            string[] strList = dataLine.Split(';');

            try
            {
                Name = strList[0];
                Age = int.Parse(strList[1]);
                Subject = strList[2];
                YearsOfExperience = int.Parse(strList[3]);

                new Teacher(Name, Age, Subject, YearsOfExperience);
            }
            catch (InvalidParameterException ex)
            {
                Console.WriteLine("Error in parsing data line: " + ex.Message);
            }

        }

        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (value.Length > 50 || value.Length < 1 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Subject must be 1-50 characters, no semicolon");
                }
                subject = value;
            }
        }

        public int YearsOfExperience
        {
            get
            {
                return yearsOfExperience;
            }
            set
            {
                if (value > 100 || value < 0)
                {
                    throw new InvalidParameterException("YearsOfExperience must be 0-100");
                }
                yearsOfExperience = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Teacher name {0} is {1} y/o subject {2} years of experience {3}", Name, Age, Subject, YearsOfExperience);
        }

        string ToDataString()
        {
            return string.Format("{0};{1};{2};{3}", Name, Age, Subject, YearsOfExperience);
        }


    }
}
