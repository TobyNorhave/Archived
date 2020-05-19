using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Database
{
    public class DbUser
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Carb"].ConnectionString;
        private TransactionOptions _options;

        public DbUser()
        {
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public int CreateUser(User user)
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
                            //(FullName, Email, UserName, PhoneNo, Salt, PasswordHash)
                            command.CommandText = "INSERT INTO UserAuth (FullName, Email, UserName, PhoneNo, Salt, PasswordHash) VALUES (@fullName, @email, @userName, @phoneNo, @salt, @passwordHash); SELECT SCOPE_IDENTITY();";
                            command.Parameters.AddWithValue("@fullName", user.FullName);
                            command.Parameters.AddWithValue("@email", user.Email);
                            command.Parameters.AddWithValue("@userName", user.UserName);
                            command.Parameters.AddWithValue("@phoneNo", user.PhoneNo);
                            command.Parameters.AddWithValue("@salt", user.Salt);
                            command.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
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

        public User GetUserByUserName(string userName)
        {
            User user = new User();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM UserAuth WHERE UserAuth.UserName = @userName";
                        command.Parameters.AddWithValue("@userName", userName);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            user.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                            user.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                            user.Email = reader.GetString(reader.GetOrdinal("Email"));
                            user.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                            user.PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo"));
                            user.Salt = reader.GetString(reader.GetOrdinal("Salt"));
                            user.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public User GetUserByID(int id)
        {
            User user = new User();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM UserAuth WHERE UserAuth.ID = @id";
                        command.Parameters.AddWithValue("@id", id);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            user.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                            user.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                            user.Email = reader.GetString(reader.GetOrdinal("Email"));
                            user.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                            user.PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo"));
                            user.Salt = reader.GetString(reader.GetOrdinal("Salt"));
                            user.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public void UpdateUser(User user)
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
                            //(FullName, Email, UserName, PhoneNo, Salt, PasswordHash)
                            command.CommandText = "UPDATE INTO UserAuth (FullName, Email, UserName, PhoneNo, Salt, PasswordHash) VALUES (@fullName, @email, @userName, @phoneNo, @salt, @passwordHash) WHERE UserAuth.ID = @id";
                            command.Parameters.AddWithValue("@id", user.Id);
                            command.Parameters.AddWithValue("@fullName", user.FullName);
                            command.Parameters.AddWithValue("@email", user.Email);
                            command.Parameters.AddWithValue("@userName", user.UserName);
                            command.Parameters.AddWithValue("@phoneNo", user.PhoneNo);
                            command.Parameters.AddWithValue("@salt", user.Salt);
                            command.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
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
                        command.CommandText = "DELETE FROM UserAuth WHERE ID = @id";
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


