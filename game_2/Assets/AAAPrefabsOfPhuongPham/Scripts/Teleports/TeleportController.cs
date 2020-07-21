using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportController : MonoBehaviour
{
    public Transform pos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();

            playerStats.potisionCurrenNearest = pos.position;

            Inventory.instance.ButtonActionWithObj.SetActive(true);
            Button btn = Inventory.instance.ButtonActionWithObj.GetComponent<Button>();

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(playerStats.OpenUI);
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
