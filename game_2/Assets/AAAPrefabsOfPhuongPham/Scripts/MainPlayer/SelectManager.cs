using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class SelectManager : MonoBehaviour
{

    public PlayerController player;
    public Animator animatorPlayer;
    public Transform targetSwing;//for Swing
    public Transform targetEnemy;//for Lock Target
    protected Transform currentTargetEnemy;
    public Transform transformRoot;

    public float maxAngle = 30f;
    public float maxRadius = 20f;

    private bool isInFov = false;
    //private bool isInFov2 = false;
    private string ableSwing = "SelectAbleSwing";
    private string ableTarget = "SelectAbleTarget";
    //private Coroutine leaveAction;

    public LayerMask layerEnemyTarget ;
    public LayerMask layerLineCastToTarget ;


    //public Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
    void Start()
    {
        //player = GetComponent<PlayerController>();
        //animatorPlayer = GetComponent<Animator>();
        //Camera.main.transform;
        //center = new Vector3(Screen.width / 2, Screen.height / 2
        //selectIgnore = ( 1 << 24 ) & (1 << 2);
    }

    private void Update()
    {
        transformRoot = Camera.main.transform;
        //Debug.Log(Time.deltaTime);
        //isInFov = inFOV(transformRoot, target, maxAngle, maxRadius);
        isInFov = inFOVForSwing(transformRoot, maxAngle, maxRadius);// check target can Swing

        if (isInFov)
        {
            if (player.targetSwingDetect != targetSwing)
            {
                player.targetSwingDetect = targetSwing;
                player.buttonSwing.SetActive(true);
            }

        }
        else if (player.targetSwingDetect != null)
        {
            player.targetSwingDetect = null;
            player.buttonSwing.SetActive(false);

        }

        inFOVForLockTarget(transformRoot, maxAngle, maxRadius);// check target can Lock

        if (!animatorPlayer.GetBool("LockTarget"))// Don't Lock Target
        {
            if (targetEnemy != null && ( player.targetGroupCiner.m_Targets[1].target == null || targetEnemy.gameObject.GetInstanceID() != player.targetGroupCiner.m_Targets[1].target.gameObject.GetInstanceID()) ) // have current target but have new target closer center point
            {
                //currentTargetEnemy = targetEnemy;
                player.targetGroupCiner.m_Targets[1].target = targetEnemy.GetChild(0);
            }else if(targetEnemy == null && player.targetGroupCiner.m_Targets[1].target!=null)
            {
                player.targetGroupCiner.m_Targets[1].target = null;
            }

            /*
            else if (targetEnemy == null && player.targetGroupCiner.m_Targets[1].target != null)
            {
                //targetEnemy = null;
                //currentTargetEnemy = null;
                player.targetGroupCiner.m_Targets[1].target = null;

            }
            */

        }
        /*
        else if (targetEnemy == null && player.targetGroupCiner.m_Targets[1].target != null) // On Lock target Enemy
        {

            //currentTargetEnemy = null;
            player.targetGroupCiner.m_Targets[1].target = null;
            player.LockTarget();

        }
        */
        /*
        isInFov2 = inFOVForLockTarget(transformRoot, maxAngle, maxRadius);// check target can Lock
        if (!animatorPlayer.GetBool("LockTarget"))// Don't Lock Target
        {
            if (isInFov2 && player.targetGroupCiner.m_Targets[1].target != targetEnemy.GetChild(0)) // have current target but have new target closer center point
            {
                //currentTargetEnemy = targetEnemy;
                player.targetGroupCiner.m_Targets[1].target = targetEnemy.GetChild(0);
            }
            else if (!isInFov2 && player.targetGroupCiner.m_Targets[1].target != null)
            {
                //targetEnemy = null;
                //currentTargetEnemy = null;
                player.targetGroupCiner.m_Targets[1].target = null;

            }

        }
        else if (!isInFov2) // On Lock target Enemy
        {

            //currentTargetEnemy = null;
            player.targetGroupCiner.m_Targets[1].target = null;
            player.LockTarget();

        }

        */


    }


    public void OnDrawGizmos()//Test Camera
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




        if (targetSwing != null)
        {
            Gizmos.color = Color.green;
            //Gizmos.DrawRay(transformRoot.position, (targetSwing.position - transformRoot.position).normalized * maxRadius);
            Gizmos.DrawLine(transformRoot.position, targetSwing.position );

        }

        if (targetEnemy != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transformRoot.position, targetEnemy.position );

        }


    }


    // For Swing
    public bool inFOVForSwing(Transform checkingObj, float maxAngleView, float maxRadiusView)
    {
        int layer = 1 << 22;
        //int layerRay = ~(1 << 24);
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadiusView, overlaps, layer);

        for (int i = 0; i < count; i++)
        {
            if (overlaps[i] != null)
            {

                if (targetSwing == null)// if dont have target
                {
                    Vector3 directionBetween = (overlaps[i].transform.position - checkingObj.position).normalized;
                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);
                    if (angle < maxAngleView)
                    {
                        Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView))
                        {
                            if (hit.transform.CompareTag(ableSwing))
                            {

                                targetSwing = overlaps[i].transform;

                            }

                        }

                    }


                }
                else  //aready have target
                {
                    Vector3 directionBetween = (targetSwing.position - checkingObj.position).normalized;
                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);
                    //Debug.Log(angle);
                    if (angle < maxAngleView)
                    {
                        Ray ray = new Ray(checkingObj.position, targetSwing.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView))
                        {
                            if (hit.transform != targetSwing)
                            {

                                targetSwing.gameObject.GetComponent<VisualEffect>().enabled = false;

                                targetSwing = null;
                                //havetarget = false;
                            }
                        }

                    }
                    else
                    {
                        targetSwing.gameObject.GetComponent<VisualEffect>().enabled = false;

                        targetSwing = null;

                    }


                    Vector3 directionBetweenNew2 = (overlaps[i].transform.position - checkingObj.position).normalized;
                    float angleNew2 = Vector3.Angle(checkingObj.forward, directionBetweenNew2);

                    if (targetSwing == null && angleNew2 < maxAngleView)
                    {
                        Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView))
                        {
                            if (hit.transform.CompareTag(ableSwing))
                            {
                                targetSwing = overlaps[i].transform;

                            }

                        }
                    }
                    else if (targetSwing != null && angleNew2 < angle) // !check if newTarget close than targetNow
                    {
                        Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView))
                        {
                            if (hit.transform.CompareTag(ableSwing))
                            {
                                targetSwing = overlaps[i].transform;

                            }

                        }
                    }




                }




            }
            else
            {
                break;
            }


        }

        if (count == 0 && targetSwing != null)
        {

            targetSwing.gameObject.GetComponent<VisualEffect>().enabled = false;
            targetSwing = null;

        }

        if (targetSwing == null)
        {
            return false;
        }
        else
        {
            targetSwing.gameObject.GetComponent<VisualEffect>().enabled = true;

            return true;
        }
    }

    // For Lock target Enemy
    public void inFOVForLockTarget(Transform checkingObj, float maxAngleView, float maxRadiusView)
    {

        //int layer = 1 << 23; // Find Only Enemy ( Layer 23 )
        //int layerRay = ~((1 << 24) | (1 << 2)); // For ray cast find something between theme and ignore some layer ( 24 and 2 )
        //layerRay = ~layerRay;
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadiusView, overlaps, layerEnemyTarget);
        //Debug.Log("Number Enemy Find : " + count);


        //if (GameObject.ReferenceEquals(overlaps[i].transform.gameObject, targetEnemy.gameObject))
        //if (overlaps[i].gameObject.GetInstanceID() == targetEnemy.gameObject.GetInstanceID())
        //{
        // Check if current target is avalible or not

        if (targetEnemy!=null) // check if current target avable or not
        {

            Vector3 directionBetween = (targetEnemy.position - checkingObj.position).normalized;
            float angle = Vector3.Angle(checkingObj.forward, directionBetween);
            bool removeT = false;

            if (angle < maxAngleView) // check if still in angle
            {
                //Ray ray = new Ray(checkingObj.position, targetEnemy.position - checkingObj.position);
                RaycastHit hit;
                //if (Physics.Raycast(ray, out hit, maxRadiusView, layerRatcastToTarget))
                if(Physics.Linecast(checkingObj.position, targetEnemy.position,out hit,layerLineCastToTarget))
                {
                    //if (!GameObject.ReferenceEquals(hit.transform.gameObject, targetEnemy.gameObject)) // Check if have something between target and player => remove target
                    if (hit.transform.gameObject.GetInstanceID() != targetEnemy.gameObject.GetInstanceID())
                    {
                        //Debug.DrawLine(checkingObj.position, targetEnemy.position);

                        //Debug.Log(hit.transform.name);
                        removeT = true;

                    }

                }

            }
            else // if not in angle
            {
                removeT = true;
            }

            if (removeT)
            {
                //Debug.Log("remove");
                if (animatorPlayer.GetBool("LockTarget"))
                {
                    player.LockTarget();
                }
                targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = false;
                targetEnemy = null;
            }/*
            else
            {
                Debug.Log("not remove");

            }*/
            // End check current target is avalible or not
            //ontinue;
            //
        }


        //Debug.Log(overlaps[1]);
        for (int i = 0; i < count; i++)
        {

            if (overlaps[i] != null)
            {
                
                Vector3 directionBetween0 = (overlaps[i].transform.position - checkingObj.position).normalized;
                float angleInView0 = Vector3.Angle(checkingObj.forward, directionBetween0); // check angle over view or not

                if (angleInView0 >= maxAngleView) // if it over view ==> continue
                {
                    continue;
                }
                Debug.Log(i);

                if (targetEnemy != null)
                {
                    //Debug.Log("Have Target");


                    if(overlaps[i].transform.gameObject.GetInstanceID() == targetEnemy.gameObject.GetInstanceID()) //  new and old target is the same
                    {
                        continue;
                    }
                    Debug.Log("Have Target");



                    Vector3 directionBetween = (targetEnemy.position - checkingObj.position).normalized;
                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);

                    // Check if overlaps[i] is target 
                    Vector3 directionBetween1 = (overlaps[i].transform.position - checkingObj.position).normalized;
                    float angleInView = Vector3.Angle(checkingObj.forward, directionBetween1); // check angle over view or not

                    if (angleInView < angle)//|| angleInView < maxRadiusView )
                    {
                        //Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        //if (Physics.Raycast(ray, out hit, maxRadiusView, layerLineCastToTarget))
                        if(Physics.Linecast(checkingObj.position,overlaps[i].transform.position,out hit,layerLineCastToTarget))
                        {
                            if (hit.transform.CompareTag(ableTarget))
                            {
                                //Debug.DrawLine(checkingObj.position, overlaps[i].transform.position,Color.black);

                                if (!animatorPlayer.GetBool("LockTarget"))
                                {
                                    targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = false; // disable old targetEnemy
                                    targetEnemy = overlaps[i].transform;
                                    targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = true; // enable new targetEnemy

                                }


                            }

                        }
                    }
                    // End check current target and new target what closer with center point more 



                    // End if dont have target
                }
                else //(targetEnemy == null)
                {


                    //Debug.Log("Don't Have Target");
                    //Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);

                    RaycastHit hit;
                    //if (Physics.Raycast(ray, out hit, maxRadiusView, layerLineCastToTarget))
                    if(Physics.Linecast(checkingObj.position, overlaps[i].transform.position,out hit, layerLineCastToTarget))
                    {
                        //Debug.Log(" Layer : " + hit.transform.gameObject.layer + "Hit : " + hit.transform.name); 
                        if (hit.transform.CompareTag(ableTarget))
                        {
                            //Debug.DrawLine(checkingObj.position, overlaps[i].transform.position,Color.yellow);
                            targetEnemy = overlaps[i].transform;
                            targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = true;

                        }

                    }



                }


            }
            else // overlaps[i] == null
            {
                break;
            }


        }


        if (count == 0 || targetEnemy == null)
        {

            if (animatorPlayer.GetBool("LockTarget"))
            {
                player.LockTarget();

            }
            if (targetEnemy != null)
            {
                targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = false;
                targetEnemy = null;

            }


        }

        /*
        if (targetEnemy == null)
        {
            return false;
        }
        else
        {
            return true;
        }
        */

    }



    // For Lock target Enemy
    public void inFOVForLockTargetOld(Transform checkingObj, float maxAngleView, float maxRadiusView)
    {

        //int layer = 1 << 23; // Find Only Enemy ( Layer 23 )
        //int layerRay = ~((1 << 24) | (1 << 2)); // For ray cast find something between theme and ignore some layer ( 24 and 2 )
        //layerRay = ~layerRay;
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadiusView, overlaps, layerEnemyTarget);
        Debug.Log("Number Enemy Find : " + count);


        //if (GameObject.ReferenceEquals(overlaps[i].transform.gameObject, targetEnemy.gameObject))
        //if (overlaps[i].gameObject.GetInstanceID() == targetEnemy.gameObject.GetInstanceID())
        //{
        // Check if current target is avalible or not

        if (targetEnemy != null) // check if current target avable or not
        {

            Vector3 directionBetween = (targetEnemy.position - checkingObj.position).normalized;
            float angle = Vector3.Angle(checkingObj.forward, directionBetween);
            bool removeT = false;

            if (angle < maxAngleView) // check if still in angle
            {
                Ray ray = new Ray(checkingObj.position, targetEnemy.position - checkingObj.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, maxRadiusView, layerLineCastToTarget))
                {
                    //if (!GameObject.ReferenceEquals(hit.transform.gameObject, targetEnemy.gameObject)) // Check if have something between target and player => remove target
                    if (hit.transform.gameObject.GetInstanceID() != targetEnemy.gameObject.GetInstanceID())
                    {
                        Debug.DrawRay(checkingObj.position, targetEnemy.position - checkingObj.position);

                        Debug.Log(hit.transform.name);
                        removeT = true;
                        /*
                        if (animatorPlayer.GetBool("LockTarget"))
                        {
                            player.LockTarget();
                        }
                        targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = false;
                        targetEnemy = null;
                        */

                    }

                }

            }
            else // if not in angle
            {
                removeT = true;
                /*
                if (animatorPlayer.GetBool("LockTarget"))
                {
                    player.LockTarget();
                }
                targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = false;
                targetEnemy = null;
                */

            }

            if (removeT)
            {
                Debug.Log("remove");
                if (animatorPlayer.GetBool("LockTarget"))
                {
                    player.LockTarget();
                }
                targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = false;
                targetEnemy = null;
            }
            else
            {
                Debug.Log("not remove");

            }
            // End check current target is avalible or not
            //ontinue;
            //
        }


        //Debug.Log(overlaps[1]);
        for (int i = 0; i < count; i++)
        {

            if (overlaps[i] != null)
            {

                Vector3 directionBetween0 = (overlaps[i].transform.position - checkingObj.position).normalized;
                float angleInView0 = Vector3.Angle(checkingObj.forward, directionBetween0); // check angle over view or not

                if (angleInView0 >= maxAngleView) // if it over view ==> continue
                {
                    continue;
                }
                Debug.Log(i);

                if (targetEnemy != null)
                {
                    Debug.Log("Have Target");


                    if (overlaps[i].transform.gameObject.GetInstanceID() == targetEnemy.gameObject.GetInstanceID()) //  new and old target is the same
                    {
                        continue;
                    }



                    Vector3 directionBetween = (targetEnemy.position - checkingObj.position).normalized;
                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);

                    // Check if overlaps[i] is target 
                    Vector3 directionBetween1 = (overlaps[i].transform.position - checkingObj.position).normalized;
                    float angleInView = Vector3.Angle(checkingObj.forward, directionBetween1); // check angle over view or not

                    if (angleInView < angle)//|| angleInView < maxRadiusView )
                    {
                        Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView, layerLineCastToTarget))
                        {
                            if (hit.transform.CompareTag(ableTarget))
                            {
                                if (!animatorPlayer.GetBool("LockTarget"))
                                {
                                    targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = false; // disable old targetEnemy
                                    targetEnemy = overlaps[i].transform;
                                    targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = true; // enable new targetEnemy

                                }


                            }

                        }
                    }
                    // End check current target and new target what closer with center point more 



                    // End if dont have target
                }
                else //(targetEnemy == null)
                {


                    Debug.Log("Don't Have Target");
                    Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, maxRadiusView, layerLineCastToTarget))
                    {
                        //Debug.Log(" Layer : " + hit.transform.gameObject.layer + "Hit : " + hit.transform.name); 
                        if (hit.transform.CompareTag(ableTarget))
                        {
                            targetEnemy = overlaps[i].transform;
                            targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = true;

                        }

                    }



                }


            }
            else // overlaps[i] == null
            {
                break;
            }


        }


        if (count == 0 || targetEnemy == null)
        {

            if (animatorPlayer.GetBool("LockTarget"))
            {
                player.LockTarget();

            }
            if (targetEnemy != null)
            {
                targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = false;
                targetEnemy = null;

            }


        }

        /*
        if (targetEnemy == null)
        {
            return false;
        }
        else
        {
            return true;
        }
        */

    }


}
