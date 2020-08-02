using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyController
{

    private void Update()
    {
        FOVDetectTarget();
        if (alertEnemy == AlertEnemy.OnTarget) // have target
        {
            // Action with Target When OnCombat
            distance = Vector3.Distance(target.position, transform.position);
            if(distance > distanceCanAttack){
                MoveToTarget();    
            }else if (distance <= distanceCanAttack)
            {
                animator.SetFloat("SpeedMove", 0);

                if (canAction)
                {
                    
                    Attack(1);
                }
            }

        }
        else if (alertEnemy == AlertEnemy.Warning) // Warning
        {
            // Action When Waring
        }
        else if (alertEnemy == AlertEnemy.Idle) //Idle
        {
            // Action When Idle

        }

    }


    


}
