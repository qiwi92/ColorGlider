using Assets.Scripts.Powerups;
using UnityEngine;

public class SavegameService
{
    private static SavegameService _instance;
    public static SavegameService Instance => _instance ?? (_instance = new SavegameService());

    public void ClearSavegame()
    {
        PlayerPrefs.DeleteAll();
    }

    public bool HasUnlockedShop
    {
        get { return PlayerPrefs.GetInt("Shopunlocked") == 1; }
        set { PlayerPrefs.SetInt("Shopunlocked", value ? 1 : 0);}
    }

    public string Alias 
    {
        get { return PlayerPrefs.GetString("PlayerAlias"); }
        set { PlayerPrefs.SetString("PlayerAlias", value); }
    }

    public string Guid
    {
        get { return PlayerPrefs.GetString("Guid"); }
        set { PlayerPrefs.SetString("Guid",value); }
    }

    public bool SoundIsOn
    {
        get{  return PlayerPrefs.GetInt("SoundIsOn") == 1 || !PlayerPrefs.HasKey("SoundIsOn");}
        set { PlayerPrefs.SetInt("SoundIsOn", value ? 1 : 0); }
    }

    public int HighScore
    {
        get { return PlayerPrefs.GetInt("HighScore"); }
        set { PlayerPrefs.SetInt("HighScore", value); }
    }

    public int Money    
    {
        get { return PlayerPrefs.GetInt("Money"); }
        set { PlayerPrefs.SetInt("Money", value); ; }
    }


    public void SavePowerupLevel(PowerupType powerupType, int lvl)
    {
        PlayerPrefs.SetInt(powerupType.ToString(), lvl);
    }

    public int GetPowerupLevel(PowerupType powerupType)
    {
        return PlayerPrefs.GetInt(powerupType.ToString());
    }
}