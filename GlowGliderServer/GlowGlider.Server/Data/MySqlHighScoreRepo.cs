﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace GlowGlider.Server.Data
{
    public class MySqlHighScoreRepo : IHighScoreRepository
    {
        private readonly MySqlConnection _connection;

        public MySqlHighScoreRepo()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Password = "LHpxTYQmWcY8kpgE",
                Database = "d029f449",
                UserID = "d029f449",
                Server = "unholyfist.de",
                AllowUserVariables = true,
            };

            _connection = new MySqlConnection(builder.GetConnectionString(true));
        }

        private void OpenConnectionIfNeeded()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public IReadOnlyList<HighScoreData> GetBestScores()
        {
            OpenConnectionIfNeeded();

            var command = GetSelectTopRanksCommand();
            var dataReader = command.ExecuteReader();

            var rankings = ReadRankingData(dataReader).ToList();

            return rankings;
        }
        
        public IReadOnlyList<HighScoreData> GetScoresForPlayer(string playerId)
        {
            OpenConnectionIfNeeded();

            var command = GetSelectRanksAroundPlayerCommand(playerId);

            var dataReader = command.ExecuteReader();

            var rankings = ReadRankingData(dataReader).ToList();

            return rankings;
        }

        public void InsertScore(GameData data)
        {
            OpenConnectionIfNeeded();

            var transaction = _connection.BeginTransaction();

            var command = GetInsertIntoRankingCommand(data);
            if (command.ExecuteNonQuery() == 1)
            {
                var updateCmd = GetUpdateRanksCommand();
                updateCmd.ExecuteNonQuery();
            }

            transaction.Commit();
        }

        private static IEnumerable<HighScoreData> ReadRankingData(MySqlDataReader dataReader)
        {
            while (dataReader.Read())
            {
                yield return new HighScoreData
                {
                    PlayerId = (Guid)dataReader["PlayerId"],
                    PlayerName = dataReader["PlayerAlias"] as string,
                    Score = (int)dataReader["Score"],
                    Rank = (int)dataReader["Rank"]
                };
            }
        }

        private MySqlCommand GetUpdateRanksCommand()
        {
            var cmdText = "SET @counter = 0; "+
                          "UPDATE `ranking` " +
                          "SET `Rank` = @counter := @counter + 1 " +
                          "ORDER BY `Score` DESC;";

            var command = new MySqlCommand(cmdText, _connection);
            

            return command;
        }

        private MySqlCommand GetSelectTopRanksCommand()
        {
            var cmdText = "SELECT * FROM `ranking` ORDER BY `Score` DESC LIMIT 10";
            var command = new MySqlCommand(cmdText, _connection);

            return command;
        }

        private MySqlCommand GetInsertIntoRankingCommand(GameData data)
        {
            var cmdText = "INSERT INTO `ranking` " +
                          "     (`PlayerId`, `PlayerAlias`, `Score`) " +
                          "VALUES " +
                          "     (@playerId, @playerAlias, @score) " +
                          "ON DUPLICATE KEY UPDATE " +
                          " `PlayerAlias` = @playerAlias, " +
                          " `Score` = @score";

            var command = new MySqlCommand(cmdText, _connection);
            command.Parameters.AddWithValue("playerId", data.PlayerId);
            command.Parameters.AddWithValue("playerAlias", data.PlayerAlias);
            command.Parameters.AddWithValue("score", data.Score);

            return command;
        }
        
        private MySqlCommand GetSelectRanksAroundPlayerCommand(string playerId)
        {
            var cmdText = "SELECT * FROM `ranking` " +
                          "WHERE `Rank` >= (SELECT `Rank` FROM `ranking` WHERE `PlayerId` = @playerId) - 5 " +
                          "ORDER BY `Score` DESC " +
                          "LIMIT 10";

            var command = new MySqlCommand(cmdText, _connection);
            command.Parameters.AddWithValue("playerId", playerId);

            return command;
        }
    }
}