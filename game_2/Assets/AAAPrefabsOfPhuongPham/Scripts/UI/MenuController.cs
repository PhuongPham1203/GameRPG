using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static bool isGamePlaused = false;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    private void Start()
    {
        //SetQualityLevels(5);
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
        Time.timeScale = 1f;
        isGamePlaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePlaused = true;

    }
    public void SetQualityLevels(int level)
    {
        if (QualitySettings.GetQualityLevel() == level)
        {
            return;
        }

        QualitySettings.SetQualityLevel(level, true);

        Debug.Log(level);

    }
}
