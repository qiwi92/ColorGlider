using UnityEngine;

namespace Assets.Scripts.Powerups
{
    public class Boost : MonoBehaviour, IPowerup
    {
        public void Activate()
        {
            throw new System.NotImplementedException();
        }

        public void Deactivate()
        {
            throw new System.NotImplementedException();
        }

        public bool IsActive()
        {
            throw new System.NotImplementedException();
        }

        public PowerupType GetPowerupType()
        {
            return PowerupType.Boost;
        }
    }
}