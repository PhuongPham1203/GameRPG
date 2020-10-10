using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangeCenter : MonoBehaviour
{
    [Header("Range")]
    public float range = 5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,range);
        
    }
}
