using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class QuestHeaderUI : MonoBehaviour
{
    private Quest questKeep;
    public void OpenQuest()
    {
        MenuController.instance.ShowQuestInMenu(this.questKeep);
    }
    public void SetInforQuest(Quest quest)
    {
        this.questKeep = quest;
    }
}
