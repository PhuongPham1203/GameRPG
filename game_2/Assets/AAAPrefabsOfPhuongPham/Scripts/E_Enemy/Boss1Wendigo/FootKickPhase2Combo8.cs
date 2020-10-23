using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootKickPhase2Combo8 : WeaponControllerOfBoss
{
    public float speedStun = 15f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            this.enemyController = transform.root.GetComponent<EnemyController>();
            this.inforAttack = this.enemyController.inforAttackCurrent;
            //Debug.Log("Player take Damage");
            other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);
            if (this.inforAttack != null)
            {
                other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);

                CCStun ccStun = other.gameObject.AddComponent<CCStun>();
                //ccStun.isUp = this.isUp;
                ccStun.speedStun = this.speedStun;
                ccStun.SetTimeDestroy(this.inforAttack.timeStun);
            }
        }
    }
}
