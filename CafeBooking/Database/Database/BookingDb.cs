using CafeBooking.Model;
using Database.Interface;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace Database
{
    public class BookingDb : ICRUD<Booking>
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["CafeBooking"].ConnectionString;
        private TableDb _tableDb = new TableDb();

        public void Create(Booking entity)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, options))
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    int insertedId = -1;
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO [Booking] (DateTime, PersonId, TableID) OUTPUT INSERTED.ID VALUES (@dateTime, @personId, @tableId)";
                        command.Parameters.AddWithValue("dateTime", entity.DateTime);
                        command.Parameters.AddWithValue("personId", insertedId);
                        command.Parameters.AddWithValue("tableId", insertedId);
                        command.ExecuteNonQuery();
                    }

                }
                scope.Complete();
            }
        }

        public void Delete(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"DELETE FROM Booking WHERE ID={ID}";
                    command.ExecuteNonQuery();
                }
            }
        }

        public Booking Get(int ID)
        {
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    Booking booking = new Booking();

                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT ID, DateTime, Price,  FROM Booking WHERE TableID=1";

                        SqlDataReader reader = command.ExecuteReader();

                        booking.ID = reader.GetInt32(reader.GetOrdinal("Id"));
                        booking.DateTime = reader.GetDateTime(reader.GetOrdinal("DateTime"));
                        booking.Person.ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                        booking.Table.ID = reader.GetInt32(reader.GetOrdinal("TableID"));
                        return booking;
                    }

                }
            }
        }

        public IEnumerable<Booking> GetAll()
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT ID, DateTime, PersonID, TableId FROM Booking";

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Booking booking = new Booking();
                        booking.ID = reader.GetInt32(reader.GetOrdinal("Id"));
                        booking.DateTime = reader.GetDateTime(reader.GetOrdinal("DateTime"));
                        booking.Person.ID = reader.GetInt32(reader.GetOrdinal("PersonID"));
                        booking.Table.ID = reader.GetInt32(reader.GetOrdinal("TableID"));
                        bookings.Add(booking);
                    }
                }
            }
            return bookings;
        }

        public void Update(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT Booking SET DateTime=? WHERE ID={ID}";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

