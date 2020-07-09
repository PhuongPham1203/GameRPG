using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    //public SkinnedMeshRenderer mesh;
    public int hpModifier;// HP
    public int attackDameModifier;// attack
    public int postureModifier;// posture
    public int defendModifier;// defend


    public override void Use()
    {
        base.Use();
        // Equip the Item
        EquipmentManager.instance.Equip(this);

        // Remove it from the inventory
        RemoveFromInventory();


    }

}
//public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
public enum EquipmentSlot { Hair, Clothes, LightWeapon, HeavyWeapon, Bow }
//public enum EquipmentMeshRegion { Legs, Arms, Torso } // Corresponds to body blendshapes.
