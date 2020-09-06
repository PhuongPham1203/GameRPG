using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class SelectManager : MonoBehaviour
{


    public Animator animatorCharacter;
    public Transform transformRoot;
    public Transform targetEnemy;//for Lock Target
    //protected Transform currentTargetEnemy;

    public float maxAngle = 30f;
    public float maxRadius = 20f;

    //private bool isInFov2 = false;

    //private Coroutine leaveAction;

    public LayerMask layerEnemyTarget;
    public LayerMask layerLineCastToTarget;




    public void OnDrawGizmosSelected()//Test Camera
    {

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transformRoot.position, transformRoot.forward * maxRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transformRoot.position, maxRadius);

        Vector3 fovLine1up = Quaternion.AngleAxis(maxAngle, transformRoot.up) * transformRoot.forward * maxRadius;
        Vector3 fovLine2down = Quaternion.AngleAxis(-maxAngle, transformRoot.up) * transformRoot.forward * maxRadius;
        Vector3 fovLine3right = Quaternion.AngleAxis(maxAngle, transformRoot.right) * transformRoot.forward * maxRadius;
        Vector3 fovLine4left = Quaternion.AngleAxis(-maxAngle, transformRoot.right) * transformRoot.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transformRoot.position, fovLine1up);
        Gizmos.DrawRay(transformRoot.position, fovLine2down);
        Gizmos.DrawRay(transformRoot.position, fovLine3right);
        Gizmos.DrawRay(transformRoot.position, fovLine4left);

        //draw in end
        Gizmos.DrawLine(transformRoot.position + fovLine1up, transformRoot.position + fovLine3right);
        Gizmos.DrawLine(transformRoot.position + fovLine3right, transformRoot.position + fovLine2down);
        Gizmos.DrawLine(transformRoot.position + fovLine2down, transformRoot.position + fovLine4left);
        Gizmos.DrawLine(transformRoot.position + fovLine4left, transformRoot.position + fovLine1up);






        /*
        if (targetSwing != null)
        {
            Gizmos.color = Color.green;
            //Gizmos.DrawRay(transformRoot.position, (targetSwing.position - transformRoot.position).normalized * maxRadius);
            Gizmos.DrawLine(transformRoot.position, targetSwing.position );

        }
        */

        if (targetEnemy != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transformRoot.position, targetEnemy.position);

        }


    }



    // For Lock target Enemy
    public virtual void inFOVForLockTarget(Transform checkingObj, float maxAngleView, float maxRadiusView)
    {



    }


}
