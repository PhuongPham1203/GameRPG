using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Accessibility;
using System;

public class TeleportTabsUI : MonoBehaviour
{
    public GameObject allHeaderListMap;
    public GameObject[] allListMap;

    public bool needUpdate = true;
    [Header("Update for Scene or Teleport_In_Scene")]
    public bool isForScene = true;
    private void Awake()
    {
        allListMap = GetComponent<TabsController>().allTabs;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("1child count"+ allHeaderListMap.transform.childCount);    
        //Debug.Log("2child count"+ allListMap.transform.childCount);    
    }

    private void OnEnable()
    {
        if (needUpdate)
        {
            //Debug.Log("Update : "+name);
            List<TeleInformation> listTeleportAllScene = TeleportManager.instance.listTeleportAllScene;


            SetHeaderAndTabs(listTeleportAllScene, allHeaderListMap, allListMap);

            needUpdate = false;
        }




    }

    private void SetHeaderAndTabs(List<TeleInformation> listTeleportAllScene, GameObject allHeaderLM, GameObject[] allLM)
    {
        List<TeleInformation> listAllTele_Or_Scene_Activate = new List<TeleInformation>();
        if (isForScene) //  for headers and tabs scene
        {
            int i = -1;
            foreach (TeleInformation t in listTeleportAllScene)
            {

                if (t.statusTeleport == StatusTeleport.Activate)
                {
                    if (i < t.GetSceneIDInGame())
                    {
                        listAllTele_Or_Scene_Activate.Add(t);
                        //Debug.Log(t.name);
                        i = t.GetSceneIDInGame();
                    }

                }


            }

        }
        else // for header and tabs teleport
        {

            int indexSceneOfThis = int.Parse(gameObject.name.ToCharArray()[0].ToString());
            //Debug.Log(indexSceneOfThis.GetType() + " : " + indexSceneOfThis);

            foreach (TeleInformation t in listTeleportAllScene)
            {
                if (t.statusTeleport == StatusTeleport.Activate && indexSceneOfThis == t.GetSceneIDInGame())
                {
                    listAllTele_Or_Scene_Activate.Add(t);

                    //Debug.Log(t.name);
                }
            }


        }

        for (int i = 0; i < allHeaderLM.transform.childCount; i++)
        {
            bool isActive = false;
            foreach (TeleInformation ti in listAllTele_Or_Scene_Activate)
            {
                //Debug.Log();
                if (i == ti.GetSceneIDInGame() && isForScene)
                {

                    if (allLM[i].TryGetComponent(out TeleportTabsUI teleUI)) // need update
                    {
                        teleUI.needUpdate = true;
                    }


                    isActive = true;


                    break;
                }else if (i == ti.GetPositionIDInScene() && !isForScene)
                {
                    isActive = true;
                    break;
                }
            }

            allHeaderLM.transform.GetChild(i).gameObject.SetActive(isActive);
            allLM[i].SetActive(isActive);
        }

    }



}
