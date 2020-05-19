using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace Database
{
    public class TableDb : ITable<Table, Cafe>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Carb"].ConnectionString;
        private TransactionOptions _options;

        public TableDb()
        {
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public void CreateTable(Table table, Cafe cafe)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "INSERT INTO CafeTable (NoOfSeats, TableNumber, CafeID) VALUES (@noOfSeats, @tableNumber, @cafeID)";
                            command.Parameters.AddWithValue("@noOfSeats", table.NoOfSeats);
                            command.Parameters.AddWithValue("@tableNumber", table.TableNumber);
                            command.Parameters.AddWithValue("@cafeID", cafe.ID);
                            command.ExecuteNonQuery();
                            cafe.Tables.Add(table);
                        }
                        scope.Complete();
                    }
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }
            }
        }

        public Table GetTableByID(int ID)
        {
            Table table = new Table();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM CafeTable WHERE CafeTable.ID = @id";
                        command.Parameters.AddWithValue("@id", ID);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            table.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                            table.NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats"));
                            table.TableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            return table;
        }

        public List<Table> GetAllTablesInCafe(Cafe cafe)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM CafeTable where CafeID = @cafeID";
                        command.Parameters.AddWithValue("@cafeID", cafe.ID);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Table table = new Table
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats")),
                                TableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber"))
                            };
                            cafe.Tables.Add(table);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            return cafe.Tables;
        }

        public void UpdateTable(Table table)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "UPDATE CafeTable SET NoOfSeats = @noOfSeats, TableNumber = @tableNumber WHERE ID = @id";
                            command.Parameters.AddWithValue("@id", table.ID);
                            command.Parameters.AddWithValue("@noOfSeats", table.NoOfSeats);
                            command.Parameters.AddWithValue("@tableNumber", table.TableNumber);
                            command.ExecuteNonQuery();
                        }
                        scope.Complete();
                    }
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }
            }
        }

        public void Delete(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM CafeTable WHERE ID = @id";
                        command.Parameters.AddWithValue("@id", ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
