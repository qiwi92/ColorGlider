using UnityEngine;

namespace Assets.Scripts.Powerups
{
    public interface IPowerupEffect
    {
        void Activate(Color color);
        void Deactivate();
        bool IsActive();
        PowerupType GetPowerupType();

    }
}