using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace Database
{
    public class DbCafeTable
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Carb"].ConnectionString;
        private TransactionOptions _options;

        public DbCafeTable()
        {
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public int CreateTable(CafeTable table, int cafeId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                int insertedId;
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "INSERT INTO CafeTable (TableNo, NoOfSeats, CafeID) VALUES (@tableNo, @noOfSeats, @cafeID); SELECT SCOPE_IDENTITY();";
                            command.Parameters.AddWithValue("@tableNo", table.TableNo);
                            command.Parameters.AddWithValue("@noOfSeats", table.NoOfSeats);
                            command.Parameters.AddWithValue("@cafeID", cafeId);
                            insertedId = Convert.ToInt32(command.ExecuteScalar());
                        }
                        scope.Complete();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return insertedId;
            }
        }

        public CafeTable GetTableByID(int ID)
        {
            CafeTable table = new CafeTable();

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
                            table.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                            table.TableNo = reader.GetInt32(reader.GetOrdinal("TableNo"));
                            table.NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return table;
        }

        public IEnumerable<CafeTable> GetAllTablesInCafe(int cafeId)
        {
            List<CafeTable> cafeTables = new List<CafeTable>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM CafeTable WHERE CafeID = @cafeID";
                        command.Parameters.AddWithValue("@cafeID", cafeId);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CafeTable table = new CafeTable
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                TableNo = reader.GetInt32(reader.GetOrdinal("TableNo")),
                                NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats"))
                            };
                            cafeTables.Add(table);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cafeTables;
        }

        public void UpdateTable(CafeTable table)
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
                            command.CommandText = "UPDATE CafeTable SET TableNo = @tableNo, NoOfSeats = @noOfSeats WHERE ID = @id;";
                            command.Parameters.AddWithValue("@id", table.Id);
                            command.Parameters.AddWithValue("@tableNo", table.TableNo);
                            command.Parameters.AddWithValue("@noOfSeats", table.NoOfSeats);
                            command.ExecuteNonQuery();
                        }
                        scope.Complete();
                    }
                }
                catch (Exception)
                {
                    throw;
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
                throw;
            }
        }
    }
}