using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerOfBoss : MonoBehaviour
{
    //public int damage = 0;
    //public float timeStun = 0;
    //public Vector3 vectorStun = Vector3.zero;
    void Start()
    {
        if (this.enemyController == null)
        {
            this.enemyController = transform.root.GetComponent<EnemyController>();
        }
    }
    protected InforAttack inforAttack;
    protected EnemyController enemyController;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 24)
        {

            this.inforAttack = this.enemyController.inforAttackCurrent;
            //Debug.Log("Player take Damage");
            IsHit isHit = other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);
            this.CheckIfPlayerDeflect(isHit);
            /*
            if (isHit == IsHit.Deflect)
            {
                // For Enemy
                this.GetComponent<Collider>().enabled = false;
                this.enemyController.StopCoroutine(this.enemyController.actionLeaveAction);
                this.enemyController.PlayerDeflectEnemy(this.inforAttack);
                this.enemyController.GetComponent<Animator>().SetTrigger("triggerDeflect");

            }*/

        }

    }

    protected void CheckIfPlayerDeflect(IsHit i){
        if (i == IsHit.Deflect)
            {
                // For Enemy
                this.GetComponent<Collider>().enabled = false;
                this.enemyController.StopCoroutine(this.enemyController.actionLeaveAction);
                this.enemyController.PlayerDeflectEnemy(this.inforAttack);
                this.enemyController.GetComponent<Animator>().SetTrigger("triggerDeflect");

                
            }

    }



}
