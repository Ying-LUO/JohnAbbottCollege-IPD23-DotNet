using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day08CarsDb
{
    public class Car : INotifyPropertyChanged
    {
        private int _id;
        private string _makeModel; // 2-50 characters
        private double _engineSize; // 0-20
        private FuelTypeEnum _fuelType;
        public enum FuelTypeEnum { Gasoline, Diesel, Hybrid, Electric, Other }

        public Car(int id, string makeModel, double engineSize, FuelTypeEnum fuelType)
        {
            Id = id;
            MakeModel = makeModel;
            EngineSize = engineSize;
            FuelType = fuelType;
        }

        public Car(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            MakeModel = reader.GetString(reader.GetOrdinal("MakeModel"));
            EngineSize = Convert.ToDouble(reader.GetDouble(reader.GetOrdinal("EngineSize")));
            FuelTypeEnum.TryParse(reader.GetString(reader.GetOrdinal("FuelType")), out _fuelType);
            FuelType = _fuelType;
        }

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                OnPropertyChanged(ref _id, value);
            }
        }

        public string MakeModel
        {
            get
            {
                return _makeModel;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length > 50 || value.Length < 2) // maynot be reach max length if save data from dialog textbox
                {
                    throw new DataInvalidException("MakeModel must be 2-50 characters");
                }
                OnPropertyChanged(ref _makeModel, value);
            }
        }

        public double EngineSize
        {
            get
            {
                return _engineSize;
            }
            set
            {
                if (value < 0 || value > 20) // maynot be reach if save data from dialog slider
                {
                    throw new DataInvalidException("Engine Size must be 0-20");
                }
                OnPropertyChanged(ref _engineSize, value);
            }
        }

        public FuelTypeEnum FuelType
        {
            get

            {
                return _fuelType;
            }
            set
            {
                if (!Enum.IsDefined(typeof(Car.FuelTypeEnum), value))
                {
                    throw new DataInvalidException("FuelType should be in list of FuelTypeEnum");
                }
                OnPropertyChanged(ref _fuelType, value);
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
            return string.Format($"{Id};{MakeModel};{EngineSize};{FuelType}");
        }
    }
}
