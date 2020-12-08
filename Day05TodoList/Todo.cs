using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day05TodoList
{
    public class Todo : INotifyPropertyChanged
    {

        private string _task; // 1-100 characters, made up of uppercase and lowercase letters, digits, space, _-(),./\ only
        private int _difficulty; // 1-5, as slider
        private DateTime _dueDate; // year 1900-2100 both inclusive, use formatted field
        private StatusEnum _status; // one of: Pending, Done, Delegated - matches the ComboBox in Swing GUI

        public Todo(string task, int difficulty, DateTime dueDate, StatusEnum status)
        {
            Task = task;
            Difficulty = difficulty;
            DueDate = dueDate;
            Status = status;
        }

        public string Task
        {
            get
            {
                return _task;
            }
            set
            {
                    OnPropertyChanged(ref _task, value);
            }
        }

        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                OnPropertyChanged(ref _difficulty, value);
            }
        }

        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                OnPropertyChanged(ref _dueDate, value); 
            }
        }

        public StatusEnum Status
        {
            get
            {
                return _status;
            }
            set
            {
                OnPropertyChanged(ref _status, value); //Enum.TryParse("string", out StatusEnum Status); from string
            }
        }

        public enum StatusEnum { Pending, Done, Delegated }

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
            return string.Format("{0}; {1}; {2}; {3}{4}", Task, Difficulty, DueDate, Status, Environment.NewLine);
        }

    }
}
