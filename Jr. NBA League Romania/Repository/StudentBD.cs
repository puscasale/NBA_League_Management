using System.Data;
using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Repository;
using Npgsql;
public class StudentBD : IRepository<int, Student>
    {
        private readonly string connectionString;

        public StudentBD(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Student FindOne(int  id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students WHERE Id = @id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader.GetString(reader.GetOrdinal("Name"));
                    string school = reader.GetString(reader.GetOrdinal("School"));
                    return new Student(id, name, school);
                }
            }
            return null;
        }

        public IEnumerable<Student> FindAll()
        {
            List<Student> students = new List<Student>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("Id"));
                    string name = reader.GetString(reader.GetOrdinal("Name"));
                    string school = reader.GetString(reader.GetOrdinal("School"));
                    students.Add(new Student(id, name, school));
                }
            }
            return students;
        }

        public Student Save(Student entity)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Students (Id, Name, School) VALUES (@id, @name, @school)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@name", entity.Name);
                command.Parameters.AddWithValue("@school", entity.School);

                command.ExecuteNonQuery();
            }
            return entity;
        }

        public bool Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Students WHERE Id = @id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }