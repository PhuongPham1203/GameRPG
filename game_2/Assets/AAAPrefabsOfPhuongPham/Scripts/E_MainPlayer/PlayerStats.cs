using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;



public class PlayerStats : CharacterStats
{
    // !Stats
    [Header("Stats")]
    public Stat hp; // HP
    public Stat attackDame; // AttacK Dame
    public Stat posture; // Posture
    //public Stat defend; // Defend
    [Range(1, 4)]
    public int maxLife = 1;
    [Range(1, 4)]
    public int currentLife = 1;
    public int attackLightDamage { get; protected set; }
    public int attackHeavyDamage { get; protected set; }

    // Money and Soul
    public int money { get; protected set; }
    public int realMoney { get; protected set; }
    public int soul { get; protected set; }
    public int level { get; protected set; }
    public int expNow { get; protected set; }
    public int expToLevelUp { get; protected set; }
    public int sceneIndex { get; protected set; }

    [Header("UI controll")]
    public GameObject uiSaveLoadTeleport;
    public GameObject uiTeleport;
    public GameObject uiLoading;
    public GameObject uiDie;
    public GameObject uiContinue;
    public Vector3 potisionCurrenNearestNow;
    public Animator animatorRedAlert;
    //private Coroutine loadingMoveToPosition;

    [Header("VFX")]
    public ParticleSystem vfxHP;
    Vector3 pNow;
    bool needchange = false;

    private PlayerController playerController;
    //private AudioManager audioManager;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        /*
        Vector3 a = transform.position;
        a.y += 10f;
        transform.position = a;
        */

        //SetTransformCurrent(teleportNearest.x, teleportNearest.y, teleportNearest.z);

        //SetAllBaseValue();

        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        //audioManager = AudioManager.instance;

        //ResetAllCurrentAndMaxValue(startHP + hp.GetValue(), startAttackDame + attackDame.GetValue(), startPosture + posture.GetValue());
        EquipmentManager.instance.Invoke("EquipAllDefaultItems", 1f);
    }
    private void LateUpdate()
    {
        if (reduction && currentPosture > 0)
        {
            float x = (currentHP / maxHP); // percent HP lost
            float y = 1;
            if (this.animator.GetInteger("InAction") == 6)
            {
                y = 2;
            }

            currentPosture -= (maxPosture * (percenReduction * x) * y) ;
            currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture);

            UpdateHPAndPosture();

        }
    }

    public void ResetAllCurrentAndMaxValue()
    {
        ResetAllCurrentAndMaxValue(startHP + hp.GetValue(), startAttackDame + attackDame.GetValue(), startPosture + posture.GetValue());
        Debug.Log("maxHP:" + maxHP + " currentHP:" + currentHP + " currentAttackDame:" + currentAttackDame + " maxPosture:" + maxPosture);
        Debug.Log("Light Attack Dame:" + attackLightDamage + " Heavy Attack Dame" + attackHeavyDamage);
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            //Debug.Log(newItem.hpModifier);

            hp.AddModifier(newItem.hpModifier);

            if (newItem.equipSlot == EquipmentSlot.LightWeapon)
            {
                attackLightDamage = newItem.attackDameModifier;
            }
            else if (newItem.equipSlot == EquipmentSlot.HeavyWeapon)
            {
                attackHeavyDamage = newItem.attackDameModifier;
            }
            else
            {
                attackDame.AddModifier(newItem.attackDameModifier);
            }

            posture.AddModifier(newItem.postureModifier);
            //defend.AddModifier(newItem.defendModifier);

            maxHP = startHP + hp.GetValue();
            currentAttackDame = startAttackDame + attackDame.GetValue();
            maxPosture = startPosture + posture.GetValue();
            //currentDefend = defend.GetValue();

        }

        if (oldItem != null)
        {
            //armor.RemoveModifier(oldItem.defendModifier);
            //damage.RemoveModifier(oldItem.attackDameModifier);

            hp.RemoveModifier(oldItem.hpModifier);

            if (oldItem.equipSlot != EquipmentSlot.LightWeapon && oldItem.equipSlot != EquipmentSlot.HeavyWeapon)
            {
                attackDame.RemoveModifier(oldItem.attackDameModifier);
            }
            posture.RemoveModifier(oldItem.postureModifier);
            //defend.RemoveModifier(oldItem.defendModifier);

            maxHP = startHP + hp.GetValue();
            currentAttackDame = startAttackDame + attackDame.GetValue();
            maxPosture = startPosture + posture.GetValue();
            //currentDefend = defend.GetValue();

        }
    }



    /*
    private void FixedUpdate()
    {

        if (needchange && pNow == transform.position)
        {

            transform.position = teleportNearest;
            //loadingMoveToPosition = StartCoroutine(Loading(1.5f, teleportNearest));//Loading After 1.5s
            needchange = false;
            //Debug.Log("asd");
        }

    }
    */
    public override int GetAttackDame(int weapon)
    {
        if (weapon == 1)
        {
            //Debug.Log("Light "+ attackLightDamage);
            return base.GetAttackDame(weapon) + attackLightDamage;

        }
        else
        {
            //Debug.Log("Heavy " + attackHeavyDamage);

            return base.GetAttackDame(weapon) + attackHeavyDamage;
        }
    }

    public override IsHit TakeDamage(int damage, float timeStun, AttackTypeEffect attackTypeEffect, EnemyController enemyC)
    {

        //Debug.Log("Player take: " + damage);
        int inAction = this.animator.GetInteger("InAction");
        if (inAction != 8 && inAction != 10 && inAction != 1) // 8 : Die , 10 : inFinish, 1 : Swing
        {
            if (animator.GetBool("Crouch"))
            {
                playerController.CrouchPlayer();
            }

            //Debug.Log("a" + currentPosture);

            bool canBlockMore = true;
            if (attackTypeEffect == AttackTypeEffect.Dead)
            {

                canBlockMore = false;
                this.animator.SetInteger("InAction", 0);

            }
            if (currentPosture < maxPosture)
            {
                if (animator.GetInteger("InAction") == 6)
                {
                    this.vfxSteel.Play();
                    this.Reduction(timeWaitToReduction);

                    if (this.playerController.deflectStatus == DeflectStatus.Deflect) // Deflect ( Parry )
                    {
                        AudioManager.instance.PlaySoundOfPlayer("Deflect");
                        this.animator.SetTrigger("TriggerDeflect");

                        float y = 1f - (currentHP / maxHP); // percent HP lost
                        damage = damage / 2;
                        currentPosture += (int)(damage + damage * y);
                        currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture - 5);
                        this.UpdateHPAndPosture();

                        enemyC.isHitPlayer = IsHit.Deflect;// Alert to enemy is Player block Damage

                        return enemyC.isHitPlayer;
                    }
                    else // Normal Block
                    {
                        enemyC.isHitPlayer = IsHit.Block;// Alert to enemy is Player block Damage

                        this.animator.SetTrigger("InBlock");

                        int n = Random.Range(1, 5);
                        AudioManager.instance.PlaySoundOfPlayer("Block Light " + n);
                    }


                }
            }
            else
            {
                canBlockMore = false;
                Debug.Log("can't block anymore");


            }

            float x = 1f - (currentHP / maxHP); // percent HP lost
            currentPosture += (int)(damage + damage * x);
            currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture);


            if (canBlockMore && animator.GetInteger("InAction") == 6) // ! In Block
            {
                //Debug.Log("b"+currentPosture);
            }
            else
            { // ! Not Block



                currentHP -= damage;
                currentHP = Mathf.Clamp(currentHP, 0, maxHP);
                playerController.Damage(timeStun);

                AudioManager.instance.PlaySoundOfPlayer("Damage");
                vfxBlood.Play();
                this.Reduction(timeWaitToReduction);
                //Debug.Log(transform.name + " HP Take " + damage + " damege.");

                enemyC.isHitPlayer = IsHit.Hit;// Alert to enemy is hit Player

            }

            this.UpdateHPAndPosture();

            if (currentHP <= 0)
            {

                //Debug.Log("Can Finish");
                Die();

            }
            else if (currentPosture >= maxPosture)
            {

                playerController.PlayerStun(2);
            }
        }


        return enemyC.isHitPlayer;
    }

    public override void UpdateHPAndPosture()
    {
        base.UpdateHPAndPosture();
        float posture01 = Mathf.Clamp01(this.currentPosture / this.maxPosture);
        this.animatorRedAlert.SetFloat("posture", posture01);
    }
    public override void Die()
    {
        base.Die();
        playerController.PlayerDie();
        animator.SetInteger("InAction", 8);
        gameObject.layer = 2;

        // Open UI Die

        if (this.currentLife - 1 > 0)
        {
            this.currentLife--;
            this.uiContinue.SetActive(true);
        }
        else
        {
            this.uiContinue.SetActive(false);
        }
        this.uiDie.SetActive(true);

        // Set All AlertEnemy = Idle
        this.SetAllAlertEnemyToIdle();
        // Kill the Player
        //PlayerManager.instance.KillPlayer();
    }

    private void SetAllAlertEnemyToIdle()
    {
        EnemyController[] allEnemy = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in allEnemy)
        {
            enemy.SetAlentCombat(AlertEnemy.Idle);
            if (enemy.TryGetComponent<SelectEnemy>(out SelectEnemy s))
            {
                s.enabled = true;
            }
        }
    }

    public void ContinueAndReLife()
    {
        this.uiDie.SetActive(false);

        this.ResetAllCurrentAndMaxValue();
        this.animator.SetInteger("InAction", 0);
        this.vfxFinish.Play();
        StartCoroutine(LoadingReLife(1.5f));
    }
    IEnumerator LoadingReLife(float t)
    {
        yield return new WaitForSeconds(t);
        this.gameObject.layer = 24;
        this.playerController.canAction = true;
    }

    public void RestartGame()
    {
        SceneManagerOfGame.instance.RestartGameFromCheckPoint();
    }
    public void Standing()
    {
        playerController.canAction = true;
        animator.SetInteger("InAction", 0);
    }

    public IEnumerator Loading(float waitTime, Vector3 postionNearest)
    {
        Debug.Log("Start loading");
        uiLoading.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Cance();
        uiLoading.gameObject.SetActive(false);
        //PlayerManager.instance.player.transform.position = postionNearest;
        //transform.position = postionNearest;

        //PlayerManager.instance.MoveToPoint(postionNearest.x, postionNearest.y, postionNearest.z);

        Debug.Log("End loading");


    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();

        //SetAllBaseValue();
        //SetAllCurrentAndMaxValue(hp.GetValue(), attackDame.GetValue(), posture.GetValue(), defend.GetValue());

        // // SetAllCurrentAndMaxValue(playerData.baseMaxHP, playerData.baseCurrentAttackDame, playerData.baseMaxPoseture, playerData.baseCurrentDefend);

        money = playerData.baseMoney;
        realMoney = playerData.baseRealMoney;
        soul = playerData.baseSoul;

        level = playerData.baseLevel;
        expNow = playerData.baseExpNow;
        expToLevelUp = playerData.baseExpToLevelUp;


        teleportNearest = new Vector3(playerData.positon[0], playerData.positon[1], playerData.positon[2]);

        //loadingMoveToPosition = StartCoroutine(Loading(1.5f, teleportNearest));//Loading After 1.5s

        //Debug.Log(gameObject.transform.position);
        //pNow = transform.position;
        //needchange = true;

        sceneIndex = playerData.sceneIndexCurrent;


    }
    public void OpenUI()
    {

        uiSaveLoadTeleport.gameObject.SetActive(true);
    }
    public void Cance()
    {
        uiSaveLoadTeleport.gameObject.SetActive(false);
    }

    public void OpenUITeleport()
    {
        uiTeleport.gameObject.SetActive(!uiTeleport.gameObject.activeSelf);
    }


}


[System.Serializable]
public enum DeflectStatus { NotBlock, Block, Deflect };
