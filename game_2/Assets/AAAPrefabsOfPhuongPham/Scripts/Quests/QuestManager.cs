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

        /*
        QuestData allQuestData = SaveSystem.LoadQuest();

        if (allQuestData == null)
        {
            Debug.Log("Load Defaul QuestData");
            LoadDefaulQuest();
        }
        else
        {

            this.listAllQuest = allQuestData.listAllQuest;

        }

        */
    }
    #endregion

    public List<Quest> listAllQuest;


    public void LoadDefaulQuest()
    {
        foreach (Quest q in listAllQuest)
        {
            if (q == null) { continue; }

            if (q.typeColectionQuest == TypeQuestColection.Hiden)
            {
                q.statusQuest = StatusQuest.OnWay;

            }
            else
            {
                q.statusQuest = StatusQuest.NotAble;

            }

            foreach (DetailItemInQuest detail in q.listRequest)
            {
                if (detail == null) { continue; }

                detail.SetNumberCurrent(0);
            }
        }
    }

    public void TriggerQuestManager(List<SourceItemSlot> items, TypeQuest tq)
    {/* Run all quest activate */
        Debug.Log("Run Trigger Quest in : " + this.name);

        switch (tq)
        {
            case TypeQuest.KillEnemy:

                foreach (Quest q in listAllQuest)
                {
                    // Get only quest activate
                    if (q.statusQuest == StatusQuest.OnWay || q.statusQuest == StatusQuest.Success)
                    {
                        foreach (SourceItemSlot sourceItemSlot in items)
                        {
                            foreach (DetailItemInQuest detailItemInQuest in q.listRequest)
                            {
                                if (sourceItemSlot == detailItemInQuest.item)
                                {
                                    Debug.Log(q.nameQuest+" add item : "+ detailItemInQuest.item.nameItem+" : "+ detailItemInQuest.AddItem(1));
                                    
                                }
                            }

                        }



                    }

                    // For Dev Debug Only
                    if (q.statusQuest == StatusQuest.OnWay)
                    {
                        int requestNotDone = 0;
                        foreach (DetailItemInQuest detailItemInQuest in q.listRequest)
                        {
                            if (!detailItemInQuest.IsDone())
                            {

                                requestNotDone++;
                            }
                        }
                        if (requestNotDone == 0)
                        {
                            Debug.Log("Done Quest" + q.nameQuest);
                        }

                    }
                }
                break;
            // end case TypeQuest.KillEnemy
            case TypeQuest.Collect:

                

                // end case TypeQuest.Collect
                break;


        }



    }


}
