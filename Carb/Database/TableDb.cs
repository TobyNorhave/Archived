using Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace Database
{
    public class TableDb : ITable<Table, Cafe>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Carb"].ConnectionString;
        private SqlConnection _connection;
        private TransactionOptions _options;

        public TableDb()
        {
            _connection = new SqlConnection(_connectionString);
            _options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
        }

        public void Create(Table table)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO CafeTable (NoOfSeats, Available, TableNumber, CafeID) VALUES (@noOfSeats, @available, @tableNumber, @cafeID)";
                    command.Parameters.AddWithValue("@noOfSeats", table.NoOfSeats);
                    command.Parameters.AddWithValue("@available", table.Available);
                    command.Parameters.AddWithValue("@tableNumber", table.TableNumber);
                    command.Parameters.AddWithValue("@cafeID", table.Cafe.ID);
                    command.ExecuteNonQuery();
                }
                scope.Complete();
                _connection.Close();
                table.Cafe.Tables.Add(table);
            }
        }

        public void Delete(int ID)
        {
            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM CafeTable WHERE ID = @id";
                command.Parameters.AddWithValue("@id", ID);
                command.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public Table Get(int ID)
        {
            Table table = new Table();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Table WHERE ID = @id";
                command.Parameters.AddWithValue("@id", ID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    table.NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats"));
                    table.Available = reader.GetBoolean(reader.GetOrdinal("Available"));
                    table.TableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber"));
                }
                _connection.Close();
                return table;
            }
        }

        public List<Table> GetAll(Cafe cafe)
        {
            List<Table> tables = new List<Table>();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM CafeTable where CafeID = @cafeID";
                command.Parameters.AddWithValue("@cafeID", cafe.ID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cafe.ID = reader.GetInt32(reader.GetOrdinal("CafeID"));

                    Table table = new Table
                    {
                        NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats")),
                        Available = reader.GetBoolean(reader.GetOrdinal("Available")),
                        TableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber"))
                    };
                    tables.Add(table);
                }
            }
            _connection.Close();
            return tables;
        }

        public void Update(Table table)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "UPDATE CafeTable SET NoOfSeats = @noOfSeats, Available = @available, TableNumber = @tableNumber WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", table.ID);
                    command.Parameters.AddWithValue("@noOfSeats", table.NoOfSeats);
                    command.Parameters.AddWithValue("@available", table.Available);
                    command.Parameters.AddWithValue("@tableNumber", table.TableNumber);
                    command.ExecuteNonQuery();
                }
                scope.Complete();
                _connection.Close();
            }
        }
    }
}
