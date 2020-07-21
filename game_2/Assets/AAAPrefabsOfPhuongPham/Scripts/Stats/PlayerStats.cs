using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerStats : CharacterStats
{
    // Money and Soul
    public int money { get; protected set; }
    public int realMoney { get; protected set; }
    public int soul { get; protected set; }
    public int level { get; protected set; }
    public int expNow { get; protected set; }
    public int expToLevelUp { get; protected set; }
    public int sceneIndex { get; protected set; }

    [Header("UI contron")]
    public GameObject uiTeleport;
    public Vector3 potisionCurrenNearest;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Vector3 a = transform.position;
        a.y += 10f;
        transform.position = a;
        */

        //SetTransformCurrent(teleportNearest.x, teleportNearest.y, teleportNearest.z);

        SetAllBaseValue();
        SetAllCurrentAndMaxValue(hp.GetValue(), attackDame.GetValue(), posture.GetValue(), defend.GetValue());

        animator = GetComponent<Animator>();

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void SetAllBaseValue()
    {
        hp.SetBaseValue(startHP);
        attackDame.SetBaseValue(startAttackDame);
        posture.SetBaseValue(startPosture);
        defend.SetBaseValue(startDefend);

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void SetAllCurrentAndMaxValue(int maxHPValue, int currentAttackDameValue, int maxPostureValue, int currentDefendValue)
    {

        maxHP = maxHPValue;
        currentHP = maxHP;

        currentAttackDame = currentDefendValue;

        maxPosture = maxPostureValue;
        currentPosture = 0;//maxPosture;

        currentDefend = currentDefendValue;
    }


    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            hp.AddModifier(newItem.hpModifier);
            attackDame.AddModifier(newItem.attackDameModifier);
            posture.AddModifier(newItem.postureModifier);
            defend.AddModifier(newItem.defendModifier);

            maxHP = hp.GetValue();
            currentAttackDame = attackDame.GetValue();
            maxPosture = posture.GetValue();
            currentDefend = defend.GetValue();

        }

        if (oldItem != null)
        {
            //armor.RemoveModifier(oldItem.defendModifier);
            //damage.RemoveModifier(oldItem.attackDameModifier);

            hp.RemoveModifier(oldItem.hpModifier);
            attackDame.RemoveModifier(oldItem.attackDameModifier);
            posture.RemoveModifier(oldItem.postureModifier);
            defend.RemoveModifier(oldItem.defendModifier);

            maxHP = hp.GetValue();
            currentAttackDame = attackDame.GetValue();
            maxPosture = posture.GetValue();
            currentDefend = defend.GetValue();

        }
    }
    public override void Die()
    {
        base.Die();
        // Kill the Player
        //PlayerManager.instance.KillPlayer();
    }



    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();

        SetAllBaseValue();
        //SetAllCurrentAndMaxValue(hp.GetValue(), attackDame.GetValue(), posture.GetValue(), defend.GetValue());
        SetAllCurrentAndMaxValue(playerData.baseMaxHP, playerData.baseCurrentAttackDame,playerData.baseMaxPoseture, playerData.baseCurrentDefend);

        money = playerData.baseMoney;
        realMoney = playerData.baseRealMoney;
        soul = playerData.baseSoul;

        level = playerData.baseLevel;
        expNow = playerData.baseExpNow;
        expToLevelUp = playerData.baseExpToLevelUp;


        SetTransformCurrent(playerData.positon[0], playerData.positon[1], playerData.positon[2]);
        sceneIndex = playerData.sceneIndexCurrent;

        //Debug.Log(gameObject.transform.position);

    }

    public void OpenUI()
    {
        
        uiTeleport.gameObject.SetActive(true);
    }
    public void Cance()
    {
        uiTeleport.gameObject.SetActive(false);
    }


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

}
