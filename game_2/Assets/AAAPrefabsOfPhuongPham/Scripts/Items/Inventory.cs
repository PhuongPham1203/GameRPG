using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton 

    public static Inventory instance;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!!!");
            Destroy(this);

            return;

        }
        instance = this;

    }
    #endregion
    [Header("List Item")]
    public List<SourceItemSlot> items;
    public int spaceItem = 300;
    [Header("List Weapon")]
    public List<SourceItemSlot> weapons;
    public int spaceWeapon = 200;
    public GameObject ButtonActionWithObj;
    public bool Add(SourceItemSlot item, int number)
    {
        bool canAdd = true;
        if (!item.isDefaultItem)// if is Defaul Item
        {
            if (item.typeUnit == TypeUnit.Weapon)// Check if item is Weapon
            {
                if (weapons.Count >= spaceWeapon)
                {
                    Debug.Log("not enough room Weapon");
                    return false;
                }
                weapons.Add(item);
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();

                }
            }
            else// Item is Item ( Item/ReadOnly )
            {

                // Add Item ( Item/ReadOnly ) to Inventory
                // if have same item in Inventory
                // add number
                if (item.currentNumberItem >= item.maxNumberItem) // max item can keep
                {
                    Debug.Log("max number Item can keep");
                    // ! alert cant take any more
                    canAdd = false;
                }
                else
                { // canAdd
                    item.currentNumberItem += number;
                    Mathf.Clamp(item.currentNumberItem, 0, item.maxNumberItem);

                    canAdd = true;
                }
                bool isItemHaveInInventory = false;
                foreach (SourceItemSlot s in this.items)
                {
                    if (s == item)
                    { // already have in inventory

                        //this.items.Add(item,true);
                        isItemHaveInInventory = true;
                        break;
                    }
                }
                if (!isItemHaveInInventory)
                {
                    items.Add(item);
                }
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();

                }

                /*
                if (items.Count >= spaceItem)
                {
                    Debug.Log("not enough room Item");
                    return false;
                }
                items.Add(item);
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();

                }
                */
            }

        }
        return canAdd;
    }

    public void Remove(SourceItemSlot item)
    {
        if (item.typeUnit == TypeUnit.Weapon)//Weapon
        {
            weapons.Remove(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();

            }
        }



    }



}
