using UnityEngine;

namespace Assets.Scripts
{
    public class ShieldData : IPowerupData
    {
        public float GetSpawnChance(int level, float durationInSec)
        {
            if (durationInSec < 4)
            {
                return 0;
            }

            return Mathf.Clamp(0.1f, 0, 0.5f);
        }

        public float GetActiveDuration(int level)
        {
            return 2.0f + level * 0.5f;
        }

        public float GetCost(int level)
        {
            return 20*Mathf.Pow(1.1f, level);
        }

        public string GetName()
        {
            return "Shield";
        }
    }
}