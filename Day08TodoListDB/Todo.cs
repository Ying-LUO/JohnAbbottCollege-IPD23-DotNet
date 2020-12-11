using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08TodoListDB
{
    public enum TaskStatusEnum { Done, Pending }

    public class Todo
    {
        private int _id;
        private string _task;
        private DateTime _dueDate;
        private TaskStatusEnum _status;

        public Todo(int id, string task, DateTime dueDate, TaskStatusEnum status)
        {
            Id = id;
            Task = task;
            DueDate = dueDate;
            Status = status;
        }

        public Todo(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Task = reader.GetString(reader.GetOrdinal("Task"));
            DueDate = Convert.ToDateTime(reader.GetDateTime(reader.GetOrdinal("DueDate")));
            TaskStatusEnum.TryParse(reader.GetString(reader.GetOrdinal("Status")), out _status);
            Status = _status;
        }


        public int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        public string Task
        {
            get => _task;
            set
            {
                if (value.Length>50 || value.Length<1)
                {
                    throw new DataInvalidException("Task must be 1-50 characters");
                }
                _task = value;
            }
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set
            {   // no specific date validation
                _dueDate = value;
            }
        }

        public TaskStatusEnum Status
        {
            get => _status;
            set
            {
                if (!Enum.IsDefined(typeof(TaskStatusEnum), value))
                {
                    throw new DataInvalidException("Status must be Done/Pending");
                }
                _status = value;
            }
        }

        public override string ToString()
        {
            return string.Format($"{Id};{Task};{DueDate};{Status}");
        }

    }
}
