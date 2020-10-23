﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerOfBoss : MonoBehaviour
{
    //public int damage = 0;
    //public float timeStun = 0;
    //public Vector3 vectorStun = Vector3.zero;
    protected InforAttack inforAttack;
    protected EnemyController enemyController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            this.enemyController = transform.root.GetComponent<EnemyController>();
            this.inforAttack = this.enemyController.inforAttackCurrent;
            //Debug.Log("Player take Damage");
            other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);
        }

    }



}
