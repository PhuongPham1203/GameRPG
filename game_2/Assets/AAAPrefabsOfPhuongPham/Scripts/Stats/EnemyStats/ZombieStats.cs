using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieStats : CharacterStats
{
    AudioEnemy audioEnemy;

    private void Start()
    {
        //SetAllBaseValue();
        enemyController = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
        ResetAllCurrentAndMaxValue( startHP ,startAttackDame,startPosture );
        audioEnemy = GetComponent<AudioEnemy>();


    }

    //public Image nameUI;
    /*
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }


    void OnEquipmentChanged(Equipment newItem,Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.defendModifier);
            damage.AddModifier(newItem.attackDameModifier);
            

        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.defendModifier);
            damage.RemoveModifier(oldItem.attackDameModifier);

        }
    }
    */
    public override void TakeDamege(int damage){
        //base.TakeDamege(damage);
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
                    animator.SetTrigger("InBlock");

                    audioEnemy.PlaySoundOfEnemy("Block" + Random.Range(1, 3));

                    vfxSteel.Play();
                    enemyController.BlockDamage(0.5f);

                    this.Reduction(3f);
                }
            }
            else
            {
                canBlockMore = false;
                Debug.Log("can't block anymore");


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
                this.Reduction(3f);

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

        //new WaitForSeconds(0.2f);

        Invoke("MyDelayedCode", 0.5f);

        


        StartCoroutine(DestroyAfter(3f));

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

    IEnumerator DestroyAfter(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Instantiate(vfxDie, transform.position, transform.rotation);
        Destroy(gameObject);


    }


    void MyDelayedCode()
    {
        if (!PlayerManager.instance.player.GetComponent<PlayerController>().onCombat && AudioManager.instance.IsPlayTheme("OnCombat"))
        {
            //Debug.Log("STop");
            AudioManager.instance.StopSoundOfTheme("OnCombat");
        }
        //Debug.Log("Hello!");
    }

}
