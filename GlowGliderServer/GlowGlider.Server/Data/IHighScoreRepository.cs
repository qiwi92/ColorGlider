using System.Collections.Generic;

namespace GlowGlider.Server.Controllers
{
    public interface IHighScoreRepository
    {
        IReadOnlyList<HighScoreData> GetBestScores();
        IReadOnlyList<HighScoreData> GetScoresForPlayer(string playerName);
        void InsertScore(string playerName, int score);
    }
}