using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : SourceItemSlot
{
    public EquipmentSlot equipSlot;
    public GameObject prefabObj;
    //public SkinnedMeshRenderer mesh;
    public int hpModifier;// HP
    public int attackDameModifier;// attack
    public int postureModifier;// posture
    //public int defendModifier;// defend
    public float timeStum;


    public override void Use()
    {
        //base.Use();
        // Equip the Item
        Debug.Log("Equip Weapon" + this.name);
        EquipmentManager.instance.Equip(this);

        // Remove it from the inventory
        RemoveFromInventory();


    }


}
//public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
//public enum EquipmentMeshRegion { Legs, Arms, Torso } // Corresponds to body blendshapes.
