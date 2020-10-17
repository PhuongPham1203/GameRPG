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

    public override void TakeDamage(int damage, float timeStun, AttackTypeEffect attackTypeEffect, EnemyController enemyC)
    {
        
        if (animator.GetInteger("InAction") != 8 && animator.GetInteger("InAction") != 10)
        {
            if(this.enemyController.alertEnemy != AlertEnemy.OnTarget){
                Debug.Log("Activate by player  hit");
                this.ActivateBot();
            }
            


            bool canBlockMore = true;
            if (currentPosture < maxPosture)
            {
                if (animator.GetBool("Block"))
                {
                    this.animator.SetTrigger("BlockDamage");

                    this.audioEnemy.PlaySoundOfEnemy("Block" + Random.Range(1, 3));

                    this.vfxSteel.Play();
                    this.enemyController.BlockDamage(timeStun);

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

                this.currentHP -= damage;

                //enemyController.Damage(0.5f);
                this.audioEnemy.PlaySoundOfEnemy("Blood");
                this.vfxBlood.Play();
                this.Reduction(timeWaitToReduction);

                /*
                if (canBlockMore)
                {
                    animator.SetBool("Block",true);
                }
                */

                //Debug.Log(transform.name + " HP Take " + damage + " damege.");

            }

            UpdateHPAndPosture();

            if (this.currentHP <= 0)
            {

                //Debug.Log("Can Finish");
                Die();

            }
            else if (this.currentPosture >= this.maxPosture)
            {

                this.enemyController.CanFinishBot(2f);
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

        //Invoke("MyDelayedCode", 0.5f);

        


        StartCoroutine(DisableAfter(3.5f));

        //Destroy(gameObject,3);

        //PlayerManager.instance.KillPlayer();
    }
    private void ActivateBot(){
        if (this.enemyController != null )
            {
                if(this.enemyController.alertEnemy == AlertEnemy.Idle)
                {
                    this.enemyController.SetAlentCombat(AlertEnemy.OnTarget);

                    if (AudioManager.instance.IsPlayTheme("OnCombat_Weindigo"))
                    {

                    }
                    else
                    {
                        AudioManager.instance.PlaySoundOfTheme("OnCombat_Weindigo");

                        if (AudioManager.instance.IsPlayTheme("OnCombat"))
                        {
                            AudioManager.instance.StopSoundOfTheme("OnCombat");
                        }
                    }
                }
            }
    }

}



[System.Serializable]
public class PhaseBossStats
{
    public string name = "Phase";
    public int HP = 100;
    public int Posture = 100;
}