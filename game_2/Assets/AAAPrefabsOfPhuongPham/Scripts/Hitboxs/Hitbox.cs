using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [Range(0,3f)]
    public float timeDetroy=0.25f;

    public CharacterStats characterStats;
    private void Awake()
    {
        characterStats = PlayerManager.instance.player.GetComponent<CharacterStats>();

        Destroy(gameObject, timeDetroy);
    }




}
