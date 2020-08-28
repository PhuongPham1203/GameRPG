using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager instance;

    [Header("All Teleport In This Scene")]
    public List<TeleInformation> listTeleportAllScene;
    public GameObject teleportUI;

    private void Awake()
    {
        if (instance != null)
        {

            Debug.LogWarning("More than one instance of TeleportManager found!!!");
            Destroy(this);

            return;
        }
        instance = this;

        /*
        GameData gameData = SaveSystem.LoadGameData();

        if (gameData == null)
        {
            foreach (TeleInformation t in listTeleportAllScene)
            {
                
                t.statusTeleport = StatusTeleport.Disable;
            }

            this.listTeleportAllScene[0].statusTeleport = StatusTeleport.Activate;
            //Debug.Log(listTeleportAllScene);

            SaveSystem.SaveGameData(listTeleportAllScene);


            //PlayerPrefs.

        }
        else
        {
            Debug.Log("have file save gamedata.p2teamdata");

            gameData.GetListDataTeleport(this.listTeleportAllScene);

            //Debug.Log(listTeleportAllScene);
        }
        */

    }



    public void SavePointTeleport()
    {
        Debug.Log("Save Point Game");
        SaveSystem.SaveGameData(this.listTeleportAllScene);
    }

    public void LoadDefaulTeleport()
    {
        foreach (TeleInformation t in this.listTeleportAllScene)
        {

            t.statusTeleport = StatusTeleport.Disable;
        }

        this.listTeleportAllScene[0].statusTeleport = StatusTeleport.Activate;

    }

}
