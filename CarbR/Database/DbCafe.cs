using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace Database
{
    public class DbCafe
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Carb"].ConnectionString;
        private TransactionOptions _options;

        public DbCafe()
        {
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public int CreateCafe(Cafe cafe, int userId)
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
                            command.CommandText = "INSERT INTO Cafe (CName, CAddress, PriceRange, Rating, OpenTime, CloseTime, PhoneNo, CDescription, CType, UserID, Zip) " +
                                "VALUES (@cName, @cAddress, @priceRange, @rating, @openTime, @closeTime, @phoneNo, @cDescription, @cType, @userId, @zip); SELECT SCOPE_IDENTITY();";
                            command.Parameters.AddWithValue("@cName", cafe.Name);
                            command.Parameters.AddWithValue("@cAddress", cafe.Address);
                            command.Parameters.AddWithValue("@priceRange", cafe.PriceRange);
                            command.Parameters.AddWithValue("@rating", cafe.Rating);
                            command.Parameters.AddWithValue("@openTime", cafe.OpenTime);
                            command.Parameters.AddWithValue("@closeTime", cafe.CloseTime);
                            command.Parameters.AddWithValue("@phoneNo", cafe.PhoneNo);
                            command.Parameters.AddWithValue("@cDescription", cafe.Description);
                            command.Parameters.AddWithValue("@cType", cafe.Type);
                            command.Parameters.AddWithValue("@userID", userId);
                            command.Parameters.AddWithValue("@zip", cafe.Zip);
                            insertedId = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }
                    scope.Complete();
                }
                catch (Exception)
                {
                    throw;
                }
                return insertedId;
            }
        }

        public IEnumerable<Cafe> GetCafesByUserId(int Id)
        {
            DbCafeTable dbCafeTable = new DbCafeTable(); 
            List<Cafe> cafes = new List<Cafe>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Cafe, ZipCity WHERE Cafe.UserID = @id and ZipCity.Zip = cafe.Zip";
                        command.Parameters.AddWithValue("@id", Id);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Cafe cafe = new Cafe
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Name = reader.GetString(reader.GetOrdinal("CName")),
                                City = reader.GetString(reader.GetOrdinal("City")),
                                Address = reader.GetString(reader.GetOrdinal("CAddress")),
                                PriceRange = reader.GetDecimal(reader.GetOrdinal("PriceRange")),
                                Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                                OpenTime = reader.GetDateTime(reader.GetOrdinal("OpenTime")),
                                CloseTime = reader.GetDateTime(reader.GetOrdinal("CloseTime")),
                                PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo")),
                                Description = reader.GetString(reader.GetOrdinal("CDescription")),
                                Type = reader.GetString(reader.GetOrdinal("CType")),
                                Zip = reader.GetString(reader.GetOrdinal("Zip")),
                            };
                            cafe.Tables = dbCafeTable.GetAllTablesInCafe(cafe.Id);
                            cafes.Add(cafe);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cafes;
        }

        public Cafe GetCafeById(int Id)
        {
            DbCafeTable dbCafeTable = new DbCafeTable();
            DbUser dbUser = new DbUser();
            Cafe cafe = new Cafe();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Cafe, ZipCity WHERE ZipCity.Zip = Cafe.Zip AND Cafe.ID = @id";
                        command.Parameters.AddWithValue("@id", Id);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            cafe.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                            cafe.Name = reader.GetString(reader.GetOrdinal("CName"));
                            cafe.City = reader.GetString(reader.GetOrdinal("City"));
                            cafe.Address = reader.GetString(reader.GetOrdinal("CAddress"));
                            cafe.PriceRange = reader.GetDecimal(reader.GetOrdinal("PriceRange"));
                            cafe.Rating = reader.GetInt32(reader.GetOrdinal("Rating"));
                            cafe.OpenTime = reader.GetDateTime(reader.GetOrdinal("OpenTime"));
                            cafe.CloseTime = reader.GetDateTime(reader.GetOrdinal("CloseTime"));
                            cafe.PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo"));
                            cafe.Description = reader.GetString(reader.GetOrdinal("CDescription"));
                            cafe.Type = reader.GetString(reader.GetOrdinal("CType"));
                            cafe.User = dbUser.GetUserByID(reader.GetInt32(reader.GetOrdinal("UserID")));
                            cafe.Zip = reader.GetString(reader.GetOrdinal("Zip"));
                            cafe.Tables = dbCafeTable.GetAllTablesInCafe(cafe.Id);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cafe;
        }

        public IEnumerable<Cafe> GetAllCafes()
        {
            DbCafeTable dbCafeTable = new DbCafeTable();
            List<Cafe> cafes = new List<Cafe>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Cafe, ZipCity Where Cafe.Zip = ZipCity.Zip";

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Cafe cafe = new Cafe
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Name = reader.GetString(reader.GetOrdinal("CName")),
                                City = reader.GetString(reader.GetOrdinal("City")),
                                Address = reader.GetString(reader.GetOrdinal("CAddress")),
                                PriceRange = reader.GetDecimal(reader.GetOrdinal("PriceRange")),
                                Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                                OpenTime = reader.GetDateTime(reader.GetOrdinal("OpenTime")),
                                CloseTime = reader.GetDateTime(reader.GetOrdinal("CloseTime")),
                                PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo")),
                                Description = reader.GetString(reader.GetOrdinal("CDescription")),
                                Type = reader.GetString(reader.GetOrdinal("CType")),
                                Zip = reader.GetString(reader.GetOrdinal("Zip")),
                            };
                            cafe.Tables = dbCafeTable.GetAllTablesInCafe(cafe.Id);
                            cafes.Add(cafe);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cafes;
        }

        public IEnumerable<Cafe> GetAllCafesByZip(string Zip)
        {
            DbCafeTable dbCafeTable = new DbCafeTable();
            List<Cafe> cafes = new List<Cafe>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Cafe, ZipCity WHERE ZipCity.Zip = Cafe.Zip AND Cafe.Zip = @zip";
                        command.Parameters.AddWithValue("@zip", Zip);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Cafe cafe = new Cafe
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Name = reader.GetString(reader.GetOrdinal("CName")),
                                City = reader.GetString(reader.GetOrdinal("City")),
                                Address = reader.GetString(reader.GetOrdinal("CAddress")),
                                PriceRange = reader.GetDecimal(reader.GetOrdinal("PriceRange")),
                                Rating = reader.GetInt32(reader.GetOrdinal("Rating")),
                                OpenTime = reader.GetDateTime(reader.GetOrdinal("OpenTime")),
                                CloseTime = reader.GetDateTime(reader.GetOrdinal("CloseTime")),
                                PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo")),
                                Description = reader.GetString(reader.GetOrdinal("CDescription")),
                                Type = reader.GetString(reader.GetOrdinal("CType")),
                                Zip = reader.GetString(reader.GetOrdinal("Zip")),
                            };
                            cafe.Tables = dbCafeTable.GetAllTablesInCafe(cafe.Id);
                            cafes.Add(cafe);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cafes;
        }

        public void UpdateCafe(Cafe cafe)
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
                            command.CommandText = "UPDATE Cafe SET CName = @cName, CAddress = @cAddress, PriceRange = @priceRange, Rating = @rating, OpenTime = @openTime, CloseTime = @closeTime, PhoneNo = @phoneNo, CDescription = @cDescription, CType = @cType, Zip = @zip WHERE ID = @id;";
                            command.Parameters.AddWithValue("@id", cafe.Id);
                            command.Parameters.AddWithValue("@cName", cafe.Name);
                            command.Parameters.AddWithValue("@cAddress", cafe.Address);
                            command.Parameters.AddWithValue("@priceRange", cafe.PriceRange);
                            command.Parameters.AddWithValue("@rating", cafe.Rating);
                            command.Parameters.AddWithValue("@openTime", cafe.OpenTime);
                            command.Parameters.AddWithValue("@closeTime", cafe.CloseTime);
                            command.Parameters.AddWithValue("@phoneNo", cafe.PhoneNo);
                            command.Parameters.AddWithValue("@cDescription", cafe.Description);
                            command.Parameters.AddWithValue("@cType", cafe.Type);
                            command.Parameters.AddWithValue("@zip", cafe.Zip);
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
                        command.CommandText = "DELETE FROM Cafe WHERE ID = @id";
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