using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerOfGame : MonoBehaviour
{
    public static SceneManagerOfGame instance;
    public GameObject loadingScene;
    public GameObject canvasMainMenu;
    private void Awake()
    {
        instance = this;
        
    }

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();
    public void LoadGame(){
        loadingScene.gameObject.SetActive(true);
        canvasMainMenu.gameObject.SetActive(false);

        sceneLoading.Add( SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_PLAYER,LoadSceneMode.Additive) );
        //sceneLoading.Add( SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_APOCALYPSE,LoadSceneMode.Additive) );
        //SceneManager.UnloadSceneAsync((int)SceneIndexes.SCENE_MANAGER,LoadSceneMode.Additive);

        
        StartCoro

    }

    public IEnumerator GetSceneLoadProgress(){

    }
}
