using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Quest")]
public class Quest : ScriptableObject
{
    public string nameQuest = "New Quest";
    //public string nameQuestEN = "New Quest";
    //public string nameQuestVI = "New Quest";
    
    [Header("Type Quest: 1-Main \t 2-Sub \t 3-Hiden")]
    //[Range(1, 3)]
    public TypeQuestColection typeColectionQuest = TypeQuestColection.Main;

    [Header("Status Quest: 0-NotAble \t 1-OnWay \t 2-Success")]
    //[Range(0, 2)]
    public StatusQuest statusQuest = StatusQuest.NotAble;
    
    [TextArea]
    public string information = "New Quest information";
    //public string informationEN = "New Quest information";
    //public string informationVI = "New Quest information";
   

    [Header("Dialog before Accept")]
    public Dialog dialogBefore;
    [Header("Dialog after Done")]
    public Dialog dialogAfter;


    [Space]
    [Header("Request")]
    public List<DetailItemInQuest> listRequest;


    [Space]
    [Header("Rewards")]
    public List<DetailItemInQuest> listReward;


}


public enum TypeQuestColection { Main, Sub, Hiden }
public enum StatusQuest { NotAble, OnWay, Success, Fail }
public enum TypeQuest { Collect, KillEnemy, Transformers , Escape, Defend }
