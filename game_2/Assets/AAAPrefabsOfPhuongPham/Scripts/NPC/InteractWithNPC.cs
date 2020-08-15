using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithNPC : Interactable
{
    //public List<Quest> listQuest;

    public Quest currentQuest;


    public override void Interact()
    {
        //base.Interact();
        Debug.Log("Interac with npc");

        List<Quest> all = QuestManager.instance.listAllQuest;

        foreach(Quest q in all)
        {
            q.statusQuest = StatusQuest.OnWay;
        }

        foreach (Quest q in all)
        {
            Debug.Log(q.statusQuest);
        }


    }

}
