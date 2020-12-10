using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day07CarsWithDialog
{
    public class Car : INotifyPropertyChanged
    {
        private string _makeModel; // 2-50 characters
        private double _engineSize; // 0-20
        private FuelTypeEnum _fuelType;
        public enum FuelTypeEnum { Gasoline, Diesel, Hybrid, Electric, Other }

        public Car(string makeModel, double engineSize, FuelTypeEnum fuelType)
        {
            MakeModel = makeModel;
            EngineSize = engineSize;
            FuelType = fuelType;
        }

        [Name("makeModel")]
        [Index(0)]
        public string MakeModel
        {
            get
            {
                return _makeModel;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length >50 || value.Length<2) // maynot be reach max length if save data from dialog textbox
                {
                    throw new DataInvalidException("MakeModel must be 2-50 characters");
                }
                OnPropertyChanged(ref _makeModel, value);
            }
        }

        [Name("engineSize")]
        [Index(1)]
        public double EngineSize
        {
            get
            {
                return _engineSize;
            }
            set
            {
                if (value<0 || value >20) // maynot be reach if save data from dialog slider
                {
                    throw new DataInvalidException("Engine Size must be 0-20");   
                }
                OnPropertyChanged(ref _engineSize, value);
            }
        }

        [Name("fuelType")]
        [Index(2)]
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
            return string.Format("{0};{1};{2}{3}", MakeModel, EngineSize, FuelType, Environment.NewLine);
        }

    }
}
