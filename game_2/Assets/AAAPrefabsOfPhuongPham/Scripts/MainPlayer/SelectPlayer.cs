using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SelectPlayer : SelectManager
{

    public PlayerController player;
    public Transform targetSwing;//for Swing

    protected bool isInFov = false;
    public string ableSwing = "SelectAbleSwing";
    public string ableTarget = "SelectAbleTarget";

    public float radiusInCombat = 20f;

    private void Start()
    {
        animatorCharacter = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckInCombatOrNot();

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

        if (!animatorCharacter.GetBool("LockTarget"))// Don't Lock Target
        {
            if (targetEnemy != null && (player.targetGroupCiner.m_Targets[1].target == null || targetEnemy.gameObject.GetInstanceID() != player.targetGroupCiner.m_Targets[1].target.gameObject.GetInstanceID())) // have current target but have new target closer center point
            {
                //currentTargetEnemy = targetEnemy;
                player.targetGroupCiner.m_Targets[1].target = targetEnemy.GetChild(0);
            }
            else if (targetEnemy == null && player.targetGroupCiner.m_Targets[1].target != null)
            {
                player.targetGroupCiner.m_Targets[1].target = null;
            }


        }



    }
    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radiusInCombat );
        */
    }
    void CheckInCombatOrNot()
    {
        Collider[] overlaps = new Collider[1];
        int count = Physics.OverlapSphereNonAlloc(transform.position, radiusInCombat, overlaps, layerEnemyTarget);

        if (count == 0 && player.onCombat)
        {

            player.OnCombat(false);

        }
        else if (count > 0 && !player.onCombat)
        {
            player.OnCombat(true);

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
                            if (hit.transform.gameObject.GetInstanceID() != targetSwing.gameObject.GetInstanceID())
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


    public override void inFOVForLockTarget(Transform checkingObj, float maxAngleView, float maxRadiusView)
    {
        //base.inFOVForLockTarget(checkingObj, maxAngleView, maxRadiusView);
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

        if (targetEnemy != null) // check if current target avable or not
        {

            Vector3 directionBetween = (targetEnemy.position - checkingObj.position).normalized;
            float angle = Vector3.Angle(checkingObj.forward, directionBetween);
            bool removeT = false;

            if (angle < maxAngleView) // check if still in angle
            {
                //Ray ray = new Ray(checkingObj.position, targetEnemy.position - checkingObj.position);
                float distan = Vector3.Distance(checkingObj.position, targetEnemy.position);
                if (distan < maxRadiusView)
                {


                    RaycastHit hit;
                    //if (Physics.Raycast(ray, out hit, maxRadiusView, layerRatcastToTarget))
                    if (Physics.Linecast(checkingObj.position, targetEnemy.position, out hit, layerLineCastToTarget))
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
                else
                {
                    removeT = true;
                }

            }
            else // if not in angle
            {
                removeT = true;
            }

            if (removeT)
            {
                //Debug.Log("remove");
                if (animatorCharacter.GetBool("LockTarget"))
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
                //Debug.Log(i);

                if (targetEnemy != null)
                {
                    //Debug.Log("Have Target");


                    if (overlaps[i].transform.gameObject.GetInstanceID() == targetEnemy.gameObject.GetInstanceID()) //  new and old target is the same
                    {
                        continue;
                    }
                    //Debug.Log("Have Target");

                    // we have 2 target and we need find which closer center point

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
                        if (Physics.Linecast(checkingObj.position, overlaps[i].transform.position, out hit, layerLineCastToTarget))
                        {
                            if (hit.transform.CompareTag(ableTarget))
                            {
                                //Debug.DrawLine(checkingObj.position, overlaps[i].transform.position,Color.black);

                                if (!animatorCharacter.GetBool("LockTarget"))
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
                    if (Physics.Linecast(checkingObj.position, overlaps[i].transform.position, out hit, layerLineCastToTarget))
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

            if (animatorCharacter.GetBool("LockTarget"))
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
