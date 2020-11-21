using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxPlayer : Hitbox
{
    [Header("Weapon : 1-Light 2-Heavy")]
    [Range(1, 2)]
    public int weapon = 1;
    public Equipment weaponEquipment;

    private void Awake()
    {
        if (weaponEquipment.equipSlot == EquipmentSlot.LightWeapon)
        {
            weapon = 1;
        }else if(weaponEquipment.equipSlot == EquipmentSlot.HeavyWeapon){
            weapon = 2;
        }

        characterStats = PlayerManager.instance.player.GetComponent<CharacterStats>();

        Destroy(gameObject, timeDetroy);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 23)
        {
            //Debug.Log(characterStats.transform.name + " hitbox "+ characterStats.GetAttackDame(1));
            //other.gameObject.GetComponent<PlayerStats>().TakeDamage(characterStats.GetAttackDame(1));
            //Debug.Log("Hitbox Player:"+ characterStats.GetAttackDame(weapon));

            other.gameObject.GetComponent<CharacterStats>().TakeDamage(characterStats.GetAttackDame(weapon), this.weaponEquipment.timeStum, AttackTypeEffect.Normal, null);

        }
    }

}
