﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Remoting.Contexts;


public class CharacterStats : MonoBehaviour
{
    [Header("Name")]
    public string nameSelf = "Enemy";

    // !Source Start
    [Header("Source Start")]
    public int startHP = 100;
    public int startAttackDame = 0;
    public int startPosture = 100;
    //public int startDefend = 0;

    // !Max and current Stat
    //[Header("Max and Current Stat")]
    public float maxHP { get; protected set; }
    public float currentHP { get; protected set; }
    public int currentAttackDame { get; protected set; }

    public float maxPosture { get; protected set; }
    public float currentPosture { get; protected set; }
    //public int currentDefend { get; protected set; }

    public Vector3 teleportNearest { get; protected set; }


    [Header("Reduction Posture")]
    public bool reduction = true;
    [Range(0.0001f, 0.5f)]
    public float percenReduction ;
    private Coroutine actionReduction;

    //public Stat damage; // old damage
    //public Stat armor; // old Armor
    [Header("VFX")]
    public ParticleSystem vfxBlood;
    public ParticleSystem vfxSteel;
    public ParticleSystem vfxFinish;
    public GameObject vfxDie;


    [Header("UI Profile")]
    public Image hpUI;
    public Image postureUI;
    public Color startPostureUI;
    public Color end90PostureUI;
    public Color endPostureUI;



    protected Animator animator;
    protected EnemyController enemyController;
    

    public int d = 100;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamege(d);
        }

        

    }

    private void LateUpdate()
    {
        if (reduction && currentPosture > 0)
        {
            float x =  (currentHP / maxHP); // percent HP lost

            currentPosture -= (maxPosture * ( percenReduction + percenReduction * x ));
            currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture);

            UpdateHPAndPosture();

        }
    }



    public virtual int GetAttackDame(int weapon)
    {
        return currentAttackDame;
    }

    public virtual void TakeDamege(int damage)
    {
       
    }

    public virtual void TakeTrueDamegeFinish(int damage)
    {
        //float x = 1f - ((float)currentHP / (float)maxHP); // percent HP lost
                                                          //Debug.Log(x);
        //currentPosture += (int)(damage + damage * x);
        //currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture);

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPAndPosture();
        vfxFinish.Play();

        AudioManager.instance.PlaySoundOfPlayer("Damage");

    }

    public virtual void SetUIActivate(bool activate)
    {
        if (activate)
        {
            hpUI = PlayerManager.instance.hpTarget;
            postureUI = PlayerManager.instance.postureTarget;
            PlayerManager.instance.nameTarget.text = nameSelf;

            UpdateHPAndPosture();
        }

        PlayerManager.instance.UITarget.SetActive(activate);
    }

    public virtual void UpdateHPAndPosture()
    {
        float hp01 = Mathf.Clamp01(currentHP / maxHP);
        float posture01 = Mathf.Clamp01(currentPosture / maxPosture);
        hpUI.fillAmount = hp01;
        postureUI.fillAmount = posture01;

        Color comp;
        if (posture01 < 0.9)
        {
            comp = Color.Lerp(startPostureUI, end90PostureUI, posture01);
        }
        else
        {
            comp = endPostureUI;
        }

        postureUI.color = comp;

        //Debug.Log(hp01);
        //Debug.Log(posture01);

    }
    /* 
    public virtual void Finish1()
    {
        Debug.Log("Finish1");
    }
    public virtual void Finish2()
    {
        Debug.Log("Finish2");
    }
    */
    public virtual void Die()
    {
        //Die in some way
        // this method is meant to be overwritter;
        Debug.Log(transform.name + " Die.");
    }

    public float[] GetTransformCurrent()
    {
        //Vector3 positionCur = transform.position;
        return new float[] { teleportNearest.x, teleportNearest.y, teleportNearest.z };
    }
    public void SetTransformCurrent(float x, float y, float z)
    {
        teleportNearest = new Vector3(x, y, z);
        //Debug.Log("teleportNearest : "+ teleportNearest);


        Debug.Log("befo " + teleportNearest);
        PlayerManager.instance.player.transform.position = teleportNearest;

    }
    //public GameObject cube;
    public virtual void SetTest()
    {
        //teleportNearest = new Vector3(x, y, z);
        //Debug.Log("teleportNearest : "+ teleportNearest);


        //Debug.Log("befo " + teleportNearest);
        PlayerManager.instance.player.transform.position = new Vector3(0, 0);
        Debug.Log("befo " + PlayerManager.instance.player.transform.position);

    }

    /*
    protected virtual void SetAllBaseValue()
    {
        hp.SetBaseValue(startHP);
        attackDame.SetBaseValue(startAttackDame);
        posture.SetBaseValue(startPosture);
        defend.SetBaseValue(startDefend);

        //sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    */
    protected virtual void ResetAllCurrentAndMaxValue(int maxHPValue, int currentAttackDameValue, int maxPostureValue/*, int currentDefendValue*/)
    {

        maxHP = maxHPValue;
        currentHP = maxHP;

        currentAttackDame = currentAttackDameValue;

        maxPosture = maxPostureValue;
        currentPosture = 0;//maxPosture;

        //currentDefend = currentDefendValue;
    }

    //Reduction

    private IEnumerator CanReduction(float waitTime)
    {

        reduction = false;
        //Debug.Log("start Re");

        yield return new WaitForSeconds(waitTime);
        reduction = true;
        //Debug.Log("end Re");
    }

    public void Reduction(float t)
    {

        if (actionReduction != null)
        {
            StopCoroutine(actionReduction);
        }
        actionReduction = StartCoroutine(CanReduction(t));

    }

    


}
