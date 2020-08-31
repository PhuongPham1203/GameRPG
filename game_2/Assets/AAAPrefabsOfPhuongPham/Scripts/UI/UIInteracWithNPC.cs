using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteracWithNPC : MonoBehaviour
{
    public InteractWithNPC npcInteractWith;

    public GameObject uiButtonTalk;
    public GameObject uiButtonQuest;
    public GameObject uiButtonStore;

    [Header("List Talk")]
    public GameObject uiParentListTalk;
    public GameObject childTalkHeader;
    public DialogManager dialogManager;

    public GameObject uiParentListQuest;

    [Header("For Quest")]
    public Text questTitle;
    public Text questDescription;

    public GameObject prefabDetalItemQuest;

    public Transform parentRequests;
    public Transform parentRewards;

    public Quest questCanAccept;

    public GameObject buttonAccept;
    public GameObject buttonDone;


    public void SetOpen(bool openbuttonTalk, bool openbuttonQuest, bool openButtonStore)
    {
        this.uiButtonTalk.SetActive(openbuttonTalk);
        this.uiButtonQuest.SetActive(openbuttonQuest);
        this.uiButtonStore.SetActive(openButtonStore);

    }

    public void SetInformationQuest(Quest quest)
    {
        if (quest == null)
        {
            return;
        }
        questCanAccept = quest;

        this.questTitle.text = quest.nameQuest;
        this.questDescription.text = quest.information;

        //bool activateAccept = false;
        int numberItemNotDone = 0;
        // Delete all old request and Add request
        for (int i = 0; i < parentRequests.childCount; i++)//delete old request
        {
            Destroy(parentRequests.GetChild(i).gameObject);
        }
        Debug.Log(quest.listRequest.Count);
        foreach (DetailItemInQuest detailItem in quest.listRequest)
        {
            GameObject detail = Instantiate(prefabDetalItemQuest, parentRequests);
            QuestDetailItem questDetailItem = detail.GetComponent<QuestDetailItem>();

            questDetailItem.numberCurrentAndTarget.text = detailItem.GetNumberCurrentItem() + " / " + detailItem.numberTarget;
            questDetailItem.nameItem.text = detailItem.item.nameItem;

            if (!detailItem.IsDone())// not done
            {
                numberItemNotDone++;
            }
        }

        if (quest.statusQuest == StatusQuest.NotAble)
        {
            buttonAccept.SetActive(true);
            buttonDone.SetActive(false);
        }
        else if (quest.statusQuest == StatusQuest.OnWay)
        {
            buttonAccept.SetActive(false);
            if (numberItemNotDone == 0)
            {
                buttonDone.SetActive(true);
            }
            else
            {
                buttonDone.SetActive(false);
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

    public void OpenQuest()
    {
        if (this.questCanAccept.statusQuest == StatusQuest.NotAble)
        {
            if (this.questCanAccept.dialogBefore != null)
            {
                UIInteracWithNPC uIInteracWithNPC = MenuController.instance.uiInteracWithNPC.GetComponent<UIInteracWithNPC>();
                DialogManager dialogManager = uIInteracWithNPC.dialogManager;

                dialogManager.gameObject.SetActive(true);

                dialogManager.StartConversation(this.questCanAccept.dialogBefore, TypeTalk.QuestTalkBefore);

            }
            else
            {
                this.transform.GetChild(2).gameObject.SetActive(true);// Window (Quest)

            }

        }
        else if (this.questCanAccept.statusQuest == StatusQuest.OnWay)
        {
            this.transform.GetChild(2).gameObject.SetActive(true);// Window (Quest)
        }
    }

    public void SetQuestActivate()
    {
        if (this.questCanAccept != null)
        {
            this.questCanAccept.statusQuest = StatusQuest.OnWay;
        }


    }

    public void SetDoneQuestActivate()
    {
        if (this.questCanAccept != null)
        {
            this.questCanAccept.statusQuest = StatusQuest.Success;
            if (this.questCanAccept.dialogAfter != null)
            {
                UIInteracWithNPC uIInteracWithNPC = MenuController.instance.uiInteracWithNPC.GetComponent<UIInteracWithNPC>();
                DialogManager dialogManager = uIInteracWithNPC.dialogManager;

                dialogManager.gameObject.SetActive(true);

                dialogManager.StartConversation(this.questCanAccept.dialogAfter, TypeTalk.QuestTalkAfter);

            }
        }
    }

    public void GetListTalkActivate()
    {
        if (npcInteractWith != null)
        {
            List<NormalTalk> listActivate = npcInteractWith.GetListNormalTalkActivate();

            for (int i = 0; i < uiParentListTalk.transform.childCount; i++)
            {
                Destroy(uiParentListTalk.transform.GetChild(i).gameObject);
            }

            foreach (NormalTalk nt in listActivate)
            {
                GameObject childHeader = Instantiate(childTalkHeader, uiParentListTalk.transform);

                //childHeader.GetComponent<Toggle>().group = childHeader.GetComponentInParent<ToggleGroup>();

                childHeader.GetComponentInChildren<Text>().text = nt.dialog.nameDialogEN;
                TalkHeader th = childHeader.GetComponent<TalkHeader>();
                th.dialog = nt.dialog;

            }


        }
    }
}
