using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerOfBoss : MonoBehaviour
{
    public int damage = 0;
    public float timeStun = 0;
    public Vector3 vectorStun = Vector3.zero;
    private InforAttack inforAttack;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 24)
        {
            this.inforAttack = transform.root.GetComponent<EnemyController>().inforAttackCurrent;
            //Debug.Log("Player take Damage");
            other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack,this.inforAttack.timeStun,this.inforAttack.attackTypeEffect, transform.root.GetComponent<EnemyController>());
        }

    }



}
