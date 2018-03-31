using System.Collections.Generic;
using GlowGlider.Shared;

namespace Highscore
{
    public interface IHighScoreModel
    {
        IReadOnlyList<PlayerHighScore> RelevantHighScores { get; }
        void UploadHighScore(int highScore, string alias);
    }
}