﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Quest")]
public class Quest : ScriptableObject
{
    public string nameQuest = "New Quest";
    
    [Header("Type Quest: 1-Main \t 2-Sub \t 3-Hiden")]
    //[Range(1, 3)]
    public TypeQuest typeQuest = TypeQuest.Main;

    [Header("Status Quest: 0-NotAble \t 1-OnWay \t 2-Success")]
    //[Range(0, 2)]
    public StatusQuest statusQuest = StatusQuest.NotAble;
    
    [TextArea]
    public string information = "New Quest information";

    [Space]
    [Header("Request")]
    public List<DetailItemInQuest> listRequest;


    [Space]
    [Header("Rewards")]
    public List<DetailItemInQuest> listReward;


}


public enum TypeQuest { Main, Sub, Hiden }
public enum StatusQuest { NotAble, OnWay, Success }
