using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(" to2" + other.gameObject.name);
        if (other.gameObject.layer == 24)
        {
            EnemyController enemyController = transform.root.GetComponent<EnemyController>();
            // touch Player
            if (enemyController.alertEnemy == AlertEnemy.Idle)
            {
                enemyController.alertEnemy = AlertEnemy.Warning;
                StartCoroutine(enemyController.LookAtAfter(0.2f));

            }
            else if (enemyController.alertEnemy == AlertEnemy.Warning)
            {
                StartCoroutine(enemyController.LookAtAfter(0.2f));

            }
        }
    }
}
