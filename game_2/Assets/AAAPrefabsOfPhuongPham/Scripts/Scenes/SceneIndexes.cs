using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    public int sceneBuiltId;
    //public Scene scene;
    public string nameScene;
    [TextArea]
    public string information = "New Item information";

    public int GetSceneBuiltID()
    {
        return sceneBuiltId;
    }
}