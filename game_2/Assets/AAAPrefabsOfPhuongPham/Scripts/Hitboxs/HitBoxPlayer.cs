using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxPlayer : Hitbox
{
    [Header("Weapon : 1-Light 2-Heavy")]
    [Range(1,2)]
    public int weapon = 1;

    private void Awake()
    {
        characterStats = PlayerManager.instance.player.GetComponent<CharacterStats>();

        Destroy(gameObject, timeDetroy);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 23)
        {
            //Debug.Log(characterStats.transform.name + " hitbox "+ characterStats.GetAttackDame(1));
            //other.gameObject.GetComponent<PlayerStats>().TakeDamege(characterStats.GetAttackDame(1));
            //Debug.Log("Hitbox Player:"+ characterStats.GetAttackDame(weapon));
            other.gameObject.GetComponent<CharacterStats>().TakeDamege(characterStats.GetAttackDame(weapon));

        }
    }

}
