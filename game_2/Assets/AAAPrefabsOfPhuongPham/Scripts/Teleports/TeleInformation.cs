using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Contexts;
using UnityEngine;


[CreateAssetMenu(fileName = "New Teleport Information", menuName = "Inventory/Teleport Information")]
public class TeleInformation : ScriptableObject
{
    [Header("position ID in Scene")]
    [SerializeField]
    private int positionID;
    [Header("Scene ID in Game")]
    [SerializeField]
    private int sceneID;
    public string nameTeleport = "New Teleport";
    public bool defaulTele = false;
    [Header("Scene Built Index in Game")]
    [SerializeField]
    private SceneIndexes sceneBuiltIndexes;


    [Header("Status Teleport: 0-Disable \t 1-Activate")]
    //[Range(1, 3)]
    public StatusTeleport statusTeleport = StatusTeleport.Disable;

    public SceneIndexes GetSceneIndexes()
    {
        return this.sceneBuiltIndexes;
    }

    public int GetPositionIDInScene()
    {
        return this.positionID;
    }

    public int GetSceneIDInGame()
    {
        return this.sceneID;
    }

}

public enum StatusTeleport { Disable, Activate }
