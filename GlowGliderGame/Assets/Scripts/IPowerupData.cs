namespace Assets.Scripts
{
    public interface IPowerupData
    {
        float GetSpawnChance(int level, float durationInSec);
        float GetActiveDuration(int level);
        float GetCost(int level);
        string GetName();
    }
}