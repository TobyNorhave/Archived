using CafeBooking.Model;
using Database.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Database
{
    public class CafeDb : ICRUD<Cafe>, IEnumerable
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["CafeBooking"].ConnectionString;

        public void Create(Cafe entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {


                    cmd.CommandText = "insert into [Café] (Name, ZipID, Address, PriceRange, Rating, Type)" +
                        "values(@name, @zip, @address, @pricerange, @rating, @type)";
                    cmd.Parameters.AddWithValue("Name", entity.Name);
                    cmd.Parameters.AddWithValue("ZipID", entity.ZipID);
                    cmd.Parameters.AddWithValue("Address", entity.Address);
                    cmd.Parameters.AddWithValue("PriceRange", entity.PriceRange);
                    cmd.Parameters.AddWithValue("Rating", entity.Rating);
                    cmd.Parameters.AddWithValue("Type", entity.Type);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"DELETE FROM Cafe WHERE ID={ID}";
                    command.ExecuteNonQuery();
                }
            }
        }

        public Cafe Get(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                Cafe cafe = new Cafe();

                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT ID, Name, Address, PriceRange, Rating, Type FROM Cafe WHERE ID={ID}";

                    SqlDataReader reader = command.ExecuteReader();

                    cafe.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    cafe.Name = reader.GetString(reader.GetOrdinal("Name"));
                    cafe.PriceRange = (int)reader.GetDouble(reader.GetOrdinal("PriceRange"));
                    cafe.Rating = reader.GetInt32(reader.GetOrdinal("Rating"));
                    cafe.Type = reader.GetInt32(reader.GetOrdinal("Type"));
                }
                return cafe;

            }
        }

        public IEnumerable<Cafe> GetAll()
        {
            List<Cafe> cafes = new List<Cafe>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * from cafe";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Cafe cafe = new Cafe();
                        cafe.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                        cafe.Name = reader.GetString(reader.GetOrdinal("Name"));
                        cafe.PriceRange = (int)reader.GetDouble(reader.GetOrdinal("PriceRange"));
                        cafe.Rating = reader.GetInt32(reader.GetOrdinal("Rating"));
                        cafe.Type = reader.GetInt32(reader.GetOrdinal("Type"));
                        cafes.Add(cafe);
                    }
                }
            }
            return cafes;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Update(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT Cafe SET Name=?, ZipID=?, Address=?, PriceRange=?, Rating=?, Type=? WHERE ID={ID}";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
