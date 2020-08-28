using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static bool isGamePlaused = false;

    [Header("For main menu")]
    public GameObject pauseMenu;
    public GameObject teleportUI;

    [Header("For NPC")]
    public GameObject uiInteracWithNPC;

    public static MenuController instance;
    #region Singleton

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        //SetQualityLevels(5);
        TeleportManager.instance.teleportUI = teleportUI;

        /*
        Button[] allButton = this.GetComponentsInChildren<Button>();
        
        foreach(Button b in allButton)
        {
            b.onClick.AddListener(()=>AudioManager.instance.PlaySoundOfPlayer("ButtonClick"));
        }
        */
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

    public void SavePointGame()
    {
        TeleportManager.instance.SavePointTeleport();
    }

    public void ResetPointGame()
    {

    }

    public void OpenUIInteracWithNPC(bool open)
    {
        this.uiInteracWithNPC.SetActive(open);
    }

    

}
