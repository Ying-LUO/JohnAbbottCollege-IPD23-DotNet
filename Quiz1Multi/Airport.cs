using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quiz1Multi
{
    class Airport
    {

        Regex CodePattern = new Regex(@"^[A-Z]{3}$");
        Regex CityPattern = new Regex(@"^[A-Za-z0-9 ,.-]{1,50}$");

        public delegate void LoggerDelegate(string message);
        public static LoggerDelegate Logger = null;

        private string _code; // exactly 3 uppercase letters, use regexp
		private string _city; // 1-50 characters, made up of uppercase and lowercase letters, digits, and .,- characters
		private double _latitude, _longitude; // -90 to 90, -180 to 180
		private int _elevationMeters; // -1000 to 10000

        public Airport(string code, string city, double lat, double lng, int elevM) 
        {
            Code = code;
            City = city;
            Latitude = lat;
            Longitude = lng;
            ElevationMeters = elevM;
            if (Logger != null)
            {
                Logger.Invoke("New Airport created. ");
            }
        }

        public Airport(string dataLine)
        {
            string[] strList = dataLine.Split(';');

            if (strList.Length != 5)
            {
                if (Logger != null)
                {
                    Logger.Invoke("Line has invalid number of fields:\n");
                }
                throw new InvalidParameterException("Line has invalid number of fields:\n" + dataLine);
            }
            Code = strList[0];
            City = strList[1];
            double lat;
            if (!double.TryParse(strList[2], out lat))
            {
                if (Logger != null)
                {
                    Logger.Invoke("Line Latitude must be double:\n");
                }
                throw new InvalidParameterException("Line Latitude must be double:\n" + dataLine);
            }
            Latitude = lat;

            double lng;
            if (!double.TryParse(strList[3], out lng))
            {
                if (Logger != null)
                {
                    Logger.Invoke("Line Longitude must be double:\n");
                }
                throw new InvalidParameterException("Line Longitude must be double:\n" + dataLine);
            }
            Longitude = lng;

            int elevM;
            if (!int.TryParse(strList[4], out elevM))
            {
                if (Logger != null)
                {
                    Logger.Invoke("Line ElevationMeters must be integer:\n");
                }
                throw new InvalidParameterException("Line ElevationMeters must be integer:\n" + dataLine);
            }
            ElevationMeters = elevM;
        }

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (!CodePattern.IsMatch(value))
                {
                    if (Logger != null)
                    {
                        Logger.Invoke("Code must be 3 uppercase letters");
                    }
                    throw new InvalidParameterException("Code must be 3 uppercase letters");
                }
                _code = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (!CityPattern.IsMatch(value))
                {

                    if (Logger != null)
                    {
                        Logger.Invoke("City must be 1-50 characters, made up of uppercase and lowercase letters, digits, and .,- characters");
                    }
                    throw new InvalidParameterException("City must be 1-50 characters, made up of uppercase and lowercase letters, digits, and .,- characters");
                }
                _city = value;
            }
        }

        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if (value > 90 || value < -90)
                {
                    if (Logger != null)
                    {
                        Logger.Invoke("Latitude must be -90 to 90");
                    }
                    throw new InvalidParameterException("Latitude must be -90 to 90");
                }
                _latitude = value;
            }
        }

        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if (value > 180 || value < -180)
                {
                    if (Logger != null)
                    {
                        Logger.Invoke("Longitude must be -180 to 180");
                    }
                    throw new InvalidParameterException("Longitude must be -180 to 180");
                }
                _longitude = value;
            }
        }

        public int ElevationMeters
        {
            get
            {
                return _elevationMeters;
            }
            set
            {
                if (value > 10000 || value < -1000)
                {
                    if (Logger != null)
                    {
                        Logger.Invoke("ElevationMeters must be -1000 to 10000");
                    }
                    throw new InvalidParameterException("ElevationMeters must be -1000 to 10000");
                }
                _elevationMeters = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} in {1} at {2} lat / {3} lng at {4}m elevation", Code, City, Latitude, Longitude, ElevationMeters);
        }
        public virtual string ToDataString()
        {
            return string.Format("{0};{1};{2};{3};{4}", Code, City, Latitude, Longitude, ElevationMeters);
        }

	}
}
