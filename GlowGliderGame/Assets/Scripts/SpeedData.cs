using UnityEngine;

namespace Assets.Scripts
{
    public class SpeedData
    {
        public float GetSpeed(int score, bool isBoosted)
        {
            var baseSpeed = Mathf.Clamp(3 + 0.1f * score, 0, 15);

            if (isBoosted)
            {
                return baseSpeed + 5.0f;
            }

            return baseSpeed;
        }
    }
}