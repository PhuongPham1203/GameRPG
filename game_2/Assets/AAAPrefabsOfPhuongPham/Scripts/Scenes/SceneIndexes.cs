using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Scene Index", menuName = "Inventory/Scene Index")]

public class SceneIndexes : ScriptableObject
{
    /*
    SCENE_MANAGER = 0,
    SCENE_PLAYER = 1,
    SCENE_APOCALYPSE = 2,
    SCENE_CITY = 3,
    SCENE_CLOUDFOREST = 4,
    SCENE_HELL = 5
    */
    public int sceneId;
    public string nameScene;
    [TextArea]
    public string information = "New Item information";

}