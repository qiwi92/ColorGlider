﻿namespace Assets.Scripts
{
    public interface IPowerup
    {
        void Activate();
        void Deactivate();
        bool IsActive();
        PowerupType GetPowerupType();

    }
}