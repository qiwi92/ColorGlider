using System.Collections.Generic;
using GlowGlider.Shared;

namespace GlowGlider.Server.Data
{
    public interface IHighScoreRepository
    {
        IReadOnlyList<HighScoreData> GetBestScores();
        IReadOnlyList<HighScoreData> GetScoresForPlayer(string playerId);
        void InsertScore(GameData data);
    }
}