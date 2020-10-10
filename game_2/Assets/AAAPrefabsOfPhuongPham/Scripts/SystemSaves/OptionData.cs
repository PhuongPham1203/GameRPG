using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class OptionData
{
    public bool isMuteMusic;
    public float volumeMusic; // -80 -> 20 

    public bool isMuteSfx;
    public float volumeSfx; // -80 -> 20

    public int language; // 0:EN - 1:VI - 2:Xcode

    public int graphic; // 0 -> 5
    public OptionData()
    {

    }
}
