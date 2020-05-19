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
        private PersonDb _personDb;
        private TableDb _tableDb;

        public CafeDb()
        {
            _personDb = new PersonDb();
            _tableDb = new TableDb();
            _connection = new SqlConnection(_connectionString);
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public void CreateCafe(Cafe cafe)
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

        public List<Cafe> GetCafesByAdminID(int ID)
        {
            List<Cafe> cafes = new List<Cafe>();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Cafe, ZipCity WHERE Cafe.AdminID = @id and ZipCity.Zip = cafe.ZipID";
                command.Parameters.AddWithValue("@id", ID);

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
                        TypeID = reader.GetInt32(reader.GetOrdinal("TypeID")),
                        Admin = _personDb.GetAdminByID(reader.GetInt32(reader.GetOrdinal("AdminID")))
                    };
                    cafe.Tables = _tableDb.GetAllTablesInCafe(cafe);
                    cafes.Add(cafe);
                }
            }
            _connection.Close();
            return cafes;
        }

        public Cafe GetCafeByID(int ID)
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
                    cafe.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    cafe.Name = reader.GetString(reader.GetOrdinal("CName"));
                    cafe.City = reader.GetString(reader.GetOrdinal("City"));
                    cafe.Address = reader.GetString(reader.GetOrdinal("CAddress"));
                    cafe.PriceRange = reader.GetDecimal(reader.GetOrdinal("PriceRange"));
                    cafe.Rating = reader.GetDecimal(reader.GetOrdinal("Rating"));
                    cafe.OpenTime = reader.GetDateTime(reader.GetOrdinal("OpenTime"));
                    cafe.CloseTime = reader.GetDateTime(reader.GetOrdinal("CloseTime"));
                    cafe.ZipID = reader.GetInt32(reader.GetOrdinal("ZipID"));
                    cafe.TypeID = reader.GetInt32(reader.GetOrdinal("TypeID"));
                    cafe.Admin = _personDb.GetAdminByID(reader.GetInt32(reader.GetOrdinal("AdminID")));
                    cafe.Tables = _tableDb.GetAllTablesInCafe(cafe);
                }
            }
            _connection.Close();
            return cafe;
        }

        public List<Cafe> GetAllCafes()
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
                        TypeID = reader.GetInt32(reader.GetOrdinal("TypeID")),
                        Admin = _personDb.GetAdminByID(reader.GetInt32(reader.GetOrdinal("AdminID")))
                    };
                    cafe.Tables = _tableDb.GetAllTablesInCafe(cafe);
                    cafes.Add(cafe);
                }
            }
            _connection.Close();
            return cafes;
        }

        public List<Cafe> GetAllCafesByZip(int zipID)
        {
            List<Cafe> cafes = new List<Cafe>();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Cafe, ZipCity WHERE ZipCity.Zip = ZipID AND ZipID = @zipID";
                command.Parameters.AddWithValue("@zipID", zipID);

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
                        TypeID = reader.GetInt32(reader.GetOrdinal("TypeID")),
                        Admin = _personDb.GetAdminByID(reader.GetInt32(reader.GetOrdinal("AdminID")))
                    };
                    cafe.Tables = _tableDb.GetAllTablesInCafe(cafe);
                    cafes.Add(cafe);
                }
            }
            _connection.Close();
            return cafes;
        }

        public void UpdateCafe(Cafe cafe)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Cafe SET CName = @cName, CAddress = @cAddress, PriceRange = @priceRange, Rating = @rating, OpenTime = @openTime, CloseTime = @closeTime, ZipID = @zipID, TypeID = @typeID, AdminID = @adminID WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", cafe.ID);
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
                _connection.Close();
                scope.Complete();
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
    }
}
