using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08SimpleDatabase
{
    class Program
    {
        const string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\JOHNABBOTTCOLLEGE\DAY08SIMPLEDATABASE\SIMPLEDB.MDF;Integrated Security=True;Connect Timeout=30";
        static void Main(string[] args)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnString);
                conn.Open(); //exception need handle

                Random random = new Random();

                {//insert
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO People (Name, Age) VALUES (@Name, @Age)", conn)) //@Name can be any string, even @eee
                    {
                        cmd.Parameters.AddWithValue("@Name", "Test" + random.Next());
                        cmd.Parameters.AddWithValue("@Age", random.Next(1, 100));
                        cmd.ExecuteNonQuery();
                    }
                }
                {//select
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM People", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())    // exception need handle
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));//(int)reader["Id"];
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            int age = reader.GetInt32(reader.GetOrdinal("Age"));
                            Console.WriteLine($"{id}: {name} is {age} y/0");
                        }
                    }
                }
                {//select specific record
                    int fid = 2; // looking for this one record
                    Console.WriteLine($"Looking for record with Id={fid}");
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM People WHERE Id=@Id", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())    // exception need handle
                    {
                        cmd.Parameters.AddWithValue("@Id", fid);
                        
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("Id"));//(int)reader["Id"];
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            int age = reader.GetInt32(reader.GetOrdinal("Age"));
                            Console.WriteLine($"{id}: {name} is {age} y/0");
                        }else
                        {
                            Console.WriteLine("Record not found");
                        }
                    }
                }
            }
            finally
            {
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
            
             
        }
    }
}
