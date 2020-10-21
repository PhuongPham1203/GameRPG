using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerOfBoss1 : WeaponControllerOfBoss
{
    //public int damage = 0;
    //public float timeStun = 0;
    //public Vector3 vectorStun = Vector3.zero;
    //private InforAttack inforAttack;
    public GameObject flyWeapon;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 24)
        {
            this.inforAttack = transform.root.GetComponent<EnemyController>().inforAttackCurrent;
            //Debug.Log("Player take Damage");
            other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack,this.inforAttack.timeStun,this.inforAttack.attackTypeEffect, transform.root.GetComponent<EnemyController>());
        }

    }

    public void CreateFlyWeapon(){
        if(this.flyWeapon!=null){
            GameObject g = Instantiate(flyWeapon);
            if(g.TryGetComponent<FlyWeaponController>(out FlyWeaponController f)){
                f.inforAttack = this.inforAttack;
                f.enemyController = this.transform.root.GetComponent<EnemyController>();
                f.wayFly = (PlayerManager.instance.player.transform.position - this.transform.root.position).normalized;
                g.transform.LookAt(PlayerManager.instance.player.transform.position);
            }
        }

    }


}
