using UnityEngine;

namespace Assets.Scripts
{
    public class ShieldData : IPowerupData
    {
        public float GetSpawnChance(int level)
        {
            return Mathf.Clamp(level * 0.1f , 0 , 0.5f);
        }

        public float GetCost(int level)
        {
            return 20*Mathf.Pow(1.1f, level);
        }
    }
}