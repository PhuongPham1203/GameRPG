using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static bool isGamePlaused = false;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OpenMenu()
    {
        //Debug.Log("press");
        if (isGamePlaused)
        {

            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        //Time.timeScale = 1f;
        isGamePlaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        //Time.timeScale = 0f;
        isGamePlaused = true;

    }
}
