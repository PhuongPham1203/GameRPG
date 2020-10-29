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
        else if (other.gameObject.layer == 24)
        {
            if (this.enemyController == null)
            {
                this.enemyController = transform.root.GetComponent<EnemyController>();
            }
            this.inforAttack = this.enemyController.inforAttackCurrent;
            //Debug.Log("Player take Damage");
            other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);
        }

    }

    public void CreateFlyWeapon(EnemyController e)
    {
        if (this.flyWeapon != null)
        {

            GameObject g = Instantiate(flyWeapon);
            g.transform.position = this.transform.position;

            FlyWeaponController f = g.GetComponent<FlyWeaponController>();
            //float sf = f.speedFly;
            //f.speedFly = 0;

            f.enemyController = e;
            f.inforAttack = e.inforAttackCurrent;

            //f.SetInfoAttackFlyWeapon(this.inforAttack);
            Vector3 posP = PlayerManager.instance.player.transform.position;
            //posP.y+=0.5f;
            g.transform.LookAt(posP);



            f.wayFly = (posP - g.transform.position).normalized;
            //f.speedFly = sf;


        }

    }


}
