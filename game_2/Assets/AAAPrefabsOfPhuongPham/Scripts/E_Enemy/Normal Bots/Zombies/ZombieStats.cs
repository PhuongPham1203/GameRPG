using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieStats : CharacterStats
{

    private void Start()
    {
        //SetAllBaseValue();
        this.enemyController = GetComponent<EnemyController>();
        this.animator = GetComponent<Animator>();
        this.ResetAllCurrentAndMaxValue( startHP ,startAttackDame,startPosture );
        this.audioEnemy = GetComponent<AudioEnemy>();


    }

    
    public override IsHit TakeDamage(int damage, float timeStun, AttackTypeEffect attackTypeEffect,EnemyController enemyC)
    {
        //base.TakeDamage(damage);
        //Debug.Log("Zombie"+transform.name+" take: " + damage);

        if (animator.GetInteger("InAction") != 8 /*&& animator.GetInteger("InAction") != 10*/)
        {
            if(this.enemyController.alertEnemy == AlertEnemy.Idle)
            {
                float distanc = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);
                if (distanc < 5)
                {
                    StartCoroutine(this.enemyController.LookAtAfter(0.6f));
                }
                this.enemyController.SetAlentCombat(AlertEnemy.Warning);
                //transform.rotation = Quaternion.Inverse(transform.rotation);
            }
            else if (this.enemyController.alertEnemy == AlertEnemy.Warning)
            {
                float distanc = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);

                if (distanc < 5)
                {
                    StartCoroutine(this.enemyController.LookAtAfter(0.5f));
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
                    this.enemyController.BlockDamage(0.5f);

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

                this.enemyController.Damage(0.5f);
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

                this.enemyController.CanFinishBot(2f);
                //enemyController.Stun(2f);
                //playerController.PlayerStun();
                
            }
        }
        return IsHit.Hit;
    }
    /*
    public override void Finish1()
    {
        base.Finish1();
        enemyController.StopAllCoroutines();
        enemyController.canAction = false;

        //AudioManager.instance.PlaySoundOfPlayer("Damage");


    }

    public override void Finish2()
    {
        base.Finish2();
        currentHP = 0;

        Die();
    }
    */
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
        PlayerManager.instance.player.GetComponent<SelectPlayer>().targetEnemy = null;
        

        //new WaitForSeconds(0.2f);

        //Invoke("MyDelayedCode", 0.5f);

        StartCoroutine(MyDelayedCode(2f));
        
        StartCoroutine(DisableAfter(3f));

        //Destroy(gameObject,3);

        //PlayerManager.instance.KillPlayer();
    }
    public override void SetUIActivate(bool activate)
    {
        base.SetUIActivate(activate);
    }

    public override void UpdateHPAndPosture()
    {
        if (currentPosture>= maxPosture)
        {
            enemyController.canFinish = true;
        }else if (enemyController.alertEnemy == AlertEnemy.OnTarget ){
            enemyController.canFinish = false;

        }

        if (hpUI!=null)
        {
            base.UpdateHPAndPosture();

        }
    }

    public override int GetAttackDame(int weapon)
    {
        return base.GetAttackDame(weapon);
        //return currentAttackDame ;
    }

    private void OnDestroy()
    {
        //vfxDie.Play();
        //Debug.Log("Détroy");
    }

    protected IEnumerator DestroyAfter(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Instantiate(vfxDie, transform.position, transform.rotation);
        Destroy(gameObject);


    }


    IEnumerator MyDelayedCode(float t)
    {
        yield return new WaitForSeconds(t);
        if (!PlayerManager.instance.player.GetComponent<PlayerController>().onCombat && AudioManager.instance.IsPlayTheme("OnCombat"))
        {
            //Debug.Log("STop");
            AudioManager.instance.StopSoundOfTheme("OnCombat");
        }
        //Debug.Log("Hello!");
    }

}
