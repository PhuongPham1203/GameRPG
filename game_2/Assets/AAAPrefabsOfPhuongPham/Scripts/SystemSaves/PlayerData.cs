using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int baseMaxHP;
    public int baseCurrentAttackDame;
    public int baseMaxPoseture;
    public int baseCurrentDefend;

    public int baseMoney;
    public int baseRealMoney;
    public int baseSoul;

    public int baseLevel;
    public int baseExpNow;
    public int baseExpToLevelUp;

    public float[] positon;
    public int sceneIndexCurrent;

    public PlayerData(PlayerStats playerStats)
    {
        baseMaxHP = playerStats.maxHP;
        baseCurrentAttackDame = playerStats.currentAttackDame;
        baseMaxPoseture = playerStats.maxPosture;
        baseCurrentDefend = playerStats.currentDefend;

        baseMoney = playerStats.money;
        baseRealMoney = playerStats.realMoney;
        baseSoul = playerStats.soul;

        baseLevel = playerStats.level;
        baseExpNow = playerStats.expNow;
        baseExpToLevelUp = playerStats.expToLevelUp;

        positon = new float[3];
        positon[0] = playerStats.potisionCurrenNearest.x;
        positon[1] = playerStats.potisionCurrenNearest.y;
        positon[2] = playerStats.potisionCurrenNearest.z;

        sceneIndexCurrent = playerStats.sceneIndex;
    }

}
