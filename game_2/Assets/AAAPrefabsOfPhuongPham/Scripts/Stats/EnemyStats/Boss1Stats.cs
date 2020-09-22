using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Stats : CharacterStats
{

    [Header("Phase Boss Stats")]
    public PhaseBossStats[] phaseBossStats;

    private void Start()
    {
        this.ResetAllCurrentAndMaxValue(this.phaseBossStats[0].HP,0, this.phaseBossStats[0].Posture);
    }

}

[System.Serializable]

public class PhaseBossStats
{
    public string name = "Phase";
    public int HP = 100;
    public int Posture = 100;
}