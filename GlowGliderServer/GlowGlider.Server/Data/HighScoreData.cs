﻿using System;

namespace GlowGlider.Server.Data
{
    public class HighScoreData
    {
        public int Rank { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
    }
}