namespace Assets.Scripts
{
    public interface IPowerupData
    {
        float GetSpawnChance(int level, float durationInSec);
        float GetActiveDuration(int level);
        int GetCost(int level);
        string GetName();
    }
}