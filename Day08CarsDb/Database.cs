using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08CarsDb
{
    public class Database
    {
        const string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\JohnAbbottCollege\Day08CarsDb\CarsDb.mdf;Integrated Security=True;Connect Timeout=30";

        private SqlConnection conn;
        public Database()
        {
            conn = new SqlConnection(ConnString);
            conn.Open();
        }

        public ObservableCollection<Car> GetAllCars()
        {
            try
            {
                ObservableCollection<Car> carList = new ObservableCollection<Car>();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cars", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())    // exception need handle
                {
                    while (reader.Read())
                    {
                            // choose to handle data from database in object class rather than here
                            Car car = new Car(reader);
                            carList.Add(car);
                            Console.WriteLine($"Retrieved records:{car.ToString()}");
                    }
                }
                return carList;
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while querying data from database: " + ex.Message);
            }
            
        }

        public int AddCar(Car car)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Cars (MakeModel, EngineSize, FuelType) output INSERTED.ID VALUES (@MakeModel, @EngineSize, @FuelType)", conn)) //@Name can be any string, even @eee
                {
                    cmd.Parameters.AddWithValue("@MakeModel", car.MakeModel);
                    cmd.Parameters.AddWithValue("@EngineSize", car.EngineSize);
                    cmd.Parameters.AddWithValue("@FuelType", car.FuelType.ToString());
                    //cmd.ExecuteNonQuery();
                    int newId = (int)cmd.ExecuteScalar();
                    Console.WriteLine($"Insert record:{car.ToString()}");
                    return newId;
                }
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while inserting data into database: " + ex.Message);
            }
        }

        public void UpdateCar(Car car)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Cars SET MakeModel = @MakeModel, EngineSize = @EngineSize, FuelType = @FuelType WHERE Id = @Id", conn)) //@Name can be any string, even @eee
                {
                    cmd.Parameters.AddWithValue("@MakeModel", car.MakeModel);
                    cmd.Parameters.AddWithValue("@EngineSize", car.EngineSize);
                    cmd.Parameters.AddWithValue("@FuelType", car.FuelType.ToString());
                    cmd.Parameters.AddWithValue("@Id", car.Id);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Update record:{car.ToString()}");
                }
            }
            catch (SqlException ex)
            {
                throw new DataInvalidException("Error while updating data in database: " + ex.Message);
            }
        }

        public void DeleteCar(int id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Cars WHERE Id = @Id", conn)) //@Name can be any string, even @eee
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
