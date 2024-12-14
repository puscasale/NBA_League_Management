using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Repository
{
    public class MatchBD : IRepository<int, Match>
    {
        private readonly string connectionString;

        public MatchBD(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Match FindOne(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT m.Id, t1.Id AS Team1Id, t1.Name AS Team1Name, 
                           t2.Id AS Team2Id, t2.Name AS Team2Name, m.Date
                    FROM Matches m
                    INNER JOIN Teams t1 ON m.Team1Id = t1.Id
                    INNER JOIN Teams t2 ON m.Team2Id = t2.Id
                    WHERE m.Id = @id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int matchId = reader.GetInt32(reader.GetOrdinal("Id"));
                    int team1Id = reader.GetInt32(reader.GetOrdinal("Team1Id"));
                    string team1Name = reader.GetString(reader.GetOrdinal("Team1Name"));
                    int team2Id = reader.GetInt32(reader.GetOrdinal("Team2Id"));
                    string team2Name = reader.GetString(reader.GetOrdinal("Team2Name"));
                    DateTime date = reader.GetDateTime(reader.GetOrdinal("Date"));

                    Team team1 = new Team(team1Id, team1Name);
                    Team team2 = new Team(team2Id, team2Name);

                    return new Match(matchId, team1, team2, date);
                }
            }
            return null;
        }

        public IEnumerable<Match> FindAll()
        {
            List<Match> matches = new List<Match>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT m.Id, t1.Id AS Team1Id, t1.Name AS Team1Name, 
                           t2.Id AS Team2Id, t2.Name AS Team2Name, m.Date
                    FROM Matches m
                    INNER JOIN Teams t1 ON m.Team1Id = t1.Id
                    INNER JOIN Teams t2 ON m.Team2Id = t2.Id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int matchId = reader.GetInt32(reader.GetOrdinal("Id"));
                    int team1Id = reader.GetInt32(reader.GetOrdinal("Team1Id"));
                    string team1Name = reader.GetString(reader.GetOrdinal("Team1Name"));
                    int team2Id = reader.GetInt32(reader.GetOrdinal("Team2Id"));
                    string team2Name = reader.GetString(reader.GetOrdinal("Team2Name"));
                    DateTime date = reader.GetDateTime(reader.GetOrdinal("Date"));

                    Team team1 = new Team(team1Id, team1Name);
                    Team team2 = new Team(team2Id, team2Name);

                    matches.Add(new Match(matchId, team1, team2, date));
                }
            }
            return matches;
        }

        public Match Save(Match entity)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    INSERT INTO Matches (Id, Team1Id, Team2Id, Date)
                    VALUES (@id, @team1Id, @team2Id, @date)";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@team1Id", entity.Team1.Id);
                command.Parameters.AddWithValue("@team2Id", entity.Team2.Id);
                command.Parameters.AddWithValue("@date", entity.Date);

                command.ExecuteNonQuery();
            }
            return entity;
        }

        public bool Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Matches WHERE Id = @id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
