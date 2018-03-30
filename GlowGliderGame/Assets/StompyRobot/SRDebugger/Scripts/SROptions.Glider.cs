public partial class SROptions
{
    public static bool InvincibilityCheatActive;
    public bool Invincibility
    {
        get { return InvincibilityCheatActive; }
        set
        {
            InvincibilityCheatActive = value;
            
        }
    }
}