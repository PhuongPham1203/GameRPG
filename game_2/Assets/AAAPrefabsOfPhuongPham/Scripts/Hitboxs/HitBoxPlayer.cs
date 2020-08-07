using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxPlayer : Hitbox
{
    [Header("Weapon : 1-Light 2-Heavy")]
    [Range(1,2)]
    public int weapon = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 23)
        {
            //Debug.Log(characterStats.transform.name + " hitbox "+ characterStats.GetAttackDame(1));
            //other.gameObject.GetComponent<PlayerStats>().TakeDamege(characterStats.GetAttackDame(1));
            other.gameObject.GetComponent<CharacterStats>().TakeDamege(PlayerManager.instance.player.GetComponent<CharacterStats>().GetAttackDame(weapon));

        }
    }

}
