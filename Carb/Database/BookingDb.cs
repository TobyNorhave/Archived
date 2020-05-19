using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Transactions;

namespace Database
{
    public class BookingDb : IBooking<Customer, Booking>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Carb"].ConnectionString;
        private SqlConnection _connection;
        private TransactionOptions _options;

        public BookingDb()
        {
            _connection = new SqlConnection(_connectionString);
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public void Create(Booking booking)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                try
                {
                    _connection.Open();
                    using (SqlCommand command = _connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO Booking (BookingDate, PersonID)  VALUES (@bookingDate, @personId); SELECT SCOPE_IDENTITY();";
                        command.Parameters.AddWithValue("@bookingDate", booking.BookingDate);
                        command.Parameters.AddWithValue("@personId", booking.Customer.ID);
                        int insertedID = Convert.ToInt32(command.ExecuteScalar());

                        foreach (var item in booking.Tables)
                        {
                            command.CommandText = "UPDATE CafeTable SET BookingID = @bookingID WHERE ID = @ID";
                            command.Parameters.AddWithValue("@bookingID", insertedID);
                            command.Parameters.AddWithValue("@ID", item.ID);
                        }
                        command.ExecuteNonQuery();
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    scope.Dispose();
                }
                _connection.Close();
            }
        }

        public void Delete(int ID)
        {
            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Booking WHERE ID = @id";
                command.Parameters.AddWithValue("@id", ID);
                command.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public Booking Get(int ID)
        {
            Booking booking = new Booking();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT FROM Booking WHERE ID=@id";
                command.Parameters.AddWithValue("@id", ID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    booking.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    booking.BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate"));
                    booking.Customer.ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                }
            }
            _connection.Close();
            return booking;
        }

        public List<Booking> GetAllCustomersOnBooking(Customer customer)
        {
            List<Booking> bookings = new List<Booking>();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Booking WHERE PersonID = @personId";
                command.Parameters.AddWithValue("@personId", customer.ID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    customer.ID = reader.GetInt32(reader.GetOrdinal("PersonID"));

                    Booking booking = new Booking
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate"))
                    };
                    bookings.Add(booking);
                }
            }
            _connection.Close();
            return bookings;
        }

        public List<Booking> GetAllCafeBookings(Cafe cafe)
        {
            List<Booking> bookings = new List<Booking>();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Booking, Person, CafeTable where Person.ID = Booking.PersonID and CafeTable.CafeID = Booking.ID;";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Booking booking = new Booking();

                    bookings.Add(booking);
                }
            }
            _connection.Close();
            return bookings;
        }

        public void Update(Booking booking)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Booking SET BookingDate = @bookingDate WHERE ID=@id";
                    command.Parameters.AddWithValue("@bookingDate", booking.BookingDate);
                    command.ExecuteNonQuery();
                }
                _connection.Close();
                scope.Complete();
            }
        }

    }
}
