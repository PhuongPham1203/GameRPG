using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerOfZombie : WeaponControllerOfBoss
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 24)
        {

            this.inforAttack = this.enemyController.inforAttackCurrent;
            //Debug.Log("Player take Damage");
            IsHit isHit = other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);
            this.CheckIfPlayerDeflect(isHit);
        }

    }
}
