using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public float baseMaxHP;
    public int baseCurrentAttackDame;
    public float baseMaxPoseture;
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
        //baseCurrentDefend = playerStats.currentDefend;

        baseMoney = playerStats.money;
        baseRealMoney = playerStats.realMoney;
        baseSoul = playerStats.soul;

        baseLevel = playerStats.level;
        baseExpNow = playerStats.expNow;
        baseExpToLevelUp = playerStats.expToLevelUp;

        positon = new float[3];
        positon[0] = playerStats.potisionCurrenNearestNow.x;
        positon[1] = playerStats.potisionCurrenNearestNow.y;
        positon[2] = playerStats.potisionCurrenNearestNow.z;

        sceneIndexCurrent = playerStats.sceneIndex;
    }

}
