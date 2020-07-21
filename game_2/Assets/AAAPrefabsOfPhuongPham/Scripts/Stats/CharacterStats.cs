using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class CharacterStats : MonoBehaviour
{

    // !Source Start
    [Header("Source Start")]
    public int startHP = 100;
    public int startAttackDame = 0;
    public int startPosture = 100;
    public int startDefend = 0;

    // !Max and current Stat
    [Header("Max and Current Stat")]
    public int maxHP = 0;
    public int currentHP { get; protected set; }
    public int currentAttackDame { get; protected set; }
    public int maxPosture = 0;
    public int currentPosture { get; protected set; }
    public int currentDefend { get; protected set; }

    public Vector3 teleportNearest { get; protected set; }

    // !Stats
    [Header("Stats")]
    public Stat hp; // HP
    public Stat attackDame; // AttacK Dame
    public Stat posture; // Posture
    public Stat defend; // Defend


    //public Stat damage; // old damage
    //public Stat armor; // old Armor

    protected Animator animator;

    private void Awake()
    {
        //currentHealth = maxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamege(90);
        }
    }

    public virtual void TakeDamege(int damage)
    {
        if (currentPosture != maxPosture)
        {
            if (animator.GetInteger("InAction") == 6)
            {
                animator.SetTrigger("InBlock");

            }
        }
        else
        {
            Debug.Log("can't block anymore");
        }
        if (animator.GetInteger("InAction") == 6) // ! In Block
        {
            damage -= currentDefend;
            damage = Mathf.Clamp(damage, 0, 999999);

            currentPosture += damage;
            currentPosture = Mathf.Clamp(currentPosture, 0, maxPosture);

            Debug.Log(transform.name + " Posture plus " + damage + " damege.");



        }
        else
        { // ! Not Block
            currentPosture += damage;

            damage -= currentDefend;
            damage = Mathf.Clamp(damage, 0, 999999);

            currentHP -= damage;

            Debug.Log(transform.name + " HP Take " + damage + " damege.");
            Debug.Log(transform.name + " Posture plus " + damage + " damege.");
        }


        if (currentHP <= 0)
        {
            Die();

        }
    }

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
        //cube.transform.position = teleportNearest;
        //transform.position = teleportNearest;

        transform.TransformPoint(teleportNearest);
    }
    public GameObject cube;
}
