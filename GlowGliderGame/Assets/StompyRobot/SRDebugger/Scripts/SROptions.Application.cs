using System.ComponentModel;
using UnityEngine;

public partial class SROptions
{
    private const string ApplicationCategory = "Application";

    [Category(ApplicationCategory)]
    public void ClearSavegame()
    {
        SavegameService.Instance.ClearSavegame();
    }

    [Category(ApplicationCategory)]
    public string Guid => SavegameService.Instance.Guid;

    [Category(ApplicationCategory)]
    public string GraphicsTier => Graphics.activeTier.ToString();
}