using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08TodoListDB
{
    public class Database
    {
        const string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\JohnAbbottCollege\Day08TodoListDB\TodoListDb.mdf;Integrated Security=True;Connect Timeout=30";

        private SqlConnection conn;
        public Database()
        {
            conn = new SqlConnection(ConnString);
            conn.Open();
        }

        public List<Todo> GetAllTodos()
        {
            try
            {
                List<Todo> list = new List<Todo>();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Todos", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())    // exception need handle
                {
                    while (reader.Read())
                    {
                        // choose to handle data from database in object class rather than here
                        Todo todo = new Todo(reader);
                        list.Add(todo);
                        Console.WriteLine($"Retrieved records:{todo.ToString()}");
                    }
                }
                return list;
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while querying data from database: " + ex.Message);
            }

        }

        public int AddTodo(Todo todo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Todos (Task, DueDate, Status) output INSERTED.ID VALUES (@Task, @DueDate, @Status)", conn)) //@Name can be any string, even @eee
                {
                    cmd.Parameters.AddWithValue("@Task", todo.Task);
                    cmd.Parameters.AddWithValue("@DueDate", todo.DueDate);
                    cmd.Parameters.AddWithValue("@Status", todo.Status.ToString());
                    //cmd.ExecuteNonQuery();
                    int newId = (int)cmd.ExecuteScalar();
                    Console.WriteLine($"Insert record:{todo.ToString()}");
                    return newId;
                }
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while inserting data into database: " + ex.Message);
            }
        }

        public void UpdateTodo(Todo todo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Todos SET Task = @Task, DueDate = @DueDate, Status = @Status WHERE Id = @Id", conn)) //@Name can be any string, even @eee
                {
                    cmd.Parameters.AddWithValue("@Task", todo.Task);
                    cmd.Parameters.AddWithValue("@DueDate", todo.DueDate);
                    cmd.Parameters.AddWithValue("@Status", todo.Status.ToString());
                    cmd.Parameters.AddWithValue("@Id", todo.Id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Update record:{todo.ToString()}");
                }
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while updating data in database: " + ex.Message);
            }
        }

        public void DeleteTodo(int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Todos WHERE Id = @Id", conn)) //@Name can be any string, even @eee
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Delete record of id:{id}");
                }
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while delete data in database: " + ex.Message);
            }
        }

    }
}
