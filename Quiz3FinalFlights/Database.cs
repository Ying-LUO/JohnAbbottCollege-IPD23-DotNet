using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz3FinalFlights
{
    public class Database
    {
        const string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\JohnAbbottCollege\Quiz3FinalFlights\FlightsDb.mdf;Integrated Security=True;Connect Timeout=30";

        private SqlConnection conn;
        public Database()
        {
            conn = new SqlConnection(ConnString);
            conn.Open();
        }

        public List<Flight> GetAllFlights()
        {
            try
            {
                List<Flight> list = new List<Flight>();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Flights", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())    // exception need handle
                {
                    while (reader.Read())
                    {
                        // choose to handle data from database in object class rather than here
                        Flight flight = new Flight(reader);
                        list.Add(flight);
                    }
                }
                return list;
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while querying data from database: " + ex.Message);
            }

        }

        public int AddFlight(Flight flight)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Flights (OnDay, FromCode, ToCode, Type, Passengers) output INSERTED.ID VALUES (@OnDay, @FromCode, @ToCode, @Type, @Passengers)", conn)) 
                {
                    cmd.Parameters.AddWithValue("@OnDay", flight.OnDay);
                    cmd.Parameters.AddWithValue("@FromCode", flight.FromCode);
                    cmd.Parameters.AddWithValue("@ToCode", flight.ToCode);
                    cmd.Parameters.AddWithValue("@Type", flight.Type.ToString());
                    cmd.Parameters.AddWithValue("@Passengers", flight.Passengers);
                    int newId = (int)cmd.ExecuteScalar();
                    return newId;
                }
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while inserting data into database: " + ex.Message);
            }
        }

        public void UpdateFlight(Flight flight)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Flights SET OnDay = @OnDay, FromCode = @FromCode, ToCode = @ToCode, Type = @Type, Passengers = @Passengers WHERE Id = @Id", conn)) //@Name can be any string, even @eee
                {
                    cmd.Parameters.AddWithValue("@OnDay", flight.OnDay);
                    cmd.Parameters.AddWithValue("@FromCode", flight.FromCode);
                    cmd.Parameters.AddWithValue("@ToCode", flight.ToCode);
                    cmd.Parameters.AddWithValue("@Type", flight.Type.ToString());
                    cmd.Parameters.AddWithValue("@Passengers", flight.Passengers);
                    cmd.Parameters.AddWithValue("@Id", flight.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while updating data in database: " + ex.Message);
            }
        }

        public void DeleteFlight(int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Flights WHERE Id = @Id", conn)) //@Name can be any string, even @eee
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
