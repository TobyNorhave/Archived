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
        private SqlConnection _connection;
        private TransactionOptions _options;

        public PersonDb()
        {
            _connection = new SqlConnection(_connectionString);
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public void CreateCustomer(Customer customer)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
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
                _connection.Close();
            }
        }

        public Customer GetCustomerByID(int ID)
        {
            Customer customer = new Customer();
            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Person, Customer WHERE Customer.PersonID = Person.ID and Person.ID = @id";
                command.Parameters.AddWithValue("@id", ID);

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
            _connection.Close();
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
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
            _connection.Close();
            return customers;
        }

        public void UpdateCustomer(Customer customer)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
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
                _connection.Close();
            }
        }

        public void CreateAdmin(Administrator admin)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
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
                _connection.Close();
            }
        }

        public Administrator GetAdminByID(int ID)
        {
            Administrator admin = new Administrator();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Person, Administrator WHERE Administrator.PersonID = Person.ID and Person.ID = @id";
                command.Parameters.AddWithValue("@id", ID);

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
            _connection.Close();
            return admin;
        }

        public List<Administrator> GetAllAdmins()
        {
            List<Administrator> admins = new List<Administrator>();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
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
            _connection.Close();
            return admins;
        }

        public void UpdateAdmin(Administrator admin)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
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
                _connection.Close();
            }
        }

        public void Delete(int ID)
        {
            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Person WHERE ID = @id";
                command.Parameters.AddWithValue("@id", ID);
                command.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
