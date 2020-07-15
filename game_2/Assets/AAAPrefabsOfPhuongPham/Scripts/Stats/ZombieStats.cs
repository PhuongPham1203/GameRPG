using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : CharacterStats
{
    /*
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
            armor.RemoveModifier(oldItem.defendModifier);
            damage.RemoveModifier(oldItem.attackDameModifier);

        }
    }
    */
    public override void TakeDamege(int damage){
        
    }
    public override void Die()
    {
        base.Die();
        // Kill the this Enemy
        Destroy(gameObject,3);
        
        //PlayerManager.instance.KillPlayer();
    }
}
