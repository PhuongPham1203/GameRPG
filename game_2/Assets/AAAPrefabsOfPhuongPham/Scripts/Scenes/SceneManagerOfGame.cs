﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SceneManagerOfGame : MonoBehaviour
{

    public static SceneManagerOfGame instance;
    public GameObject loadingScene;
    public GameObject canvasMainMenu;
    public GameObject optionMenu;
    public GameObject cameraInScene;
    public Slider sliderBar;
    public Text numberPercen;

    public AudioMixer audioMixer;

    public Animator animatorIcon;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        //SaveSystem.LoadGame();
    }

    [Header("List Scene Index")]
    public List<SceneIndexes> listSceneIndex;
    public List<SceneIndexes> listSceneIndex2;
    List<AsyncOperation> sceneLoading = new List<AsyncOperation>();
    public void LoadGame()
    {

        loadingScene.gameObject.SetActive(true);
        canvasMainMenu.gameObject.SetActive(false);

        foreach (SceneIndexes scene in listSceneIndex)
        {
            if (scene != null)
            {
                sceneLoading.Add(SceneManager.LoadSceneAsync(scene.sceneId, LoadSceneMode.Additive));

            }
        }

        foreach (AsyncOperation async in sceneLoading)
        {
            async.allowSceneActivation = false;
        }

        /*
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_PLAYER, LoadSceneMode.Additive));
        //sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_APOCALYPSE, LoadSceneMode.Additive));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_CITY, LoadSceneMode.Additive));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_CLOUDFOREST, LoadSceneMode.Additive));
        //SceneManager.UnloadSceneAsync((int)SceneIndexes.SCENE_MANAGER,LoadSceneMode.Additive);
        */
        StartCoroutine(GetSceneLoadProgress());
        Debug.Log("point");
        animatorIcon.SetInteger("type", 2);

    }

    public void LoadGameTestContinue()
    {
        loadingScene.gameObject.SetActive(true);
        canvasMainMenu.gameObject.SetActive(false);

        foreach (SceneIndexes scene in listSceneIndex2)
        {
            if (scene != null)
            {
                sceneLoading.Add(SceneManager.LoadSceneAsync(scene.sceneId, LoadSceneMode.Additive));

            }
        }

        foreach (AsyncOperation async in sceneLoading)
        {
            async.allowSceneActivation = false;
        }

        /*
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_PLAYER, LoadSceneMode.Additive));
        //sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_APOCALYPSE, LoadSceneMode.Additive));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_CITY, LoadSceneMode.Additive));
        sceneLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.SCENE_CLOUDFOREST, LoadSceneMode.Additive));
        //SceneManager.UnloadSceneAsync((int)SceneIndexes.SCENE_MANAGER,LoadSceneMode.Additive);
        */
        StartCoroutine(GetSceneLoadProgress());
        Debug.Log("point");
        animatorIcon.SetInteger("type", 2);
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
                    /*
                    if(operation.allowSceneActivation == false && operation.progress >= 0.9f)
                    {
                        operation.allowSceneActivation = true;
                    }
                    */
                    totalSceneProgess += (operation.progress / sceneLoading.Count);
                }
                Debug.Log(totalSceneProgess);
                sliderBar.value = totalSceneProgess / 0.9f;

                totalSceneProgess = totalSceneProgess * 100f;
                totalSceneProgess = Mathf.RoundToInt(totalSceneProgess);
                numberPercen.text = totalSceneProgess.ToString();

                //Debug.Log(Mathf.RoundToInt(totalSceneProgess));
                Debug.Log(totalSceneProgess + " percent");

                if (totalSceneProgess >= 90)
                {
                    foreach (AsyncOperation async in sceneLoading)//Activate the Scene
                    {
                        if (!async.allowSceneActivation)
                        {
                            async.allowSceneActivation = true;

                        }
                    }
                }



                yield return null;
            }


        }

        Debug.Log("Done");



        animatorIcon.SetInteger("type", 0);
        cameraInScene.gameObject.SetActive(false);
        loadingScene.gameObject.SetActive(false);   

    }
    public void SetVolumeTheme(Slider number)
    {
        //AudioManager.instance.SetVolumeTheme(number);
        audioMixer.SetFloat("Volume Theme", number.value);
    }
    public void SetVolumeSFX(Slider number)
    {
        //AudioManager.instance.SetVolumeSFX(number);
        audioMixer.SetFloat("Volume SFX", number.value);
    }

    public void OpenOption(bool open)
    {
        optionMenu.gameObject.SetActive(open);
    }

}
