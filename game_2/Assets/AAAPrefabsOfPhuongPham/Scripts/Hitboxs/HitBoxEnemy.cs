using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxEnemy : Hitbox
{
    private void Awake()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 24 || other.gameObject.layer == 23)
        {
            //Debug.Log(characterStats.transform.name + " hitbox "+ characterStats.GetAttackDame(1));
            //other.gameObject.GetComponent<PlayerStats>().TakeDamage(characterStats.GetAttackDame(1));
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(  999999,10,AttackTypeEffect.Dead,new EnemyController() );
            
        }
    }

}
    