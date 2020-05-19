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

        public void CreateTable(Table table, Cafe cafe)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO CafeTable (NoOfSeats, TableNumber, CafeID) VALUES (@noOfSeats, @tableNumber, @cafeID)";
                    command.Parameters.AddWithValue("@noOfSeats", table.NoOfSeats);
                    command.Parameters.AddWithValue("@tableNumber", table.TableNumber);
                    command.Parameters.AddWithValue("@cafeID", cafe.ID);
                    command.ExecuteNonQuery();
                    cafe.Tables.Add(table);
                }
                scope.Complete();
                _connection.Close();
            }
        }

        public Table GetTableByID(int ID)
        {
            Table table = new Table();

            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM CafeTable WHERE CafeTable.ID = @id";
                command.Parameters.AddWithValue("@id", ID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    table.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    table.NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats"));
                    table.TableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber"));
                }
                _connection.Close();
            }
            return table;
        }

        public List<Table> GetAllTablesInCafe(Cafe cafe)
        {
            _connection.Open();
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM CafeTable where CafeID = @cafeID";
                command.Parameters.AddWithValue("@cafeID", cafe.ID);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Table table = new Table
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        NoOfSeats = reader.GetInt32(reader.GetOrdinal("NoOfSeats")),
                        TableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber"))
                    };
                    cafe.Tables.Add(table);
                }
            }
            _connection.Close();
            return cafe.Tables;
        }

        public void UpdateTable(Table table)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, _options))
            {
                _connection.Open();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "UPDATE CafeTable SET NoOfSeats = @noOfSeats, TableNumber = @tableNumber WHERE ID = @id";
                    command.Parameters.AddWithValue("@id", table.ID);
                    command.Parameters.AddWithValue("@noOfSeats", table.NoOfSeats);
                    command.Parameters.AddWithValue("@tableNumber", table.TableNumber);
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
                command.CommandText = "DELETE FROM CafeTable WHERE ID = @id";
                command.Parameters.AddWithValue("@id", ID);
                command.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
