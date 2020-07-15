using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyController
{

    private void Update()
    {
        FOVDetectTarget();
        if (alert == 2) // have target
        {
            // Action with Target When OnCombat
            distance = Vector3.Distance(target.position, transform.position);
            if(distance > distanceCanAttack){
                MoveToTarget();    
            }

        }else if (alert == 1) // Warning
        {
            // Action When Waring
        }
        else if (alert == 0) //Idle
        {
            // Action When Idle
        }
        
    }




}
