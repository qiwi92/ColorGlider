using System.Collections.Generic;

namespace GlowGlider.Server.Controllers
{
    internal class MockHighScoreRepository : IHighScoreRepository
    {
        public IReadOnlyList<HighScoreData> GetBestScores()
        {
            return new[]
            {
                new HighScoreData {Rank=1, PlayerName = "RUBEN", Score = 121},
                new HighScoreData {Rank=2, PlayerName = "JOSH", Score = 69},
                new HighScoreData {Rank=3, PlayerName = "JERRY", Score = 40},
                new HighScoreData {Rank=4, PlayerName = "MAGI", Score = 32},
                new HighScoreData {Rank=5, PlayerName = "MARCEL", Score = -1},
            };
        }

        public IReadOnlyList<HighScoreData> GetScoresForPlayer(string playerName)
        {
            return new[]
            {
                new HighScoreData {Rank=1, PlayerName = "RUBEN", Score = 121},
                new HighScoreData {Rank=2, PlayerName = "JOSH", Score = 69},
                new HighScoreData {Rank=3, PlayerName = "JERRY", Score = 40},
                new HighScoreData {Rank=87, PlayerName = "MAGI", Score = 32},
                new HighScoreData {Rank = 88, PlayerName = playerName.ToUpperInvariant(), Score = 15},
                new HighScoreData {Rank=89, PlayerName = "MARCEL", Score = -1},
            };
        }

        public void InsertScore(string playerName, int score)
        {
            
        }
    }
}