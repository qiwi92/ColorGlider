﻿using System.ComponentModel;
using UnityEngine;

public partial class SROptions
{
    private const string ApplicationCategory = "Application";

    [Category(ApplicationCategory)]
    public void ClearSavegame()
    {
        PlayerPrefs.DeleteAll();
    }
}