using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootKickPhase2Combo8 : WeaponControllerOfBoss
{
    public float speedStun = 15f;
    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.layer == 2 && other.gameObject.CompareTag("Deflect"))
        { // layer ignore Raycast
            if (this.enemyController == null)
            {
                this.enemyController = transform.root.GetComponent<EnemyController>();
            }
            if (this.enemyController.inforAttackCurrent.attackTypeEffect == AttackTypeEffect.Dead)
            {
                return;
            }
            this.GetComponent<Collider>().enabled = false;
            this.inforAttack = this.enemyController.inforAttackCurrent;
            // For Player
            AudioManager.instance.PlaySoundOfPlayer("Deflect");

            other.GetComponentInParent<CharacterStats>().vfxSteel.Play();
            other.GetComponentInParent<CharacterStats>().AddPostureDeflect(this.inforAttack.damageAttack / 4);

            // For Enemy
            this.enemyController.StopCoroutine(this.enemyController.actionLeaveAction);
            this.enemyController.PlayerDeflectEnemy(this.inforAttack);
            this.enemyController.GetComponent<Animator>().SetTrigger("triggerDeflect");
        }
        else 
        */
        if (other.gameObject.layer == 24)
        {

            this.inforAttack = this.enemyController.inforAttackCurrent;
            //Debug.Log("Player take Damage");
            //other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);
            if (this.inforAttack != null)
            {
                IsHit isHitPlayer = other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);
                if (isHitPlayer == IsHit.Deflect)
                {
                    // For Enemy
                    this.GetComponent<Collider>().enabled = false;
                    this.enemyController.StopCoroutine(this.enemyController.actionLeaveAction);
                    this.enemyController.PlayerDeflectEnemy(this.inforAttack);
                    this.enemyController.GetComponent<Animator>().SetTrigger("triggerDeflect");

                }
                else
                {
                    CCStun ccStun = other.gameObject.AddComponent<CCStun>();
                    //ccStun.isUp = this.isUp;
                    ccStun.speedStun = this.speedStun;
                    ccStun.SetTimeDestroy(this.inforAttack.timeStun);
                }

            }
        }
    }
}
