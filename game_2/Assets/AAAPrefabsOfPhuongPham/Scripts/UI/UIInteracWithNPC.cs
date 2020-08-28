using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteracWithNPC : MonoBehaviour
{

    public GameObject uiButtonTalk;
    public GameObject uiButtonQuest;
    public GameObject uiButtonStore;

    public GameObject uiTalk;
    public GameObject uiQuest;

    [Header("For Quest")]
    public Text questTitle;
    public Text questDescription;

    public GameObject prefabDetalItemQuest;

    public Transform parentRequests;
    public Transform parentRewards;

    public Quest questCanAccept;

    public GameObject buttonAccept;
    public GameObject buttonDone;


    public void SetOpen(bool openbuttonTalk, bool openbuttonQuest ,bool openButtonStore)
    {
        this.uiButtonTalk.SetActive(openbuttonTalk);
        this.uiButtonQuest.SetActive(openbuttonQuest);
        this.uiButtonStore.SetActive(openButtonStore);
            
    }

    public void SetInformationQuest(Quest quest)
    {
        if(quest == null)
        {
            return;
        }
        questCanAccept = quest;

        this.questTitle.text = quest.nameQuest;
        this.questDescription.text = quest.information;

        //bool activateAccept = false;
        int numberItemNotDone = 0;
        // Delete all old request and Add request
        for(int i = 0; i<parentRequests.childCount;i++)//delete old request
        {
            Destroy(parentRequests.GetChild(i).gameObject);
        }
        foreach(DetailItemInQuest detailItem in quest.listRequest)
        {
            GameObject detail = Instantiate(prefabDetalItemQuest,parentRequests);
            QuestDetailItem questDetailItem = detail.GetComponent<QuestDetailItem>();
            
            questDetailItem.numberCurrentAndTarget.text = detailItem.GetNumberCurrentItem()+" / "+detailItem.numberTarget;
            questDetailItem.nameItem.text = detailItem.item.nameItem;

            if (!detailItem.IsDone())// not done
            {
                numberItemNotDone++;
            }
        }
        
        if(quest.statusQuest == StatusQuest.NotAble)
        {
            buttonAccept.SetActive(true);
            buttonDone.SetActive(false);
        }else if (quest.statusQuest == StatusQuest.OnWay)
        {
            buttonAccept.SetActive(false);
            if (numberItemNotDone==0)
            {
                buttonDone.SetActive(true);    
            }

        }

        

        // Delete all old reward and Add reward
        for (int i = 0; i < parentRewards.childCount; i++)//delete old request
        {
            Destroy(parentRewards.GetChild(i).gameObject);
        }
        foreach (DetailItemInQuest detailItem in quest.listReward)
        {
            GameObject detail = Instantiate(prefabDetalItemQuest, parentRewards);
            QuestDetailItem questDetailItem = detail.GetComponent<QuestDetailItem>();

            questDetailItem.numberCurrentAndTarget.text = detailItem.numberTarget.ToString();
            questDetailItem.nameItem.text = detailItem.item.nameItem;

        }

        
        

    }

    public void SetQuestActivate()
    {
        if (this.questCanAccept!=null)
        {
            this.questCanAccept.statusQuest = StatusQuest.OnWay;
        }


    }

    public void SetDoneQuestActivate()
    {
        if (this.questCanAccept != null)
        {
            this.questCanAccept.statusQuest = StatusQuest.Success;
        }
    }


}
