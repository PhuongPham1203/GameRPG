using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPhase3ControllerOfBoss1 : WeaponControllerOfBoss1
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            this.enemyController = transform.root.GetComponent<EnemyController>();
            this.inforAttack = this.enemyController.inforAttackCurrent;
            //Debug.Log("Player take Damage");
            IsHit isHitPlayer = other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);
            //Debug.Log(isHitPlayer);
            if(isHitPlayer == IsHit.Hit){
                //Debug.Log("Add HP"+this.inforAttack.damageAttack);
                this.enemyController.GetComponent<CharacterStats>().AddHPandPosture(this.inforAttack.damageAttack,0);
            }
        }

    }
}
