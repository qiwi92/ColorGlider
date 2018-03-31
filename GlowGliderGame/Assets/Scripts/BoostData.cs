﻿using UnityEngine;

namespace Assets.Scripts
{
    public class BoostData : IPowerupData
    {
        public float GetSpawnChance(int level, float durationInSec)
        {
            if (durationInSec < 4)
            {
                return 0;
            }

            return Mathf.Clamp(level * 0.01f, 0, 0.5f);
        }

        public float GetCost(int level)
        {
            return 200 * Mathf.Pow(1.1f, level);
        }

        public string GetName()
        {
            return "Speed Boost";
        }
    }
}