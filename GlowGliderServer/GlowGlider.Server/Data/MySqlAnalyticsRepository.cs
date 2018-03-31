using System.Threading.Tasks;
using GlowGlider.Shared;
using MySql.Data.MySqlClient;

namespace GlowGlider.Server.Data
{
    internal class MySqlAnalyticsRepository : MySqlRepositoryBase, IAnalyticsRepository
    {
        public async Task StoreDataAsync(AnalyticsData data)
        {
            OpenConnectionIfNeeded();

            const string commandString =
                "INSERT INTO `analytics` " +
                "   (`PlayerId`, `Score`, `GameDuration`) " +
                "VALUES " +
                "   (@playerId, @score, @duration) ";

            var command = new MySqlCommand(commandString, Connection);
            command.Parameters.AddWithValue("playerId", data.PlayerId);
            command.Parameters.AddWithValue("score", data.Score);
            command.Parameters.AddWithValue("duration", data.GameDuration);

            await command.ExecuteNonQueryAsync();
        }
    }
}