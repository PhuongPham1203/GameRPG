using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        hp.SetBaseValue(startHP);
        attackDame.SetBaseValue(startAttackDame);
        posture.SetBaseValue(startPosture);
        defend.SetBaseValue(startDefend);

        maxHP = hp.GetValue();
        currentHP = maxHP;

        currentAttackDame = attackDame.GetValue();

        maxPosture = posture.GetValue();
        currentPosture = 0;//maxPosture;

        currentDefend = defend.GetValue();

        animator = GetComponent<Animator>();

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    /* 
    // Old Code
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
    }*/

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            //armor.AddModifier(newItem.defendModifier);
            //damage.AddModifier(newItem.attackDameModifier);

            hp.AddModifier(newItem.hpModifier);
            attackDame.AddModifier(newItem.attackDameModifier);
            posture.AddModifier(newItem.postureModifier);
            defend.AddModifier(newItem.defendModifier);

            maxHP = hp.GetValue();
            currentAttackDame = attackDame.GetValue();
            maxPosture = posture.GetValue();
            currentDefend = defend.GetValue();

        }

        if (oldItem != null)
        {
            //armor.RemoveModifier(oldItem.defendModifier);
            //damage.RemoveModifier(oldItem.attackDameModifier);

            hp.RemoveModifier(oldItem.hpModifier);
            attackDame.RemoveModifier(oldItem.attackDameModifier);
            posture.RemoveModifier(oldItem.postureModifier);
            defend.RemoveModifier(oldItem.defendModifier);
            
            maxHP = hp.GetValue();
            currentAttackDame = attackDame.GetValue();
            maxPosture = posture.GetValue();
            currentDefend = defend.GetValue() ;

        }
    }
    public override void Die()
    {
        base.Die();
        // Kill the Player
        //PlayerManager.instance.KillPlayer();
    }
}
