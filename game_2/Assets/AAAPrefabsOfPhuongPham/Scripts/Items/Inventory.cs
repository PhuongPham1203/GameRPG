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
            return;

        }
        instance = this;

    }
    #endregion
    [Header("List Item")]
    public List<SourceItemSlot> items;
    public int spaceItem = 100;
    [Header("List Weapon")]
    public List<SourceItemSlot> weapons;
    public int spaceWeapon = 50;
    public GameObject ButtonActionWithObj;
    public bool Add(SourceItemSlot item)
    {
        if (!item.isDefaultItem)// if is Defaul Item
        {
            if (item.typeUnit == 2)// Check if item is Weapon
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
            }

        }
        return true;
    }

    public void Remove(SourceItemSlot item)
    {
        if (item.typeUnit == 2)//Weapon
        {
            weapons.Remove(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();

            }
        }



    }



}
