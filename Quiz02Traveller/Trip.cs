using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quiz02Traveller
{
    public class Trip : INotifyPropertyChanged
    {
        private String _destination;
        private String _name;
        private String _passportNo;
        private DateTime _depDate;
        private DateTime _retDate;

        static string StringPattern = @"^[-() \\.A-Za-z0-9]{2,30}$";
        static string PassportPattern = @"^[A-Z]{2}[0-9]{6}$";

        public Trip(string destination, string name, string passportNo, DateTime depDate, DateTime returnDate)
        {
            Destination = destination;
            Name = name;
            PassportNo = passportNo;
            DepartureDate = depDate;
            ReturnDate = returnDate;
        }

        public Trip(string dataLine)
        {
            String[] dataStr = dataLine.Split(';');
            if (dataStr.Length != 5)
            {
                throw new DataInvalidException("Invalid data structure,\nDestination;Name;PassportNo;DepartDate;ReturnDate per line");
            }

            try
            {
                Destination = dataStr[0];
                Name = dataStr[1];
                PassportNo = dataStr[2];
                DateTime depDate;
                DateTime.TryParse(dataStr[3], out depDate);
                DepartureDate = depDate;
                DateTime retDate;
                DateTime.TryParse(dataStr[4], out retDate);
                ReturnDate = retDate;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
            {
                throw new DataInvalidException("Error in parsing data to Todo List" + ex.Message);
            }
        }

        public string Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                Match m = Regex.Match(value, StringPattern);
                if(m.Success)
                {
                    OnPropertyChanged(ref _destination, value);
                }
                else
                {
                    throw new DataInvalidException("Destination must be 2-30 characters, no semicolon");
                }
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Match m = Regex.Match(value, StringPattern);
                if (m.Success)
                {
                    OnPropertyChanged(ref _name, value);
                }
                else
                {
                    throw new DataInvalidException("Name must be 2-30 characters, no semicolon");
                }
            }
        }

        public string PassportNo
        {
            get
            {
                return _passportNo;
            }
            set
            {
                Match m = Regex.Match(value, PassportPattern);
                if (m.Success)
                {
                    OnPropertyChanged(ref _passportNo, value);
                }
                else
                {
                    throw new DataInvalidException("PassportNo must be 2 uppercase characters with 6 numbers");
                }
            }
        }

        public DateTime DepartureDate
        {
            get
            {
                return _depDate;
            }
            set
            {
                OnPropertyChanged(ref _depDate, value);
            }
        }

        public DateTime ReturnDate
        {
            get
            {
                return _retDate;
            }
            set
            {
                int result = DateTime.Compare(value, DepartureDate);
                if (result >= 0)
                {
                    OnPropertyChanged(ref _retDate, value);
                }
                else
                {
                    throw new DataInvalidException("Return Date must be greater than or the same as Departure date ");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            property = value;
            var handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4}{5}", Destination, Name, PassportNo, DepartureDate, ReturnDate, Environment.NewLine);
        }
    }
}
