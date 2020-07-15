using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //public int maxAttackDame = 0;
    public int currentAttackDame { get; protected set; }
    public int maxPosture = 0;
    public int currentPosture { get; protected set; }
    //public int maxDefend = 0;
    public int currentDefend { get; protected set; }

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
}
