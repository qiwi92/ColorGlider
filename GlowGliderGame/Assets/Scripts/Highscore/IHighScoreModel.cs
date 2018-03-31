using System.Collections.Generic;
using GlowGlider.Shared;

namespace Highscore
{
    public interface IHighScoreModel
    {
        IEnumerable<PlayerHighScore> HighScoresAroundPlayer { get; }
        void UploadHighScore(int highScore, string alias);
    }
}