using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxEnemy : Hitbox
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 24)
        {
            //Debug.Log(characterStats.transform.name + " hitbox "+ characterStats.GetAttackDame(1));
            //other.gameObject.GetComponent<PlayerStats>().TakeDamege(characterStats.GetAttackDame(1));
            other.gameObject.GetComponent<PlayerStats>().TakeDamege(  transform.root.GetComponent<CharacterStats>().GetAttackDame(69)  );
            
        }
    }

}
    