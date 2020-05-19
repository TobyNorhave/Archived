using CafeBooking.Model;
using Database.Interface;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Database
{
    public class TableDb : ICRUD<Table>
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["CafeBooking"].ConnectionString;

        public void Create(Table entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Table (NoOfSeats, Available, TableNumber) VALUES (@noOfSeats, @available, @tableNumber)";
                    command.Parameters.AddWithValue("noOfSeats", entity.NoOfSeats);
                    command.Parameters.AddWithValue("available", entity.Available);
                    command.Parameters.AddWithValue("tableNumber", entity.TableNo);
                    command.ExecuteNonQuery();
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
                    command.CommandText = $"DELETE FROM Table WHERE ID={ID}";
                    command.ExecuteNonQuery();
                }
            }
        }

        public Table Get(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                Table table = new Table();

                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT ID, NoOfSeats, Available, TableNumber, CafeID FROM Table WHERE ID={ID}";

                    SqlDataReader reader = command.ExecuteReader();
                    
                    table.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    table.NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats"));
                    table.Available = reader.GetBoolean(reader.GetOrdinal("Available"));
                    table.TableNo = reader.GetInt32(reader.GetOrdinal("TableNumber"));
                }
                return table;

            }
        }

        public IEnumerable<Table> GetAll()
        {
            List<Table> tables = new List<Table>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT ID, NoOfSeats, Available, TableNumber FROM Table";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Table table = new Table();
                        table.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                        table.NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats"));
                        table.Available = reader.GetBoolean(reader.GetOrdinal("Available"));
                        table.TableNo = reader.GetInt32(reader.GetOrdinal("TableNumber"));
                        tables.Add(table);
                    }
                }
            }
            return tables;
        }

        public void Update(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT Table SET NoOfSeats=?, Available=?, TableNumber=? WHERE ID={ID}";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
