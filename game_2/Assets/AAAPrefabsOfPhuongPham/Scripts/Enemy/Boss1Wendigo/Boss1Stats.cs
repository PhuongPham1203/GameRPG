using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Stats : CharacterStats
{

    [Header("Phase Boss Stats")]
    public PhaseBossStats[] phaseBossStats;

    private void Start()
    {
        this.enemyController = GetComponent<EnemyController>();
        this.animator = GetComponent<Animator>();
        this.audioEnemy = GetComponent<AudioEnemy>();
        this.ResetAllCurrentAndMaxValue(this.phaseBossStats[0].HP,0, this.phaseBossStats[0].Posture);
    }

    public override void TakeDamage(int damage, float timeStun, AttackTypeEffect attackTypeEffect, CharacterStats enemyStats)
    {
        
        if (animator.GetInteger("InAction") != 8 && animator.GetInteger("InAction") != 10)
        {
            if(enemyController.alertEnemy == AlertEnemy.Idle)
            {
                float distanc = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);
                if (distanc < 5)
                {
                    StartCoroutine(enemyController.LookAtAfter(0.6f));
                }
                enemyController.SetAlentCombat(AlertEnemy.Warning);
                //transform.rotation = Quaternion.Inverse(transform.rotation);
            }
            else if (enemyController.alertEnemy == AlertEnemy.Warning)
            {
                float distanc = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);

                if (distanc < 5)
                {
                    StartCoroutine(enemyController.LookAtAfter(0.5f));
                }
            }


            bool canBlockMore = true;
            if (currentPosture < maxPosture)
            {
                if (animator.GetBool("Block"))
                {
                    animator.SetTrigger("BlockDamage");

                    audioEnemy.PlaySoundOfEnemy("Block" + Random.Range(1, 3));

                    vfxSteel.Play();
                    enemyController.BlockDamage(0.5f);

                    this.Reduction(timeWaitToReduction);
                }
            }
            else
            {
                canBlockMore = false;
                //Debug.Log("can't block anymore");


            }

            float x = 1f - ((float)currentHP / (float)maxHP); // percent HP lost
            //Debug.Log(x);
            currentPosture += (int)(damage + damage * x);
            currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture);
            //Debug.Log(transform.name + " Posture plus " + (int)(damage + damage * x) + " damege.");

            if (canBlockMore && animator.GetBool("Block")) // ! In Block
            {
                
            }
            else
            { // ! Not Block

                currentHP -= damage;

                enemyController.Damage(0.5f);
                //AudioManager.instance.PlaySoundOfPlayer("Damage");
                vfxBlood.Play();
                this.Reduction(timeWaitToReduction);

                if (canBlockMore)
                {
                    animator.SetBool("Block",true);
                }

                //Debug.Log(transform.name + " HP Take " + damage + " damege.");

            }

            UpdateHPAndPosture();

            if (currentHP <= 0)
            {

                //Debug.Log("Can Finish");
                Die();

            }
            else if (currentPosture >= maxPosture)
            {

                enemyController.CanFinishBot(2f);
                //enemyController.Stun(2f);
                //playerController.PlayerStun();
                
            }
        }
    }
        
    public override void Die()
    {
        base.Die();

        // Kill the this Enemy
        //PlayerManager.instance.player.GetComponent<PlayerController>().LockTarget();
        //SetUIActivate(false);
        enemyController.EnemyDie();
        //animator.SetInteger("InAction", 8);
        PlayerManager.instance.player.GetComponent<PlayerController>().LockTarget();
        this.gameObject.layer = 2;

        //new WaitForSeconds(0.2f);

        Invoke("MyDelayedCode", 0.5f);

        


        StartCoroutine(DisableAfter(2f));

        //Destroy(gameObject,3);

        //PlayerManager.instance.KillPlayer();
    }

}



[System.Serializable]
public class PhaseBossStats
{
    public string name = "Phase";
    public int HP = 100;
    public int Posture = 100;
}