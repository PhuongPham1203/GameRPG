using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    [Header("Item")]
    public SourceItemSlot itemSlot;
    public Image icon;
    public Text numberCurrentItem;
    //public Button openInformationButton;// Check information button
    //public Button useItemButton; // button use item
    //public Button setSlotButton;// set slot item button
    //public Button cancelButton;// on off UI button


    
    [Header("UI Information Item")]
    public Text nameItem;
    public Text informationItem;
    //public Text numberItem; // number current Item


    //private SlotInformation slot;
    //SourceItemSlot slot;
    public void AddItem(SourceItemSlot newItem)
    {

        this.itemSlot = newItem;
        this.icon.sprite = this.itemSlot.icon;
        this.icon.enabled = true;

        this.numberCurrentItem.text = this.itemSlot.currentNumberItem.ToString();




        /*
        this.inforButton.gameObject.SetActive(true);
        this.addButton.gameObject.SetActive(true);
        this.removeButton.gameObject.SetActive(true);
        */
        //removeButton.interactable = true;
    }
    public void ClearSlot()
    {
        this.itemSlot = null;

        this.icon.sprite = null;
        this.icon.enabled = false;
        this.numberCurrentItem.text = "";
        //removeButton.interactable = false;
        /*
        this.inforButton.gameObject.SetActive(false);
        this.addButton.gameObject.SetActive(false);
        this.removeButton.gameObject.SetActive(false);
        */

    }

    public void CheckInforButtonSlot(SourceItemSlot itemNew)
    {
        int languageIndex = PlayerPrefs.GetInt("_language_index", 0);
        //Debug.Log("Open Information" + this.numberCurrentItem.text);
        InventorySlot inventoryS = MenuController.instance.uiInformationItem.GetComponent<InventorySlot>();

        inventoryS.itemSlot = itemNew;
        inventoryS.nameItem.text = inventoryS.itemSlot.nameItem[languageIndex];
        inventoryS.informationItem.text = inventoryS.itemSlot.information[languageIndex];
        inventoryS.numberCurrentItem.text = "Total : " + inventoryS.itemSlot.currentNumberItem + " / " + inventoryS.itemSlot.maxNumberItem;

        MenuController.instance.uiInformationItem.SetActive(true);

    }
    /*
    public void UseItemButtonSlot()
    {
        if (this.itemSlot != null)
        {
            this.itemSlot.Use();
        }
    }
    */

    public void RemoveButtonSlot()
    {

        Inventory.instance.Remove(this.itemSlot);
    }

    public void UseItem()
    {
        if (this.itemSlot != null)
        {
            if (this.itemSlot.currentNumberItem > 0)
            {
                this.itemSlot.Use();
                this.itemSlot.currentNumberItem-- ;
                this.numberCurrentItem.text = this.itemSlot.currentNumberItem.ToString();

            }
        }
    }

    public void OpenInformationUI()
    {
        if (this.itemSlot != null)
        {
            this.CheckInforButtonSlot(this.itemSlot);

        }
    }

    public void SetFastItems(int slot)
    {
        Inventory.instance.SetFastItems(slot,this.itemSlot);
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
/*
[System.Serializable]
public class SlotInformation
{
    public SourceItemSlot item;
    public int maxNumberItem;
    public int currentNumberItem;

}
*/