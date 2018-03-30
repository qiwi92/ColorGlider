using System;

namespace GlowGlider.Server.Data
{
    public class GameData
    {
        public Guid PlayerId { get; set; }
        public string PlayerAlias { get; set; }
        public int Score { get; set; }
    }
}