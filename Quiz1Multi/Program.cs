using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz1Multi
{
    class Program
    {
        static List<Airport> AirportsList = new List<Airport>();
        const string DataFileName = @"..\..\data.txt";
        static string TimeStampFormat = "yyyy-mm-dd hh:mm:ss";
        static Dictionary<Airport, GeoCoordinate> GeoCoordinateMap = new Dictionary<Airport, GeoCoordinate>();

        static void Main(string[] args)
        {
            try
            {
                ReadDataFromFile();

                while (true)
                {
                    // show the menu and ask the user's choice
                    int choice = getMenuChoice();

                    switch (choice)
                    {
                        case 1:
                            AddAirportInfo();
                            break;
                        case 2:
                            ListAllAirportInfo();
                            break;
                        case 3:
                            FindAirportByCode();
                            break;
                        case 4:
                            FindStandardDeviation();
                            break;
                        case 5:
                            ChangeLogDelegates();
                            break;
                        case 0:
                            WriteDataToFile();
                            return;
                        default:
                            Console.WriteLine("Internal Error: Invalid control flow in menu");
                            break;
                    }

                }
            }
            finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }
        }

        private static void ChangeLogDelegates()
        {
            Console.WriteLine("Changing logging settings:\n1 - Logging to console\n2 - Logging to file");
            Console.WriteLine("Enter your choices, comma - separated, empty for none: ");
            string logStr = Console.ReadLine();

            if (string.IsNullOrEmpty(logStr))
            {
                Console.WriteLine("No Logger will be enabled");
                return;
            }

            string[] logChoice = logStr.Split(',');
            int log;
            foreach (string logch in logChoice)
            {
                if (!int.TryParse(logch, out log))
                {
                    Console.WriteLine("Please input number 1/2 ");
                    return;
                }

                if (log == 1)
                {
                    Airport.Logger += LogToConsole;
                    Console.WriteLine("Logging to console enabled");
                }
                if (log == 2)
                {
                    Airport.Logger += LogToFile;
                    Console.WriteLine("Logging to file enabled.");
                }
            }
  
        }

        private static void FindStandardDeviation()
        {
            
        }

        private static void AirportCoordinate(List<Airport> AirportsList)
        {
            foreach (Airport airport in AirportsList)
            {
                GeoCoordinate Coor = new GeoCoordinate(airport.Latitude, airport.Longitude);
                GeoCoordinateMap[airport] = Coor;
            }
        }

        private static void FindAirportByCode()
        {
            Console.WriteLine("Enter airport code: ");
            string code = Console.ReadLine();
            if (string.IsNullOrEmpty(code))
            {
                Console.WriteLine("Code cannot be empty");
                return;
            }

            Airport currentAirport = null;

            //OR LINQ: from airport in AirportsList where( airport.Code.Equals(code)) select airport;
            var curAir = AirportsList.Where(a => a.Code.Equals(code));

            if (curAir.Count() == 0)
            {
                Console.WriteLine("Cannot find Airport by this code");
                return;
            }
            else
            {
                currentAirport = curAir.First();
            }

            AirportCoordinate(AirportsList);

            Dictionary<Airport, double> Distance = new Dictionary<Airport, double>();

            foreach (Airport airport in AirportsList)
            {
                if (airport != currentAirport)
                {
                    Distance[airport] = GeoCoordinateMap[currentAirport].GetDistanceTo(GeoCoordinateMap[airport]);  //GetDistanceTo return the distance between two coordinates, in meters
                }
            }

            var SmallestDistant = Distance.OrderBy(d => d.Value).First();

            Airport NearestAirport = SmallestDistant.Key;

            Console.WriteLine("Found nearest airport to be {0}/{1} distance is {2:#.##}km", NearestAirport.Code, NearestAirport.City, SmallestDistant.Value/1000);

        }


        private static void ListAllAirportInfo()
        {
            Console.WriteLine(string.Join("\n", AirportsList));
        }

        private static void AddAirportInfo()
        {
            Console.WriteLine("Adding airport");

            Console.WriteLine("Enter code: ");
            string code = Console.ReadLine();
            if (string.IsNullOrEmpty(code))
            {
                Console.WriteLine("Code cannot be empty");
                return;
            }
            
            Console.WriteLine("Enter city: ");
            string city = Console.ReadLine();
            if (string.IsNullOrEmpty(city))
            {
                Console.WriteLine("City cannot be empty");
                return;
            }

            Console.WriteLine("Enter latitude: ");
            double lat;
            Boolean latMParse = double.TryParse(Console.ReadLine(), out lat);
            if (!latMParse)
            {
                Console.WriteLine("Latitude must be double numbers");
                return;
            }

            Console.WriteLine("Enter longitude: ");
            double lng;
            Boolean lngParse = double.TryParse(Console.ReadLine(), out lng);
            if (!lngParse)
            {
                Console.WriteLine("Longitude must be double numbers");
                return;
            }

            Console.WriteLine("Enter elevation in meters: ");
            int elevM;
            Boolean elevMParse = int.TryParse(Console.ReadLine(), out elevM);
            if (!elevMParse)
            {
                Console.WriteLine("Elevation in Meters must be integer numbers");
                return;
            }
            
            try
            {
                AirportsList.Add(new Airport(code, city, lat, lng, elevM)
                {
                    Code = code,
                    City = city,
                    Latitude =lat,
                    Longitude =lng,
                    ElevationMeters =elevM
                });
                Console.WriteLine("Airport added.");
            }
            catch (InvalidParameterException ex)
            {
                Console.WriteLine("Error in Adding Aiport: " + ex.Message);
            }
        }

        private static int getMenuChoice()
        {
            while (true)
            {
                Console.Write(
                                @"1. Add Airport
2. List all airports
3. Find nearest airport by code
4. Find airport's elevation standard deviation
5. Change log delegates
0. Exit
Enter your choice: ");
                string choiceStr = Console.ReadLine();
                int choice;
                if (!int.TryParse(choiceStr, out choice) || choice < 0 || choice > 5)
                {
                    Console.WriteLine("Value must be a number between 0-5");
                    continue;
                }
                return choice;
            }
        }

        private static void WriteDataToFile()
        {
            try
            {
                using (StreamWriter fileOutput = new StreamWriter(DataFileName))
                {
                    foreach (Airport airport in AirportsList)
                    {
                        fileOutput.WriteLine(airport.ToDataString());
                    }
                }
                Console.WriteLine("Data saved back to file.\nGood bye.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing into file: " + ex.Message);
            }
        }

        private static void ReadDataFromFile()
        {
            Console.WriteLine("Data read from file.");

            if (File.Exists(DataFileName))
            {
                try
                {
                    string[] linesArray = File.ReadAllLines(DataFileName);
                    foreach (string line in linesArray)
                    {
                        try
                        {
                            Airport airport = new Airport(line);
                            AirportsList.Add(airport);
                        }
                        catch (InvalidParameterException ex)
                        {
                            Console.WriteLine("Internal Error while reading from file: " + ex.Message);
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Error reading from file: " + ex.Message);
                }

            }
        }


        public static void LogToConsole(string message)
        {
            Console.WriteLine("LOG: " + message + "\n");
        }
        public static void LogToFile(string message)
        {
            File.AppendAllText(@"..\..\events.log", DateTime.Now.ToString(TimeStampFormat) + " LOG: " + message + "\n");
        }

    }

    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(string msg) : base(msg) { }
    }


}
