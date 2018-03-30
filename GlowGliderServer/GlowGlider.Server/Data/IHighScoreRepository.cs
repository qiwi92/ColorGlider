using System.Collections.Generic;

namespace GlowGlider.Server.Data
{
    public interface IHighScoreRepository
    {
        IReadOnlyList<HighScoreData> GetBestScores();
        IReadOnlyList<HighScoreData> GetScoresForPlayer(string playerId);
        void InsertScore(GameData data);
    }
}