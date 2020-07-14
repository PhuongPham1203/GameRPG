using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Quest")]
public class Quest : ScriptableObject
{
    public int id = 0;
    new public string name = "New Quest";
    
    [Header("Type Quest: 1-Main \t 2-Sub \t 3-Hiden")]
    [Range(1, 3)]
    public int type = 1;

    [Header("Status Quest: 0-Hiden \t 1-OnWay \t 2-Success")]
    [Range(0, 2)]
    public int statusQuest = 0;
    
    [TextArea]
    public string information = "New Quest information";

    [Space]
    [Header("Request")]
    public List<SourceItemSlot> listRequest;
    [Range(0, 9999)]
    public int numberTarget = 0;
    [Range(0, 9999)]
    public int numberCurrent = 0;

    [Space]
    [Header("Rewards")]
    public List<SourceItemSlot> listReward;
    [Range(0, 9999)]
    public int numberReward = 0;


}
