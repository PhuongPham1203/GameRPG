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
    public Vector3 potisionCurrenNearestNow;
    //private Coroutine loadingMoveToPosition;


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
        EquipmentManager.instance.Invoke("EquipAllDefaultItems",1f);
    }

    public void ResetAllCurrentAndMaxValue()
    {
        ResetAllCurrentAndMaxValue(startHP + hp.GetValue(), startAttackDame + attackDame.GetValue(), startPosture + posture.GetValue());
        Debug.Log("maxHP:"+maxHP+ " currentHP:"+ currentHP+ " currentAttackDame:"+ currentAttackDame+ " maxPosture:" + maxPosture);
        Debug.Log("Light Attack Dame:"+attackLightDamage+" Heavy Attack Dame"+ attackHeavyDamage);
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
            }else if (newItem.equipSlot == EquipmentSlot.HeavyWeapon)
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
            Debug.Log("Light "+ attackLightDamage);
            return base.GetAttackDame(weapon) + attackLightDamage;

        }
        else
        {
            Debug.Log("Heavy " + attackHeavyDamage);

            return base.GetAttackDame(weapon) + attackHeavyDamage;
        }
    }

    public override void TakeDamege(int damage)
    {
        Debug.Log("Player take: "+damage);

        if (animator.GetInteger("InAction") != 8 && animator.GetInteger("InAction") !=10)
        {
            if (animator.GetBool("Crouch"))
            {
                playerController.CrouchPlayer();
            }

            //Debug.Log("a" + currentPosture);

            bool canBlockMore = true;
            if (currentPosture < maxPosture)
            {
                if (animator.GetInteger("InAction") == 6)
                {
                    animator.SetTrigger("InBlock");

                    int n = Random.Range(1, 5);
                    //Debug.Log(n);
                    if (animator.GetInteger("Weapon") == 1)
                    {
                        AudioManager.instance.PlaySoundOfPlayer("Block Light " + n);
                    }
                    else
                    {
                        AudioManager.instance.PlaySoundOfPlayer("Block Heavy " + n);
                    }
                    vfxSteel.Play();

                    this.Reduction(3f);

                }
            }
            else
            {
                canBlockMore = false;
                Debug.Log("can't block anymore");


            }

            float x = 1f - (currentHP / maxHP); // percent HP lost
            //Debug.Log(x);
            currentPosture += (int)(damage + damage * x);
            currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture);
            //Debug.Log(transform.name + " Posture plus " + (int)(damage + damage * x ) + " damege.");
            

            if (canBlockMore && animator.GetInteger("InAction") == 6) // ! In Block
            {
                //Debug.Log("b"+currentPosture);
            }
            else
            { // ! Not Block
                //Debug.Log("c"+currentPosture);

                currentHP -= damage;
                currentHP = Mathf.Clamp(currentHP,0,maxHP);
                playerController.Damage(0.5f);
                AudioManager.instance.PlaySoundOfPlayer("Damage");
                vfxBlood.Play();
                this.Reduction(3f);
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

                playerController.PlayerStun();
            }
        }

    }
    public override void Die()
    {
        base.Die();
        playerController.PlayerDie();
        animator.SetInteger("InAction", 8);
        gameObject.layer = 2;
        // Kill the Player
        //PlayerManager.instance.KillPlayer();
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

    /*
    #region Money
    public void AddMoney(int number) // add more money
    {
        money += number;
    }
    public void ChangeMoney(int number) // change money
    {
        money = number;
    }
    public void SubMoney(int number) // Sub money
    {
        money -= number;
    }
    #endregion

    #region Real_Money
    public void AddRealMoney(int number) // add more money
    {
        realMoney += number;
    }
    public void ChangeRealMoney(int number) // change real money
    {
        realMoney = number;
    }
    public void SubRealMoney(int number) // Sub real money
    {
        realMoney -= number;
    }
    #endregion

    #region Soul
    public void AddSoul(int number) // add more soul
    {
        soul += number;
    }
    public void ChangeSoul(int number) // change soul
    {
        soul = number;
    }
    public void SubSoul(int number) // Sub soul
    {
        soul -= number;
    }
    #endregion

    #region Level
    public void AddLevel(int number) // add more level
    {
        level += number;
    }
    public void ChangeLevel(int number) // change level
    {
        level = number;
    }
    public void SubLevel(int number) // Sub level
    {
        level -= number;
    }
    #endregion

    #region Exp_Now
    public void AddExpNow(int number) // add more expNow
    {
        expNow += number;
    }
    public void ChangeExpNow(int number) // change expNow
    {
        expNow = number;
    }
    public void SubExpNow(int number) // Sub ExpNow
    {
        expNow -= number;
    }
    #endregion

    #region Exp_To_Level_Up
    public void AddExpToLevelUp(int number) // add more expToLevelUp
    {
        expToLevelUp += number;
    }
    public void ChangeAddExpToLevelUp(int number) // change expToLevelUp
    {
        expToLevelUp = number;
    }
    public void SubAddExpToLevelUp(int number) // Sub expToLevelUp
    {
        expToLevelUp -= number;
    }
    #endregion
    */
}
