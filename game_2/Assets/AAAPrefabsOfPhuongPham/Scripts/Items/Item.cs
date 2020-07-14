using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : SourceItemSlot
{
    public override void Use()
    {
        base.Use();
        Debug.Log("Use Item" + this.name);
    }

}
