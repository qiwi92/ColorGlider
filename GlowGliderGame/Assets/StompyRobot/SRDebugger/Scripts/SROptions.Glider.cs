using System.ComponentModel;

public partial class SROptions
{
    public static bool InvincibilityCheatActive;

    [Category("Glider")]
    public bool Invincibility
    {
        get { return InvincibilityCheatActive; }
        set
        {
            InvincibilityCheatActive = value;
            
        }
    }
}