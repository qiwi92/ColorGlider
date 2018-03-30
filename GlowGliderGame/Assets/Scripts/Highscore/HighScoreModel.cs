using System.Collections.Generic;

namespace Highscore
{
    public class HighScoreModel : IHighScoreModel
    {
        public IEnumerable<PlayerHighScore> HighScoresAroundPlayer
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}