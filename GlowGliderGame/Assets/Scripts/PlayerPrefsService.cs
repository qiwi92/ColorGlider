using UnityEngine;

public class PlayerPrefsService
{
    private static PlayerPrefsService _instance;
    public static PlayerPrefsService Instance => _instance ?? (_instance = new PlayerPrefsService());

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
}