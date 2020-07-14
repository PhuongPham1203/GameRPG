using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryIU : MonoBehaviour
{

    public Transform itemsParent;
    public Transform weaponsParent;
    //public GameObject inventoryUI;

    Inventory inventory;
    InventorySlot[] slotsItem;
    InventorySlot[] slotsWeapon;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slotsItem = itemsParent.GetComponentsInChildren<InventorySlot>();//Item
        slotsWeapon = weaponsParent.GetComponentsInChildren<InventorySlot>();//Weapon

    }


    void UpdateUI()
    {
        //Debug.Log("Update UI Item");
        for (int i = 0; i < slotsItem.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slotsItem[i].AddItem(inventory.items[i]);

            }
            else
            {
                slotsItem[i].ClearSlot();
            }
        }
        //Debug.Log("Update UI Weapon");  
        for (int i = 0; i < slotsWeapon.Length; i++)
        {
            if (i < inventory.weapons.Count)
            {
                slotsWeapon[i].AddItem(inventory.weapons[i]);

            }
            else
            {
                slotsWeapon[i].ClearSlot();
            }
        }
    }

}
