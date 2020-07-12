﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SourceItemSlot : ScriptableObject
{
    new public int id = 0;
    new public string name = "New Item";
    [TextArea]
    new public string information = "New Item information";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public virtual void Use()
    {
        // use item
        // something happen;
        Debug.Log("Using " + name);

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

}


public enum EquipmentSlot { Hair, Clothes, LightWeapon, HeavyWeapon, Bow }
public enum ItemSlot { Slot1, Slot2, Slot3, Slot4 }
