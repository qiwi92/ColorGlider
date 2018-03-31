namespace Assets.Scripts.Powerups
{
    public interface IPowerup
    {
        void Activate();
        void Deactivate();
        bool IsActive();
        PowerupType GetPowerupType();

    }
}