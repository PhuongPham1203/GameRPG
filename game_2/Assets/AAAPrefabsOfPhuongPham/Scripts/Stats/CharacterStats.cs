using System.Collections;
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
    public float maxHP { get; set; }
    public float currentHP { get; set; }
    public int currentAttackDame { get; set; }

    public float maxPosture { get; set; }
    public float currentPosture { get; set; }
    //public int currentDefend { get; protected set; }

    public Vector3 teleportNearest { get; set; }


    [Header("Reduction Posture")]
    public bool reduction = true;
    [Range(0f, 5f)]
    public float timeWaitToReduction = 3f;
    [Range(0.00001f, 0.5f)]
    public float percenReduction;
    //[Header("xPlus small && low HP => slow Reduction")]
    //[Range(0f, 1f)]
    //public float xPlus=1f;
    //public bool isHit = false;

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
    public Color startPostureUI = new Color(255f / 255f, 255f / 255f, 255f / 255f, 222f / 255f);

    public Color end90PostureUI = new Color(224f / 255f, 99f / 255f, 35f / 255f, 222f / 255f);
    public Color endPostureUI = new Color(255f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);


    protected EnemyController enemyController;
    protected Animator animator;
    protected AudioEnemy audioEnemy;

    [Header("For Dev Only")]
    public int d = 100;

    [Header("List Items this Character Keep")]
    public List<SourceItemSlot> itemKeep = new List<SourceItemSlot>();
    [Header("List Item Drop Affter Die")]
    public List<GameObject> itemDrop = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(d, 0, AttackTypeEffect.Normal, new EnemyController());
        }





    }

    private void LateUpdate()
    {
        if (reduction && currentPosture > 0)
        {
            float x = (currentHP / maxHP); // percent HP lost

            currentPosture -= (maxPosture * (percenReduction * x));
            currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture);

            UpdateHPAndPosture();

        }
    }



    public virtual int GetAttackDame(int weapon)
    {
        //Debug.Log("attack dame "+currentAttackDame);
        return currentAttackDame;
    }

    public virtual IsHit TakeDamage(int damage, float timeStun, AttackTypeEffect attackTypeEffect, EnemyController enemyController)
    {
        Debug.Log(this.name + " Take" + damage + " Dame" + " time stun: " + timeStun + " ATE : " + attackTypeEffect + " from: " + enemyController.name);
        return IsHit.Miss;
    }

    public virtual void TakeTrueDamageFinish(int damage)
    {
        //float x = 1f - ((float)currentHP / (float)maxHP); // percent HP lost
        //Debug.Log(x);
        //currentPosture += (int)(damage + damage * x);
        //currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture);

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPAndPosture();
        vfxFinish.Play();

        this.audioEnemy.PlaySoundOfEnemy("HitDamage");
        this.audioEnemy.PlaySoundOfEnemy("StreamBlood");

        //AudioManager.instance.PlaySoundOfPlayer("HitDamage");
        //AudioManager.instance.PlaySoundOfPlayer("HitDamage");

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

    public virtual void AddHPandPosture(float h, float p)
    {
        this.currentHP += h;
        this.currentHP = Mathf.Clamp(this.currentHP, 0, this.maxHP);

        this.currentPosture += p;
        this.currentPosture = Mathf.Clamp(this.currentPosture, 0, this.maxPosture);
        this.UpdateHPAndPosture();
    }

    public virtual void AddPostureDeflect(float p){
        this.currentPosture += p;
        this.currentPosture = Mathf.Clamp(this.currentPosture, 0, this.maxPosture-5);
        this.UpdateHPAndPosture();
    }

    public virtual void Die()
    {
        //Die in some way
        // this method is meant to be overwritter;
        Debug.Log(transform.name + " Die.");
    }
    protected IEnumerator DestroyAfter(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Instantiate(vfxDie, transform.position, transform.rotation);
        Destroy(gameObject);



    }

    protected IEnumerator DisableAfter(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Instantiate(vfxDie, transform.position, transform.rotation);
        //Destroy(gameObject);

        this.gameObject.SetActive(false);

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

    protected void ResetAllCurrentAndMaxValue(int maxHPValue, int currentAttackDameValue, int maxPostureValue/*, int currentDefendValue*/)
    {

        maxHP = maxHPValue;
        currentHP = maxHP;

        currentAttackDame = currentAttackDameValue;

        maxPosture = maxPostureValue;
        currentPosture = 0;//maxPosture;

        this.UpdateHPAndPosture();
        //currentDefend = currentDefendValue;
    }

    //Reduction

    private IEnumerator CanReduction(float waitTime)
    {


        yield return new WaitForSeconds(waitTime);
        reduction = true;
    }

    public void Reduction(float t)
    {
        if (actionReduction != null) StopCoroutine(actionReduction);
        reduction = false;
        actionReduction = StartCoroutine(CanReduction(t));
    }



}

[System.Serializable]
public class PhaseBossStats
{
    public PhaseBoss phaseBoss = PhaseBoss.Phase_1;
    public int HP = 100;
    public int Posture = 100;
    public GameObject[] listWeapon;
}
[System.Serializable]
public enum IsHit { Miss,Block,Hit};
