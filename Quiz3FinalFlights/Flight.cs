using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quiz3FinalFlights
{
    public enum TypeEnum { Domestic, International, Private }

    public class Flight
    {
        private int _id;
        private DateTime _onDay;
        private string _fromCode;
        private string _toCode;
        private TypeEnum _type;
        private int _passengers;

        static string UpperCasePattern = @"^[A-Z\s]+$";
        
        public Flight(int id, DateTime onDay, string fromCode, string toCode, TypeEnum type, int passengers)
        {
            Id = id;
            OnDay = onDay;
            FromCode = fromCode;
            ToCode = toCode;
            Type = type;
            Passengers = passengers;
        }

        public Flight(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            OnDay = Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("OnDay")));
            FromCode = reader.GetString(reader.GetOrdinal("FromCode"));
            ToCode = reader.GetString(reader.GetOrdinal("ToCode"));
            TypeEnum.TryParse(reader.GetString(reader.GetOrdinal("Type")), out _type);
            Type = _type;
            Passengers = reader.GetInt32(reader.GetOrdinal("Passengers"));
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        public DateTime OnDay
        {
            get => _onDay;
            set
            {
                _onDay = value;
            }
        }

        public string FromCode
        {
            get => _fromCode;
            set
            {
                Match m = Regex.Match(value, UpperCasePattern);
                if (value.Length>5 || value.Length<3 || !m.Success)
                {
                    throw new DataInvalidException("Code must be 3-5 Uppercase");
                }
                _fromCode = value;
            }
        }

        public string ToCode
        {
            get => _toCode;
            set
            {
                Match m = Regex.Match(value, UpperCasePattern);
                if (value.Length > 5 || value.Length < 3 || !m.Success)
                {
                    throw new DataInvalidException("Code must be 3-5 Uppercase");
                }
                _toCode = value;
            }
        }

        public TypeEnum Type
        {
            get => _type;
            set
            {
                if (!Enum.IsDefined(typeof(TypeEnum), value))
                {
                    throw new DataInvalidException("Type must be in list");
                }
                _type = value;
            }
        }

        public int Passengers
        {
            get => _passengers;
            set
            {
                if (value<0 || value>200)
                {
                    throw new DataInvalidException("Passengers must be 0-200");
                }
                _passengers = value;
            }
        }
    }
}
