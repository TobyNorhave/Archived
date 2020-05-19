using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace Database
{
    public class BookingDb : IBooking<Customer, Cafe, Booking>
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

        public void CreateBooking(Cafe cafe, Booking booking, int noOfPeople)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "select * from CafeTable where NoOfSeats >= @noOfPeople and CafeID = @cafe and CafeTable.ID not in (select CafeTable.ID from CafeTable " +
                    "join Booking on Booking.TableID = CafeTable.ID where CafeID = @cafe and ((Booking.EndDate > @start and Booking.StartDate < @end)))";
                    command.Parameters.AddWithValue("@start", booking.StartDate);
                    command.Parameters.AddWithValue("@end", booking.EndDate);
                    command.Parameters.AddWithValue("@cafe", cafe.ID);
                    command.Parameters.AddWithValue("@noOfPeople", noOfPeople);
                    int insertedID = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = "INSERT INTO Booking (StartDate, EndDate, TableID, PersonID) VALUES (@startDate, @endDate, @tableId, @personId);";
                    command.Parameters.AddWithValue("@startDate", booking.StartDate);
                    command.Parameters.AddWithValue("@endDate", booking.EndDate);
                    command.Parameters.AddWithValue("@tableId", insertedID);
                    command.Parameters.AddWithValue("@personId", booking.Customer.ID);
                    command.ExecuteNonQuery();
                }
                scope.Complete();
                _connection.Close();
            }
        }

        public Booking GetBookingByID(int ID)
        {
            Booking booking = new Booking();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT FROM Booking WHERE ID = @id";
                command.Parameters.AddWithValue("@id", ID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    booking.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    booking.StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                    booking.EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate"));
                    booking.Customer.ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                }
                _connection.Close();
            }
            return booking;
        }

        public List<Booking> GetAllBookingsOnCustomerID(Customer customer)
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
                        StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                        EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate"))
                    };
                    bookings.Add(booking);
                }
                _connection.Close();
            }
            return bookings;
        }

        public List<Booking> GetAllCafeBookings(Cafe cafe)
        {
            List<Booking> bookings = new List<Booking>();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * From Booking, Person, CafeTable WHERE CafeTable.CafeID = Cafe.ID;";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Booking booking = new Booking();

                    bookings.Add(booking);
                }
                _connection.Close();
            }
            return bookings;
        }

        public void UpdateBooking(Booking booking)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Booking SET BookingDate = @bookingDate WHERE ID=@id";
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

                command.CommandText = "DELETE FROM Booking WHERE ID = @id";
                command.Parameters.AddWithValue("@id", ID);
                command.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
