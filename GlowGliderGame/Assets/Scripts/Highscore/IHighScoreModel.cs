using System.Collections.Generic;

namespace Highscore
{
    public interface IHighScoreModel
    {
        IEnumerable<PlayerHighScore> HighScoresAroundPlayer { get; }
    }
}