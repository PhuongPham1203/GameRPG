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
    private SlotInformation slot;
    //SourceItemSlot slot;
    public void AddItem(SourceItemSlot newItem)
    {
        this.slot.item = newItem;
        this.icon.sprite = this.slot.item.icon;
        this.icon.enabled = true;
        this.nameItem.text = slot.item.name;

        this.inforButton.gameObject.SetActive(true);
        this.addButton.gameObject.SetActive(true);
        this.removeButton.gameObject.SetActive(true);
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

    public void CheckInforButtonSlot()
    {
        Debug.Log("Open Information" + nameItem.text);
    }

    public void UseItemButtonSlot()
    {
        if (slot != null)
        {
            slot.item.Use();
        }
    }

    public void RemoveButtonSlot()
    {

        Inventory.instance.Remove(slot.item);
    }

    /*
    public void UseItemSlot()
    {
        
        if(this.itemSlot!=null && this.numberCountItem>0){
            this.itemSlot.Use();
        }
        
    }
    */

}

[System.Serializable]
public class SlotInformation
{
    public SourceItemSlot item;
    public int maxNumberItem;
    public int currentNumberItem;

}