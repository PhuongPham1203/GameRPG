using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithNPC : Interactable
{
    [Header("Can Buy Item")]
    public bool canBuyItem = false;

    [Header("Quest Need Activate")]
    public Quest questNeedActivate;

    [Header("Current Quest")]
    public Quest currentQuestActivate;

    [Header("Normal Talk")]
    public List<NormalTalk> listNormalTalk = new List<NormalTalk>();

    [Header("List Quest")]
    public List<Quest> listQuestInThisNpc;

    
    public override void Interact()
    {
        Vector3 lookAt = transform.root.position;
        lookAt.y = PlayerManager.instance.player.transform.position.y;

        PlayerManager.instance.player.transform.LookAt(lookAt);

        // setup quest activate and need activate in this npc
        SetUpAllQuestInThisNPC();

        MenuController.instance.OpenUIInteracWithNPC(true);

        UIInteracWithNPC uinteracWithNPC = MenuController.instance.uiInteracWithNPC.GetComponent<UIInteracWithNPC>();

        bool talk=true, quest=false;

        if (currentQuestActivate == null)// dont have quest activate in this NPC
        {
            if(questNeedActivate != null)
            {

                quest = true;
                uinteracWithNPC.SetInformationQuest(questNeedActivate);

                //uinteracWithNPC.SetOpen(true, true, false);

            }
        }
        else if(currentQuestActivate != null) // have Quest Activate in this NPC
        {
            quest = true;
            uinteracWithNPC.SetInformationQuest(currentQuestActivate);

        }

        uinteracWithNPC.SetOpen(talk, quest,canBuyItem);

    }
    
    public void SetUpAllQuestInThisNPC()
    {
        if (this.currentQuestActivate == null)// dont have any quest activate in this npc
        {
            if(this.questNeedActivate == null) // dont have any quest need activate
            {
                foreach (Quest q in this.listQuestInThisNpc )
                {
                    if(q.statusQuest == StatusQuest.OnWay)
                    {
                        this.currentQuestActivate = q;
                        break;
                    }
                    else if(q.statusQuest == StatusQuest.NotAble)
                    {
                        this.questNeedActivate = q;
                        break;
                    }
                }
            }
            else // have quest need activate
            {
                if (this.questNeedActivate.statusQuest == StatusQuest.OnWay )
                {
                    this.currentQuestActivate = this.questNeedActivate;
                    this.questNeedActivate = null;
                }else if (this.questNeedActivate.statusQuest == StatusQuest.Fail)
                {
                    this.questNeedActivate = null;
                }
            }

        }else // have current quest activate
        {
            if(this.questNeedActivate != null) // because have current activate so delete questNeedActivate
            {
                this.questNeedActivate = null;
            }

            // Quest Fail or success so need delete it
            if(this.currentQuestActivate.statusQuest == StatusQuest.Fail || this.currentQuestActivate.statusQuest == StatusQuest.Success)
            {
                this.currentQuestActivate = null;
            }
        }
    }

}

[System.Serializable]
public class NormalTalk{

    [Header("Quest request and Dialog")]
    public Quest quest;
    public Dialog dialog;

    public NormalTalk()
    {

    }

}