using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }


    void OnEquipmentChanged(Equipment newItem,Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.defendModifier);
            damage.AddModifier(newItem.attackDameModifier);

        }

        if (oldItem != null)
        {
            armor.AddModifier(oldItem.defendModifier);
            damage.AddModifier(oldItem.attackDameModifier);

        }
    }
    public override void Die()
    {
        base.Die();
        // Kill the Player
        //PlayerManager.instance.KillPlayer();
    }
}
