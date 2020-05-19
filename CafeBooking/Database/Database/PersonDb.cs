using CafeBooking.Model;
using Database.Interface;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Database
{
    public class PersonDb : ICRUD<Person>
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["CafeBooking"].ConnectionString;

        public void Create(Person entity)
        {

        }

        public void Delete(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"DELETE FROM Person WHERE ID={ID}";
                    command.ExecuteNonQuery();
                }
            }
        }

        public Person Get(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                Person person = new Person();

                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT ID, Name, PhoneNo, Email FROM Person WHERE ID={ID}";

                    SqlDataReader reader = command.ExecuteReader();

                    person.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                    person.Name = reader.GetString(reader.GetOrdinal("Name"));
                    person.PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo"));
                    person.Email = reader.GetString(reader.GetOrdinal("Email"));
                }
                return person;

            }
        }

        public IEnumerable<Person> GetAll()
        {
            List<Person> persons = new List<Person>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT ID, NoOfSeats, Available, TableNumber FROM Table";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Person person = new Person();
                        person.ID = reader.GetInt32(reader.GetOrdinal("ID"));
                        person.Name = reader.GetString(reader.GetOrdinal("Name"));
                        person.PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo"));
                        person.Email = reader.GetString(reader.GetOrdinal("Email"));
                        persons.Add(person);
                    }
                }
            }
            return persons;
        }

        public void Update(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT Person SET Name=?, PhoneNo=?, Email=? WHERE ID={ID}";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
