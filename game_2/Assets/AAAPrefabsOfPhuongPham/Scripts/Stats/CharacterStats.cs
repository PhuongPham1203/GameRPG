using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    // Source Start
    public int startHP = 100;
    public int startAttackDame = 0;
    public int startPosture = 100;
    public int startDefend = 0;

    //

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat hp; // HP
    public Stat attackDame; // AttacK Dame
    public Stat posture; // Posture
    public Stat defend; // Defend


    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        currentHealth = maxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamege(90);
        }
    }

    public void TakeDamege(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " Take " + damage + " damege.");

        if (currentHealth <= 0)
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
