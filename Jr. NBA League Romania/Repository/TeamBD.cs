using System.Data;
using Jr._NBA_League_Romania.Domain;
using Npgsql;


namespace Jr._NBA_League_Romania.Repository;

public class TeamBD : IRepository<int, Team>
{
    private string _connectionString;

    // Constructor to initialize connection string
    public TeamBD(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public Team FindOne(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM Teams WHERE Id = @Id";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var team = new Team(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name"))
                        );
                        return team;
                    }
                }
            }
        }
        return null;
    }
    
    public IEnumerable<Team> FindAll()
    {
        var teams = new List<Team>();

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM Teams";
            using (var command = new NpgsqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var team = new Team(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Name"))
                        );
                        teams.Add(team);
                    }
                }
            }
        }

        return teams;
    }
    
    public Team Save(Team entity)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var query = "INSERT INTO Teams (Id, Name) VALUES (@Id, @Name)";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.Id);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.ExecuteNonQuery();
            }
        }
        return entity;
    }
    
    public bool Delete(int id)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            var query = "DELETE FROM Teams WHERE Id = @Id";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }
    }
    
    
}