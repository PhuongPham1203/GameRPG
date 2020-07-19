using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerOfGame : MonoBehaviour
{
    /*
    public static SceneManagerOfGame instance;
    public GameObject loadingScene;
    public GameObject canvasMainMenu;
    public GameObject camera;
    private void Awake()
    {
        instance = this;

    }

    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();
    public void LoadGame()
    {

        loadingScene.gameObject.SetActive(true);
        canvasMainMenu.gameObject.SetActive(false);

        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_PLAYER, LoadSceneMode.Additive));
        //sceneLoading.Add( SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_APOCALYPSE,LoadSceneMode.Additive) );
        //SceneManager.UnloadSceneAsync((int)SceneIndexes.SCENE_MANAGER,LoadSceneMode.Additive);

        StartCoroutine(GetSceneLoadProgress());

    }

    float totalSceneProgess;
    public IEnumerator GetSceneLoadProgress()
    {
        // ! Loading 0 -> 0.9 && Activation 0.9 -> 1

        for (int i = 0; i < sceneLoading.Count; i++)
        {

            while (!sceneLoading[i].isDone)
            {

                totalSceneProgess = 0;
                foreach (AsyncOperation operation in sceneLoading)
                {
                    totalSceneProgess += operation.progress;
                }

                totalSceneProgess = (totalSceneProgess/sceneLoading.Count)*100f;

                //bar.current = Mathf.RoundToInt(totalSceneProgess);

                Debug.Log(Mathf.RoundToInt(totalSceneProgess));


                yield return null;
            }
        }

        camera.gameObject.SetActive(false);
        loadingScene.gameObject.SetActive(false);





    }
    */

    /*
    public Image loadingBar;
    public GameObject canvasMainMenu;
    public GameObject cancasLoadingScene;
    public Text progressText;

    public void LoadLevel(int sceneIndex)
    {
        canvasMainMenu.gameObject.SetActive(false);
        cancasLoadingScene.gameObject.SetActive(true);

        StartCoroutine(loadAsynchronously(sceneIndex));
        Debug.Log("done 0");





    }

    IEnumerator loadAsynchronously(int sceneIndex)
    {
        // ! Loading 0 -> 0.9 && Activation 0.9 -> 1
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progress;
            progressText.text = (progress * 100f).ToString();
            Debug.Log(progress);

            yield return null;
        }
        Debug.Log("done");

    }

    */
}
