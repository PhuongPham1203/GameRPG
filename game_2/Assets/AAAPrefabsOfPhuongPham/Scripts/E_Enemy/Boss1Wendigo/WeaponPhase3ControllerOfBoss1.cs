using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPhase3ControllerOfBoss1 : WeaponControllerOfBoss1
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.layer == 24)
        {

            this.inforAttack = this.enemyController.inforAttackCurrent;
            //Debug.Log("Player take Damage");
            IsHit isHitPlayer = other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);
            //Debug.Log(isHitPlayer);
            if (isHitPlayer == IsHit.Hit)
            {
                this.enemyController.GetComponent<CharacterStats>().AddHPandPosture(this.inforAttack.damageAttack, 0);
            }
            else if (isHitPlayer == IsHit.Deflect)
            {
                // For Enemy
                this.GetComponent<Collider>().enabled = false;
                this.enemyController.StopCoroutine(this.enemyController.actionLeaveAction);
                this.enemyController.PlayerDeflectEnemy(this.inforAttack);
                this.enemyController.GetComponent<Animator>().SetTrigger("triggerDeflect");

            }

        }
    }

}
