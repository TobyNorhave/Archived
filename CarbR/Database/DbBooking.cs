using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace Database
{
    public class DbBooking
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Carb"].ConnectionString;
        private TransactionOptions _options;

        public DbBooking()
        {
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.Serializable
            };
        }

        public int CreateBooking(int cafeId, int userId, Booking booking, int noOfPeople)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                int createdId;
                try
                {

                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT * FROM CafeTable WHERE NoOfSeats >= @noOfPeople AND CafeID = @cafe AND CafeTable.ID NOT IN (SELECT CafeTable.ID FROM CafeTable " +
                            "JOIN Booking ON Booking.TableID = CafeTable.ID AND ((Booking.EndDate > @start AND Booking.StartDate < @end))); SELECT SCOPE_IDENTITY()";
                            command.Parameters.AddWithValue("@start", booking.StartDate);
                            command.Parameters.AddWithValue("@end", booking.EndDate);
                            command.Parameters.AddWithValue("@cafe", cafeId);
                            command.Parameters.AddWithValue("@noOfPeople", noOfPeople);
                            int insertedID = Convert.ToInt32(command.ExecuteScalar());

                            command.CommandText = "INSERT INTO Booking (BookedDate, StartDate, EndDate, UserID, CafeID, TableID) VALUES (@bookedDate, @startDate, @endDate, @userID, @cafeID, @tableId); SELECT SCOPE_IDENTITY()";
                            command.Parameters.AddWithValue("@bookedDate", booking.BookedDate);
                            command.Parameters.AddWithValue("@startDate", booking.StartDate);
                            command.Parameters.AddWithValue("@endDate", booking.EndDate);
                            command.Parameters.AddWithValue("@userID", userId);
                            command.Parameters.AddWithValue("@cafeID", cafeId);
                            command.Parameters.AddWithValue("@tableId", insertedID);
                            createdId = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }
                    scope.Complete();


                }
                catch (Exception)
                {
                    throw ;
                }
                return createdId;
            }
        }

        public Booking GetBookingById(int Id)
        {
            DbCafe dbCafe = new DbCafe();
            DbCafeTable dbCafeTable = new DbCafeTable();
            DbUser dbUser = new DbUser();
            Booking booking = new Booking();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Booking WHERE booking.ID = @id";
                        command.Parameters.AddWithValue("@id", Id);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            booking.Id = reader.GetInt32(reader.GetOrdinal("ID"));
                            booking.BookedDate = reader.GetDateTime(reader.GetOrdinal("BookedDate"));
                            booking.StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));
                            booking.EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate"));
                            booking.User = dbUser.GetUserByID(reader.GetInt32(reader.GetOrdinal("UserID")));
                            booking.Cafe = dbCafe.GetCafeById(reader.GetInt32(reader.GetOrdinal("CafeID")));
                            booking.Table = dbCafeTable.GetTableByID(reader.GetInt32(reader.GetOrdinal("TableID")));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return booking;
        }

        public IEnumerable<Booking> GetBookingByUserID(int userId)
        {
            DbCafe dbCafe = new DbCafe();
            DbCafeTable dbCafeTable = new DbCafeTable();
            DbUser dbUser = new DbUser();
            List<Booking> bookings = new List<Booking>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Booking WHERE UserID = @userId";
                        command.Parameters.AddWithValue("@userId", userId);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                BookedDate = reader.GetDateTime(reader.GetOrdinal("BookedDate")),
                                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                User = dbUser.GetUserByID(reader.GetInt32(reader.GetOrdinal("UserID"))),
                                Cafe = dbCafe.GetCafeById(reader.GetInt32(reader.GetOrdinal("CafeID"))),
                                Table = dbCafeTable.GetTableByID(reader.GetInt32(reader.GetOrdinal("TableID")))
                            };
                            bookings.Add(booking);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return bookings;
        }

        public IEnumerable<Booking> GetAllCafeBookings(int cafeId)
        {
            DbCafe dbCafe = new DbCafe();
            DbCafeTable dbCafeTable = new DbCafeTable();
            DbUser dbUser = new DbUser();
            List<Booking> bookings = new List<Booking>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Booking WHERE Booking.CafeID = @cafeId;";
                        command.Parameters.AddWithValue("@cafeId", cafeId);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                BookedDate = reader.GetDateTime(reader.GetOrdinal("BookedDate")),
                                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                User = dbUser.GetUserByID(reader.GetInt32(reader.GetOrdinal("UserID"))),
                                Cafe = dbCafe.GetCafeById(reader.GetInt32(reader.GetOrdinal("CafeID"))),
                                Table = dbCafeTable.GetTableByID(reader.GetInt32(reader.GetOrdinal("TableID")))
                            };
                            bookings.Add(booking);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return bookings;
        }

        public void Delete(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText = "DELETE FROM Booking WHERE ID = @id";
                        command.Parameters.AddWithValue("@id", Id);
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