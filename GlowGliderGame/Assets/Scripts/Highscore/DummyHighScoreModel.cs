using System.Collections.Generic;

namespace Highscore
{
    public class DummyHighScoreModel : IHighScoreModel
    {
        public IEnumerable<PlayerHighScore> HighScoresAroundPlayer => new[]
        {
            new PlayerHighScore{Name = "Goku",  Score = 9001},
            new PlayerHighScore{Name = "xXxHuntxXx",  Score = 2798},
            new PlayerHighScore{Name = "DeineMudda",  Score = 2144},
            new PlayerHighScore{Name = "Manfred",  Score = 1896},
            new PlayerHighScore{Name = "GliderLove",  Score = 1403},
            new PlayerHighScore{Name = "Luftpumpe",  Score = 12},
        };
    }
}