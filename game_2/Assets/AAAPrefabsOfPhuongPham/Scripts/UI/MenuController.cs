using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static bool isGamePlaused = false;

    [Header("For MainMenu")]
    public GameObject pauseMenu;
    public GameObject teleportUI;

    [Header("For Quest in MainMenu")]
    public Transform parentOfHeaderQuestMenu;
    public GameObject headerQuestMenu;

    public GameObject questInfor;
    public Text nameQuestInMenu;
    public Text inforQuestInMenu;
    public Transform parentRequestList;
    public Transform parentRewardList;
    public GameObject detailItemInQuest;

    [Header("For NPC")]
    public GameObject uiInteracWithNPC;

    [Header("For Items")]
    public GameObject uiInformationItem;

    public static MenuController instance;
    #region Singleton

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


    // For menu Quest

    public void UpdateHeaderTabsQuest()
    {
        int index_lang = PlayerPrefs.GetInt("_language_index", 0);

        // Delete all old Quest
        foreach (Transform child in this.parentOfHeaderQuestMenu)
        {
            Destroy(child.gameObject);
            //Debug.Log(child.name);
        }

        // Get all quest OnWay

        List<Quest> allQuest = QuestManager.instance.listAllQuest;
        List<Quest> questsOnWay = new List<Quest>();
        foreach(Quest q in allQuest)
        {
            if(q.statusQuest == StatusQuest.OnWay && q.typeColectionQuest != TypeQuestColection.Hiden)
            {
                questsOnWay.Add(q);
            }
        }

        foreach (Quest q in questsOnWay)
        {
            GameObject g = Instantiate(this.headerQuestMenu,this.parentOfHeaderQuestMenu);
            g.GetComponent<Toggle>().group = g.transform.parent.GetComponent<ToggleGroup>();
            g.GetComponentInChildren<Text>().text = q.nameQuest[index_lang];

            g.GetComponent<QuestHeaderUI>().SetInforQuest(q);

        }
    }

    public void ShowQuestInMenu(Quest q)
    {
        int index_lang = PlayerPrefs.GetInt("_language_index", 0);

        this.nameQuestInMenu.text = q.nameQuest[index_lang];
        this.inforQuestInMenu.text = q.information[index_lang];

        // request

        // Delete all old item in request
        foreach (Transform child in this.parentRequestList)
        {
            Destroy(child.gameObject);
        }

        foreach (DetailItemInQuest detailItem in q.listRequest)
        {
            GameObject detail = Instantiate(this.detailItemInQuest, this.parentRequestList);
            detail.GetComponent<QuestDetailItem>().numberCurrentAndTarget.text = detailItem.GetNumberCurrentItem().ToString()+" / "+ detailItem.numberTarget.ToString();
            detail.GetComponent<QuestDetailItem>().nameItem.text = detailItem.item.nameItem[index_lang];
        }


        // reward
        // Delete all old item in request
        foreach (Transform child in this.parentRewardList)
        {
            Destroy(child.gameObject);
        }
        foreach (DetailItemInQuest detailItem in q.listReward)
        {
            GameObject detail = Instantiate(this.detailItemInQuest, this.parentRewardList);
            detail.GetComponent<QuestDetailItem>().numberCurrentAndTarget.text = detailItem.GetNumberCurrentItem().ToString() + " / " + detailItem.numberTarget.ToString();
            detail.GetComponent<QuestDetailItem>().nameItem.text = detailItem.item.nameItem[index_lang];
        }

        this.questInfor.SetActive(true);

    }
}
