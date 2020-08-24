using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class GameData
{
    // Because c# can save file binary of obj unity so we need change to c# 

    private List<TeleInfor> listTeleportAllScene = new List<TeleInfor>();
    public GameData()
    {

    }

    public GameData(List<TeleInformation> listT)
    {
        //this.listTeleportAllScene = listT;
        SetListTeleport(listT);
    }

    public void GetListDataTeleport(List<TeleInformation> listT)
    {

        foreach (TeleInformation t1 in listT)
        {
            foreach (TeleInfor t2 in this.listTeleportAllScene)
            {
                if (t1.name == t2.GetName())
                {
                    t1.statusTeleport = t2.GetStatus();
                    break;
                }
            }
        }


        //return this.listTeleportAllScene;
    }

    public void SetListTeleport(List<TeleInformation> listT)
    {
        // t.name is name of ScriptableObject , not t.nameTeleport
        foreach (TeleInformation t in listT)
        {
            this.listTeleportAllScene.Add(new TeleInfor(t.name, t.statusTeleport));
        }

        //this.listTeleportAllScene = listT;
    }

}

[System.Serializable]
public class TeleInfor
{
    private string nameTeleInformation;
    //private int sceneID;

    private StatusTeleport statusTeleport;
    public TeleInfor()
    {

    }
    public TeleInfor(string n, StatusTeleport status)
    {
        this.nameTeleInformation = n;
        //this.sceneID = id;
        this.statusTeleport = status;
    }

    public string GetName()
    {
        return this.nameTeleInformation;
    }
    /*
    public int GetSceneID()
    {
        return this.sceneID;
    }
    */
    public StatusTeleport GetStatus()
    {
        return this.statusTeleport;
    }
}

