using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02PeopleAgain
{
    class Program
    {
        const string DataFileName = @"..\..\people.txt";
        static List<Person> peopleList = new List<Person>();
        static List<double> gpaList = new List<double>();
        static double avg;

        public delegate void LogFailedSetterDelegate(string reason);
        public static LogFailedSetterDelegate LogFailSet;

        static void Main(string[] args)
        {
            try
            {
                readDataFromFile();
                Console.WriteLine("==========Data Loaded==========");
                foreach (Person p in peopleList)
                {
                    Console.WriteLine(p.ToString());  //polymorphism
                }

                Console.WriteLine("==========Student Only==========");
                foreach (Person p in peopleList)
                {
                    if(p is Student)
                    {
                        Console.WriteLine(p.ToString());  //polymorphism
                    }
                    
                }

                Console.WriteLine("==========Person Only==========");
                foreach (Person p in peopleList)
                {
                    if (p.GetType() == typeof(Person))  // strict match, no inheritance considered
                    {
                        Console.WriteLine(p.ToString());  //polymorphism
                    }
                }

                ComputeGpaStats();

                StandardDeviation();

                WriteBackSorted();

            }
            finally
            {
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
        }

        private static void WriteBackSorted()
        {
            var sortedPeople = peopleList.OrderBy(p => p.Name);
            var sp = from p in peopleList orderby p.Name select p;
            try
            {
                using (StreamWriter fileOutput = new StreamWriter(@"..\..\byName.txt"))
                {
                    foreach (Person person in sortedPeople)
                    {
                        fileOutput.WriteLine(person.ToDataString());
                    }
                };
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
            
        }

        private static void StandardDeviation()
        {
            double sumOfSquares = 0;
            foreach (double gpa in gpaList)
            {
                double squareOfDiff = (gpa - avg) * (gpa - avg);
                sumOfSquares += squareOfDiff;
            }

            double stdDev = Math.Sqrt(sumOfSquares/gpaList.Count);
            Console.WriteLine("Standard deviation of GPAs is: {0}", stdDev);
            Console.WriteLine(string.Join(",", gpaList));
        }

        private static void ComputeGpaStats()
        {
            // average
            double sum = 0;
            int count = 0;
            
            foreach (Person p in peopleList)
            {
                if (p is Student)
                {
                    Student student = p as Student;   // if p is not a student, then p will be assigned as null, if cast problem, cast exception need to be handle
                    gpaList.Add(student.GPA);
                    sum = +student.GPA;
                    count++;
                }
            }
            // fix me: while count is zero
            avg = sum / count;    // not peopleList.Count;

            // sort the list first, then get the middle one as median, if there are two in the middle(even number list), then calculate their average as median
            gpaList.Sort();   // easiest way to sort
            // var use to specifiy the variable type, whatever the type of variable, compiler will help to find it out
            // interfaces 
            // IOrderedEnumerable<double> should be used exactly as var, just not used to
            //var sortedGpaList = gpaList.OrderBy(gpa =>gpa).ToList<double>();
            double median;
            if (gpaList.Count%2 == 1)
            {
                median = gpaList[gpaList.Count/2];
            }
            else
            {
                median = (gpaList[gpaList.Count / 2] + gpaList[gpaList.Count / 2 -1]) / 2;
            }

            //#.## can be repalced by 0.00, diff. is 0.00 will definitely show, like, 3 show as 3.00, but #.## will only show 3
            Console.WriteLine("Average GPA is {0:#.##}, median {1}", avg, median);
        }

        private static void readDataFromFile()
        {
            if (File.Exists(DataFileName))
            {
                string[] linesArray = File.ReadAllLines(DataFileName);
                foreach (string line in linesArray)
                {
                    try
                    {
                        Person p = new Person(line);
                        peopleList.Add(p);
                    }
                    catch (InvalidParameterException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        // factory method

        private static Person createPersonFromData(string dataLine)
        {
            string[] data = dataLine.Split(';');
            switch (data[0])
            {
                case "Person":
                    {
                        if (data.Length != 3)
                        {
                            throw new InvalidParameterException("Line has invalid number of fields:\n" + dataLine);
                        }
                        string name = data[1];
                        int age;
                        if (int.TryParse(data[2], out age))
                        {
                            throw new InvalidParameterException("Line age must be integer:\n" + dataLine);
                        }
                        return new Person(name, age);
                    }
                    break;
                case "Student":
                    {
                        if (data.Length != 5)
                        {
                            throw new InvalidParameterException("Line has invalid number of fields:\n" + dataLine);
                        }
                        string name = data[1];
                        int age;
                        if (int.TryParse(data[2], out age))
                        {
                            throw new InvalidParameterException("Line program must be integer:\n" + dataLine);
                        }
                        string program = data[1];
                        double gpa;
                        if (Double.TryParse(data[2], out gpa))
                        {
                            throw new InvalidParameterException("Line gpa must be integer:\n" + dataLine);
                        }
                        return new Student(name, age, program, gpa);
                    }
                    break;
                case "Teacher":
                    {
                        if (data.Length != 5)
                        {
                            throw new InvalidParameterException("Line has invalid number of fields:\n" + dataLine);
                        }
                        string name = data[1];
                        int age;
                        if (int.TryParse(data[2], out age))
                        {
                            throw new InvalidParameterException("Line program must be integer:\n" + dataLine);
                        }
                        string subject = data[1];
                        double yoe;
                        if (Double.TryParse(data[2], out yoe))
                        {
                            throw new InvalidParameterException("Line gpa must be integer:\n" + dataLine);
                        }
                        return new Student(name, age, subject, yoe);
                    }
                    break;
                default:
                    throw new InvalidParameterException("Wrong option\n" + dataLine);
                    break;
            }
        }
    }
}
