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

        static void Main(string[] args)
        {
            try
            {
                readDataFromFile();
                Console.WriteLine("Data Loaded");
                foreach (Person p in peopleList)
                {
                    Console.WriteLine(p.ToString());  //polymorphism
                }
            }
            finally
            {
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }
        }

        static void readDataFromFile()
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

        static public Person createPersonFromData(string dataLine)
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
