using System;
using System.Collections.Generic;

namespace Day01Cities
{

    // new created inner class, user self-defined
    class City
    {
        public string Name;
        public double PopulationMillions;

        // annotation @override in java, in .net you have to say keyword in front of the method, it's not annotation, it's keyword
        // in .net/C# the method by default is private, you have to decalre it explicity as public
        public override string ToString()
        {
            return string.Format("City {0} with {1} mil. population", Name, PopulationMillions);
        }
    }


    class BetterCity
    {

        public BetterCity(int id, string name)
        {
            Id = id;
            _name2 = name;  // never do this, even though the _name2 is private, but it's still readable in the same class, this way is bypass the setter, should assign the value by setter as below
            Name2 = name;
        }

        public int Id;
        public string Name { get; set; }  // proper with storage and default getter/setter, equivlent to set a private storage and then with public getter/setter as below

        // private storage normally lower case, or underscore in front, better not use in this way
        private string _name2; //storage
        
        public string Name2  // not storage, just getter/setter, like a shell
        {
            get
            {
                return _name2;
            }
            set
            {
                
                if(value.Length < 2)
                {
                    throw new InvalidOperationException("Name length must be at least 2 characters");
                }
                _name2 = value;
            }
        }
    }
    class Program
    {

        static public List<City> CitiesList = new List<City>();  // you have to repeat the type of list in the right side of declaration
        //OOP
        // since the method/variable is by default private, you have to declare it to public in class 
        // there is a internal excutable modifier, like: public/private/internel/...
        // if no initialized variable name, they equal to 0 or null
        // one sulotion one project
        // one project is one assembly
        //if you say :  c1.name = "montreal"; it's error due to it's private by default, you cannot read the variable name from c1 instance
        // mscorelib provide the library and reference for diff. purposes, you can check from "object brower"
        static void Main(string[] args)
        {
            try
            {
                City c1 = new City();
                c1.Name = "Montreal";
                c1.PopulationMillions = 2.5;
                Console.WriteLine(c1);


                // this is not a constructor
                City c2 = new City
                {
                    Name = "Toronto",
                    PopulationMillions = 4.5
                };

                BetterCity bc1 = new BetterCity();
                bc1.Name = "Vancouver";
                bc1.Name2 = "Test";   // this line actually calls the getter/setter, different with Name

                CitiesList.Add(new City { Name = "New York"});
                CitiesList.Add(new City { Name = "LA" });
                CitiesList.Add(new City { Name = "London" });

                // read and write of the variable of object without getter/setter
                CitiesList[1].PopulationMillions = 10;

                double pop2 = CitiesList[2].PopulationMillions;


                // slightly diff. with java by : and +
                foreach (City c in CitiesList)
                {
                    Console.WriteLine("City is " + c);
                }

            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }



        }



    }
}
