using System;

namespace GlowGlider.Shared
{
    public class HighScoreData
    {
        public int Rank { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }

        public override string ToString()
        {
            return $"{nameof(Rank)}: {Rank}, {nameof(PlayerName)}: {PlayerName}, {nameof(Score)}: {Score}";
        }
    }
}