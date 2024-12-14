using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using Jr._NBA_League_Romania.Domain;

namespace Jr._NBA_League_Romania.Repository
{
    public class ActivePlayerBD : IRepository<int, ActivePlayer>
    {
        private readonly string connectionString;

        public ActivePlayerBD(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ActivePlayer FindOne(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT ap.Id, ap.IdPlayer, ap.IdMatch, ap.NrPoints, ap.Type
            FROM ActivePlayers ap
            WHERE ap.Id = @id";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int activePlayerId = reader.GetInt32(reader.GetOrdinal("Id"));
                    int playerId = reader.GetInt32(reader.GetOrdinal("IdPlayer"));
                    int matchId = reader.GetInt32(reader.GetOrdinal("IdMatch"));
                    int nrPoints = reader.GetInt32(reader.GetOrdinal("NrPoints"));
                    string type = reader.GetString(reader.GetOrdinal("Type"));

                    return new ActivePlayer(activePlayerId, playerId, matchId, nrPoints, type);
                }
            }
            return null;
        }


        public IEnumerable<ActivePlayer> FindAll()
        {
            List<ActivePlayer> activePlayers = new List<ActivePlayer>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT ap.Id, ap.IdPlayer, ap.IdMatch, ap.NrPoints, ap.Type
            FROM ActivePlayers ap";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int activePlayerId = reader.GetInt32(reader.GetOrdinal("Id"));
                    int playerId = reader.GetInt32(reader.GetOrdinal("IdPlayer"));
                    int matchId = reader.GetInt32(reader.GetOrdinal("IdMatch"));
                    int nrPoints = reader.GetInt32(reader.GetOrdinal("NrPoints"));
                    string type = reader.GetString(reader.GetOrdinal("Type"));

                    activePlayers.Add(new ActivePlayer(activePlayerId, playerId, matchId, nrPoints, type));
                }
            }
            return activePlayers;
        }


        public ActivePlayer Save(ActivePlayer entity)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            INSERT INTO ActivePlayers (Id, IdPlayer, IdMatch, NrPoints, Type)
            VALUES (@id, @idPlayer, @idMatch, @nrPoints, @type)";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", entity.Id);
                command.Parameters.AddWithValue("@idPlayer", entity.IdPlayer);
                command.Parameters.AddWithValue("@idMatch", entity.IdMatch);
                command.Parameters.AddWithValue("@nrPoints", entity.NrPoints);
                command.Parameters.AddWithValue("@type", entity.Type);

                command.ExecuteNonQuery();
            }
            return entity;
        }


        public bool Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM ActivePlayers WHERE Id = @id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

    }
}
