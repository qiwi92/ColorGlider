using System.Collections.Generic;

namespace Highscore
{
    public class DummyHighScoreModel : IHighScoreModel
    {
        public IEnumerable<PlayerHighScore> HighScoresAroundPlayer => new[]
        {
            new PlayerHighScore{PlayerName = "Goku",  Score = 9001},
            new PlayerHighScore{PlayerName = "xXxHuntxXx",  Score = 2798},
            new PlayerHighScore{PlayerName = "DeineMudda",  Score = 2144},
            new PlayerHighScore{PlayerName = "Manfred",  Score = 1896, IsPlayer = true},
            new PlayerHighScore{PlayerName = "GliderLove",  Score = 1403},
            new PlayerHighScore{PlayerName = "Luftpumpe",  Score = 12},
        };
    }
}