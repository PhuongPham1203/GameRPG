using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text nameItem;
    public Button inforButton;// Check infor button
    public Button addButton;// Add button
    public Button removeButton;// Delete button

    SourceItemSlot slot;
    public void AddItem(SourceItemSlot newItem)
    {
        slot = newItem;
        icon.sprite = slot.icon;
        icon.enabled = true;
        nameItem.text = slot.name;

        inforButton.gameObject.SetActive(true);
        addButton.gameObject.SetActive(true);
        removeButton.gameObject.SetActive(true);
        //removeButton.interactable = true;
    }
    public void ClearSlot()
    {
        slot = null;
        icon.sprite = null;
        icon.enabled = false;
        nameItem.text = "";
        //removeButton.interactable = false;
        inforButton.gameObject.SetActive(false);
        addButton.gameObject.SetActive(false);
        removeButton.gameObject.SetActive(false);

    }

    public void CheckInforButtonSlot(){
        Debug.Log("Open Information"+nameItem.text);
    }

    public void UseItemButtonSlot()
    {
        if (slot != null)
        {
            slot.Use();
        }
    }

    public void RemoveButtonSlot()
    {

        Inventory.instance.Remove(slot);
    }
}
