using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Teleport Information", menuName = "Inventory/Teleport Information")]
public class TeleInformation : ScriptableObject
{
    public string nameTeleport = "New Teleport";

    public SceneIndexes sceneIndexes;

    [Header("Type Quest: 1-Main \t 2-Sub \t 3-Hiden")]
    //[Range(1, 3)]
    public StatusTeleport statusTeleport = StatusTeleport.Disable;
}

public enum StatusTeleport { Disable, Activate }
