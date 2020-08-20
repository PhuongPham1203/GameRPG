using Aura2API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class QuestData
{

    public List<Quest> listAllQuest;
    /*
    public List<Quest> listAllMainQuest;
    public List<Quest> listAllSubQuest;
    public List<Quest> listAllHidenQuest;
    public List<Quest> currentQuest;
    */

    public QuestData()
    {

    }

    public QuestData(List<Quest> listQ)
    {
        this.listAllQuest = listQ;
    }

    /*
    public static void SaveDataQuest()
    {

    }

    public static void LoadDataQuest()
    {

    }

    public static void DeleteDataQuest()
    {

    }
    */
}
