using Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace Database
{
    public class CafeDb : ICafe<Cafe>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Carb"].ConnectionString;
        private SqlConnection _connection;
        private TransactionOptions _options;

        public CafeDb()
        {
            _connection = new SqlConnection(_connectionString);
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public void Create(Cafe cafe)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Cafe (CName, CAddress, PriceRange, Rating, OpenTime, CloseTime, ZipID, TypeID, AdminID) VALUES (@cName, @cAddress, @priceRange, @rating, @openTime, @closeTime, @zipID, @typeID, @adminID)";
                    command.Parameters.AddWithValue("@cName", cafe.Name);
                    command.Parameters.AddWithValue("@cAddress", cafe.Address);
                    command.Parameters.AddWithValue("@priceRange", cafe.PriceRange);
                    command.Parameters.AddWithValue("@rating", cafe.Rating);
                    command.Parameters.AddWithValue("@openTime", cafe.OpenTime);
                    command.Parameters.AddWithValue("@closeTime", cafe.CloseTime);
                    command.Parameters.AddWithValue("@zipID", cafe.ZipID);
                    command.Parameters.AddWithValue("@typeID", cafe.TypeID);
                    command.Parameters.AddWithValue("@adminID", cafe.Admin.ID);
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
                command.CommandText = "DELETE FROM Cafe WHERE ID = @id";
                command.Parameters.AddWithValue("@id", ID);
                command.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public Cafe GetCafeWithAdmin(int ID)
        {
            Cafe cafe = new Cafe();
            Administrator admin = new Administrator();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Cafe, ZipCity, Person WHERE Cafe.ID = @id AND ZipCity.Zip = ZipID AND Cafe.AdminID = Person.ID";
                command.Parameters.AddWithValue("@id", ID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cafe.Name = reader.GetString(reader.GetOrdinal("CName"));
                    cafe.City = reader.GetString(reader.GetOrdinal("City"));
                    cafe.Address = reader.GetString(reader.GetOrdinal("CAddress"));
                    cafe.PriceRange = reader.GetDecimal(reader.GetOrdinal("PriceRange"));
                    cafe.Rating = reader.GetDecimal(reader.GetOrdinal("Rating"));
                    cafe.OpenTime = reader.GetDateTime(reader.GetOrdinal("OpenTime"));
                    cafe.CloseTime = reader.GetDateTime(reader.GetOrdinal("CloseTime"));
                    cafe.ZipID = reader.GetInt32(reader.GetOrdinal("ZipID"));
                    cafe.TypeID = reader.GetInt32(reader.GetOrdinal("TypeID"));
                    admin.FName = reader.GetString(reader.GetOrdinal("FName"));
                    admin.LName = reader.GetString(reader.GetOrdinal("LName"));
                    admin.PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo"));
                    admin.Email = reader.GetString(reader.GetOrdinal("Email"));
                    cafe.Admin = admin;
                }
            }
            _connection.Close();
            return cafe;
        }

        public Cafe GetCafe(int ID)
        {
            Cafe cafe = new Cafe();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Cafe, ZipCity WHERE Cafe.ID = @id AND ZipCity.Zip = ZipID";
                command.Parameters.AddWithValue("@id", ID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cafe.Name = reader.GetString(reader.GetOrdinal("CName"));
                    cafe.City = reader.GetString(reader.GetOrdinal("City"));
                    cafe.Address = reader.GetString(reader.GetOrdinal("CAddress"));
                    cafe.PriceRange = reader.GetDecimal(reader.GetOrdinal("PriceRange"));
                    cafe.Rating = reader.GetDecimal(reader.GetOrdinal("Rating"));
                    cafe.OpenTime = reader.GetDateTime(reader.GetOrdinal("OpenTime"));
                    cafe.CloseTime = reader.GetDateTime(reader.GetOrdinal("CloseTime"));
                    cafe.ZipID = reader.GetInt32(reader.GetOrdinal("ZipID"));
                    cafe.TypeID = reader.GetInt32(reader.GetOrdinal("TypeID"));
                }
            }
            _connection.Close();
            return cafe;
        }

        public List<Cafe> GetAll()
        {
            List<Cafe> cafes = new List<Cafe>();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Cafe, ZipCity WHERE ZipCity.Zip = ZipID";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cafe cafe = new Cafe
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        Name = reader.GetString(reader.GetOrdinal("CName")),
                        City = reader.GetString(reader.GetOrdinal("City")),
                        Address = reader.GetString(reader.GetOrdinal("CAddress")),
                        PriceRange = reader.GetDecimal(reader.GetOrdinal("PriceRange")),
                        Rating = reader.GetDecimal(reader.GetOrdinal("Rating")),
                        OpenTime = reader.GetDateTime(reader.GetOrdinal("OpenTime")),
                        CloseTime = reader.GetDateTime(reader.GetOrdinal("CloseTime")),
                        ZipID = reader.GetInt32(reader.GetOrdinal("ZipID")),
                        TypeID = reader.GetInt32(reader.GetOrdinal("TypeID"))
                    };
                    cafes.Add(cafe);
                }
            }
            _connection.Close();
            return cafes;
        }

        public void Update(Cafe cafe)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Cafe SET CName = @cName, CAddress = @cAddress, PriceRange = @priceRange, Rating = @rating, OpenTime = @openTime, CloseTime = @closeTime, ZipID = @zipID, TypeID = @typeID WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", cafe.ID);
                    command.Parameters.AddWithValue("@cName", cafe.Name);
                    command.Parameters.AddWithValue("@cAddress", cafe.Address);
                    command.Parameters.AddWithValue("@priceRange", cafe.PriceRange);
                    command.Parameters.AddWithValue("@rating", cafe.Rating);
                    command.Parameters.AddWithValue("@openTime", cafe.OpenTime);
                    command.Parameters.AddWithValue("@closeTime", cafe.CloseTime);
                    command.Parameters.AddWithValue("@zipID", cafe.ZipID);
                    command.Parameters.AddWithValue("@typeID", cafe.TypeID);
                    command.ExecuteNonQuery();
                }
                _connection.Close();
                scope.Complete();
            }
        }
    }
}
