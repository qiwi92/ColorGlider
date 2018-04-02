using UnityEngine;

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

            if (level == 0)
            {
                return -1;
            }

            return Mathf.Clamp(0.05f, 0, 0.5f);
        }

        public float GetActiveDuration(int level)
        {
            return 3.0f + level * 0.5f;
        }

        public int GetCost(int level)
        {
            return Mathf.RoundToInt(200 * Mathf.Pow(1.1f, level));
        }

        public string GetName()
        {
            return "Speed Boost";
        }
    }
}