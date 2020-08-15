using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Singleton
    public static QuestManager instance;
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

        QuestData allQuestData = SaveSystem.LoadQuest();

        if (allQuestData == null)
        {
            LoadDefaulQuest();
        }
        else
        {

            this.listAllQuest = allQuestData.listAllQuest;

        }


    }
    #endregion

    public List<Quest> listAllQuest;
    /*
    public List<Quest> listAllMainQuest;
    public List<Quest> listAllSubQuest;
    public List<Quest> listAllHidenQuest;
    public List<Quest> currentQuest;
    */

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadDefaulQuest()
    {
        foreach (Quest q in listAllQuest)
        {
            if (q == null) { continue; }

            q.statusQuest = StatusQuest.NotAble;

            foreach (DetailItemInQuest detail in q.listRequest)
            {
                if (detail == null) { continue; }

                detail.SetNumberCurrent(0);
            }
        }
    }
}
