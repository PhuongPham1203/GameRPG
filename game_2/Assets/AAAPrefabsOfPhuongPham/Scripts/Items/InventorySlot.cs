using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image icon;


    public Button itemButton;// self button
    public Text nameItem;
    public Button inforButton;// Check infor button
    public Button addButton;// Add button
    public Button removeButton;// Delete button

    Item item;
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        nameItem.text = item.name;

        inforButton.gameObject.SetActive(true);
        addButton.gameObject.SetActive(true);
        removeButton.gameObject.SetActive(true);
        //removeButton.interactable = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        nameItem.text = "";
        //removeButton.interactable = false;
        inforButton.gameObject.SetActive(false);
        addButton.gameObject.SetActive(false);
        removeButton.gameObject.SetActive(false);

    }
    public void OnRemoveButton()
    {

        Inventory.instance.Remove(item);
    }

    public void PressSlot()
    {

    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
