using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportController : MonoBehaviour
{
    public TeleInformation teleInformation;

    [Header("Position for Player Stay")]
    public Transform pos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();

            playerStats.potisionCurrenNearestNow = pos.position;

            Inventory.instance.ButtonActionWithObj.SetActive(true);
            Button btn = Inventory.instance.ButtonActionWithObj.GetComponent<Button>();

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(playerStats.OpenUI);

            if (teleInformation.statusTeleport == StatusTeleport.Disable)
            {
                //GameObject g = GameObject.FindGameObjectWithTag("MasterTabsTeleport");
                //Debug.Log(g.name);

                if (TeleportManager.instance.teleportUI.TryGetComponent(out TeleportTabsUI teleportTabsUI))
                {
                    teleportTabsUI.needUpdate = true;
                    teleInformation.statusTeleport = StatusTeleport.Activate;
                }
            }
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            Inventory.instance.ButtonActionWithObj.SetActive(false);

        }
    }

}
