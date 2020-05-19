using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace Database
{
    public class PersonDb : IPerson<Customer, Administrator>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Carb"].ConnectionString;
        private TransactionOptions _options;

        public PersonDb()
        {
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public void CreateCustomer(Customer customer)
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
                            command.CommandText = "INSERT INTO Person (FName, LName, PhoneNo, Email) VALUES (@fName, @lName, @phoneNo, @email); SELECT SCOPE_IDENTITY();";
                            command.Parameters.AddWithValue("@fName", customer.FName);
                            command.Parameters.AddWithValue("@lName", customer.LName);
                            command.Parameters.AddWithValue("@phoneNo", customer.PhoneNo);
                            command.Parameters.AddWithValue("@email", customer.Email);
                            int insertedID = Convert.ToInt32(command.ExecuteScalar());

                            command.CommandText = "INSERT INTO Customer (PersonID, VIP) VALUES (@id, @vip)";
                            command.Parameters.AddWithValue("@id", insertedID);
                            command.Parameters.AddWithValue("@vip", customer.VIP);
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

        public Customer GetCustomerByPhoneNo(string PhoneNo)
        {
            Customer customer = new Customer();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Person, Customer WHERE Customer.PersonID = Person.ID and Person.PhoneNo = @phoneNo";
                        command.Parameters.AddWithValue("@phoneNo", PhoneNo);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            customer.ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                            customer.FName = reader.GetString(reader.GetOrdinal("FName"));
                            customer.LName = reader.GetString(reader.GetOrdinal("LName"));
                            customer.PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo"));
                            customer.Email = reader.GetString(reader.GetOrdinal("Email"));
                            customer.VIP = reader.GetBoolean(reader.GetOrdinal("VIP"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Person INNER JOIN Customer ON Person.ID = Customer.PersonID";

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                FName = reader.GetString(reader.GetOrdinal("FName")),
                                LName = reader.GetString(reader.GetOrdinal("LName")),
                                PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                VIP = reader.GetBoolean(reader.GetOrdinal("VIP"))
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            return customers;
        }

        public void UpdateCustomer(Customer customer)
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
                            command.CommandText = "UPDATE Person SET FName = @fName, LName = @lName, PhoneNo = @phoneNo, Email = @email WHERE ID = @id";
                            command.Parameters.AddWithValue("@id", customer.ID);
                            command.Parameters.AddWithValue("@fName", customer.FName);
                            command.Parameters.AddWithValue("@lName", customer.LName);
                            command.Parameters.AddWithValue("@phoneNo", customer.PhoneNo);
                            command.Parameters.AddWithValue("@email", customer.Email);
                            command.ExecuteNonQuery();

                            command.CommandText = "UPDATE Customer SET VIP = @vip WHERE PersonID = @personID";
                            command.Parameters.AddWithValue("@personID", customer.ID);
                            command.Parameters.AddWithValue("@vip", customer.VIP);
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

        public void CreateAdmin(Administrator admin)
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
                            command.CommandText = "INSERT INTO Person (FName, LName, PhoneNo, Email) values (@fName, @lName, @phoneNo, @email); SELECT SCOPE_IDENTITY();";
                            command.Parameters.AddWithValue("@fName", admin.FName);
                            command.Parameters.AddWithValue("@lName", admin.LName);
                            command.Parameters.AddWithValue("@phoneNo", admin.PhoneNo);
                            command.Parameters.AddWithValue("@email", admin.Email);
                            int insertedID = Convert.ToInt32(command.ExecuteScalar());

                            command.CommandText = "INSERT INTO Administrator (PersonID) VALUES (@id)";
                            command.Parameters.AddWithValue("@id", insertedID);
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

        public Administrator GetAdminByPhoneNo(string PhoneNo)
        {
            Administrator admin = new Administrator();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Person, Administrator WHERE Administrator.PersonID = Person.ID and Person.PhoneNo = @phoneNo";
                        command.Parameters.AddWithValue("@phoneNo", PhoneNo);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            admin.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                            admin.FName = reader.GetString(reader.GetOrdinal("FName"));
                            admin.LName = reader.GetString(reader.GetOrdinal("LName"));
                            admin.PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo"));
                            admin.Email = reader.GetString(reader.GetOrdinal("Email"));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            return admin;
        }

        public List<Administrator> GetAllAdmins()
        {
            List<Administrator> admins = new List<Administrator>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Person INNER JOIN Administrator ON Person.ID = Administrator.PersonID";

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Administrator admin = new Administrator
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                FName = reader.GetString(reader.GetOrdinal("FName")),
                                LName = reader.GetString(reader.GetOrdinal("LName")),
                                PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                            };
                            admins.Add(admin);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            return admins;
        }

        public void UpdateAdmin(Administrator admin)
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
                            command.CommandText = "UPDATE Person SET FName = @fName, LName = @lName, PhoneNo = @phoneNo, Email = @email WHERE ID = @id";
                            command.Parameters.AddWithValue("@id", admin.ID);
                            command.Parameters.AddWithValue("@fName", admin.FName);
                            command.Parameters.AddWithValue("@lName", admin.LName);
                            command.Parameters.AddWithValue("@phoneNo", admin.PhoneNo);
                            command.Parameters.AddWithValue("@email", admin.Email);
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
                        command.CommandText = "DELETE FROM Person WHERE ID = @id";
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
