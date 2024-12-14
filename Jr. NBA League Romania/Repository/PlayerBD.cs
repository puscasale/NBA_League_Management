using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Repository
{
    public class PlayerBD : IRepository<int, Player>
    {
        private readonly string connectionString;

        public PlayerBD(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Player FindOne(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT p.Id, p.Name, p.School, t.Id AS TeamId, t.Name AS TeamName
                    FROM Players p
                    INNER JOIN Teams t ON p.TeamId = t.Id
                    WHERE p.Id = @id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int playerId = reader.GetInt32(reader.GetOrdinal("Id"));
                    string name = reader.GetString(reader.GetOrdinal("Name"));
                    string school = reader.GetString(reader.GetOrdinal("School"));
                    int teamId = reader.GetInt32(reader.GetOrdinal("TeamId"));
                    string teamName = reader.GetString(reader.GetOrdinal("TeamName"));

                    // Create a Team object based on data retrieved from the database
                    Team team = new Team(teamId, teamName);

                    return new Player(playerId, name, school, team);
                }
            }
            return null;
        }

        public IEnumerable<Player> FindAll()
        {
            List<Player> players = new List<Player>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT p.Id, p.Name, p.School, t.Id AS TeamId, t.Name AS TeamName
                    FROM Players p
                    INNER JOIN Teams t ON p.TeamId = t.Id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int playerId = reader.GetInt32(reader.GetOrdinal("Id"));
                    string name = reader.GetString(reader.GetOrdinal("Name"));
                    string school = reader.GetString(reader.GetOrdinal("School"));
                    int teamId = reader.GetInt32(reader.GetOrdinal("TeamId"));
                    string teamName = reader.GetString(reader.GetOrdinal("TeamName"));

                    Team team = new Team(teamId, teamName);

                    players.Add(new Player(playerId, name, school, team));
                }
            }
            return players;
        }

        public Player Save(Player entity)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Players (Id, Name, School, TeamId) 
                    VALUES (@id, @name, @school, @teamId)";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@name", entity.Name);
                command.Parameters.AddWithValue("@school", entity.School);
                command.Parameters.AddWithValue("@teamId", entity.Team.Id);

                command.ExecuteNonQuery();
            }
            return entity;
        }

        public bool Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Players WHERE Id = @id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
