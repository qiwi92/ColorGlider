namespace Assets.Scripts
{
    public interface IPowerup
    {
        void Activate();
        bool IsActive();
        PowerupType GetPowerupType();

    }
}