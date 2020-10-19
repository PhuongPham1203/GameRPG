using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Stats : CharacterStats
{

    [Header("Phase Boss Stats")]
    public PhaseBossStats[] phaseBossStats;
    protected int indexPhaseBossStat = 0;

    private void Start()
    {
        this.enemyController = GetComponent<EnemyController>();
        this.animator = GetComponent<Animator>();
        this.audioEnemy = GetComponent<AudioEnemy>();
        this.indexPhaseBossStat = 0;
        this.ResetAllCurrentAndMaxValue(this.phaseBossStats[indexPhaseBossStat].HP, 0, this.phaseBossStats[indexPhaseBossStat].Posture);
        
        
    }

    public override void TakeDamage(int damage, float timeStun, AttackTypeEffect attackTypeEffect, EnemyController enemyC)
    {

        if (animator.GetInteger("InAction") != 8 && animator.GetInteger("InAction") != 10)
        {
            if (this.enemyController.alertEnemy != AlertEnemy.OnTarget)
            {
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
                this.Die();



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

        if (this.enemyController.ReLive())
        {

            StartCoroutine(SetStatRelive(2.5f));


            return;
        }


        // Kill the this Enemy
        //PlayerManager.instance.player.GetComponent<PlayerController>().LockTarget();
        //SetUIActivate(false);
        enemyController.EnemyDie();
        //animator.SetInteger("InAction", 8);

        PlayerManager.instance.player.GetComponent<PlayerController>().LockTarget();
        this.gameObject.layer = 2;
        //Debug.Log(PlayerManager.instance.player.GetComponent<PlayerController>().targetGroupCiner.m_Targets[1].target);
        PlayerManager.instance.player.GetComponent<SelectPlayer>().targetEnemy = null;


        //Invoke("MyDelayedCode", 0.5f);


        StartCoroutine(MyDelayedCode(0.5f)); // Stop Theme

        StartCoroutine(DisableAfter(4f)); //  Disable Boss

        //Destroy(gameObject,3);

        //PlayerManager.instance.KillPlayer();
    }


    public override void UpdateHPAndPosture()
    {
        if (currentPosture >= maxPosture)
        {
            this.enemyController.canFinish = true;
        }
        else if (enemyController.alertEnemy == AlertEnemy.OnTarget)
        {
            this.enemyController.canFinish = false;

        }

        if (hpUI != null)
        {
            base.UpdateHPAndPosture();

        }
    }
    private void ActivateBot()
    {
        if (this.enemyController != null)
        {
            if (this.enemyController.alertEnemy == AlertEnemy.Idle)
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

    IEnumerator MyDelayedCode(float t)// turn off sound
    {
        yield return new WaitForSeconds(t);


        //Debug.Log("STop");
        if (AudioManager.instance.IsPlayTheme("OnCombat_Weindigo"))
        {
            AudioManager.instance.StopSoundOfTheme("OnCombat_Weindigo");

        }

        //Debug.Log("Hello!");
    }

    IEnumerator SetStatRelive(float t)
    {

        yield return new WaitForSeconds(t);

        

        this.indexPhaseBossStat += 1;

        this.ResetAllCurrentAndMaxValue(this.phaseBossStats[this.indexPhaseBossStat].HP, 0, this.phaseBossStats[this.indexPhaseBossStat].Posture);

        this.animator.SetInteger("InAction", 0);

        if (this.enemyController.alertEnemy != AlertEnemy.OnTarget)
        {
            //Debug.Log("Activate by player  hit");
            this.ActivateBot();
        }
        // disable Current Weapon
        foreach(GameObject g in this.phaseBossStats[this.indexPhaseBossStat-1].listWeapon){
            g.SetActive(false);
        }

        // enable new weapon
        foreach(GameObject g in this.phaseBossStats[this.indexPhaseBossStat].listWeapon){
            g.SetActive(true);
        }

    }
}



