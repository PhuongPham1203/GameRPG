using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SourceItemSlot : ScriptableObject
{
    //public string nameItem = "New Item";
    public string[] nameItem = { "New Item","Item mới" };
    public int maxNumberItem ;
    public int currentNumberItem ;

    [Header("Type Item Slot:")]
    //[Range(0, 2)]
    public TypeUnit typeUnit = TypeUnit.ItemReadOnly;
    [TextArea]
    //public string information = "New Item information";
    public string[] information = { "New Item information" ,"Thông tin về Item"};
    
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public virtual void Use()
    {
        // use item
        // something happen;
        Debug.Log("Using :" + name);

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

}


public enum EquipmentSlot { Hair, Clothes, LightWeapon, HeavyWeapon, Bow }
public enum ItemSlot { Slot1, Slot2, Slot3, Slot4 }
public enum TypeUnit { ItemReadOnly, ItemUse, Weapon }
