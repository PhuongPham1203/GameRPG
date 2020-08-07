using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SelectEnemy : SelectManager
{
    public EnemyController enemyController;
    private PlayerController playerController;

    private string ablePlayer = "Player";

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();

        //transformRoot = transform;
        //animatorCharacter = GetComponent<Animator>();
    }

    private void Update()
    {
        if(enemyController.alertEnemy != AlertEnemy.Die)
        {
            inFOVForLockTarget(transformRoot, maxAngle, maxRadius);

        }
    }
    public override void inFOVForLockTarget(Transform checkingObj, float maxAngleView, float maxRadiusView)
    {
        //base.inFOVForLockTarget(checkingObj, maxAngleView, maxRadiusView);

        Collider[] overlaps = new Collider[4];
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadiusView, overlaps, layerEnemyTarget);
        //Debug.Log(transform.name + " Find : " + count);



        if (targetEnemy != null)// have current target
        {

            Vector3 directionBetween = (targetEnemy.position - checkingObj.position).normalized;
            float angle = Vector3.Angle(checkingObj.forward, directionBetween);
            bool removeT = false;
            /*
            if (angle < maxAngleView) // check if still in angle
            {
            */
            float distan = Vector3.Distance(checkingObj.position, targetEnemy.position);
            if (distan < maxRadiusView)
            {
                //Ray ray = new Ray(checkingObj.position, targetEnemy.position - checkingObj.position);
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

            /*

            }
            else // if not in angle
            {
                removeT = true;
            }
            */

            if (removeT)
            {


                enemyController.SetAlentCombat(AlertEnemy.Warning);
                targetEnemy = null;

                if (AudioManager.instance.IsPlayTheme("OnCombat"))
                {
                    AudioManager.instance.StopSoundOfTheme("OnCombat");
                }
               
            }
        }

        for (int i = 0; i < count; i++)
        {
            if (overlaps[i] != null)
            {
                
                //Debug.Log(i);

                if (targetEnemy != null)
                {
                    if (overlaps[i].transform.gameObject.GetInstanceID() == targetEnemy.gameObject.GetInstanceID()) //  new and old target is the same
                    {
                        continue;
                    }
                    //Debug.Log("Have Target");



                }
                else //(targetEnemy == null)
                {

                    Vector3 directionBetween0 = (overlaps[i].transform.position - checkingObj.position).normalized;
                    float angleInView0 = Vector3.Angle(checkingObj.forward, directionBetween0); // check angle over view or not

                    if (angleInView0 >= maxAngleView) // if it over view ==> continue
                    {
                        continue;
                    }

                    //Debug.Log("Don't Have Target");
                    //Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);

                    RaycastHit hit;
                    //if (Physics.Raycast(ray, out hit, maxRadiusView, layerLineCastToTarget))
                    if (Physics.Linecast(checkingObj.position, overlaps[i].transform.position, out hit, layerLineCastToTarget))
                    {
                        //Debug.Log(" Layer : " + hit.transform.gameObject.layer + "Hit : " + hit.transform.name); 
                        if (hit.transform.CompareTag(ablePlayer))
                        {
                            //Debug.DrawLine(checkingObj.position, overlaps[i].transform.position,Color.yellow);
                            //targetEnemy = overlaps[i].transform;
                            //targetEnemy.GetChild(1).GetComponent<VisualEffect>().enabled = true;

                            targetEnemy = PlayerManager.instance.player.transform;
                            enemyController.SetAlentCombat(AlertEnemy.OnTarget);

                            if (AudioManager.instance.IsPlayTheme("OnCombat"))
                            {

                            }
                            else
                            {
                                AudioManager.instance.PlaySoundOfTheme("OnCombat");
                            }
                        }

                    }



                }


            }
            else // overlaps[i] == null
            {
                break;
            }

        }


        /*
        if (count > 0)
        {
            if (!playerController.onCombat)
            {
                Debug.Log("set on combat");

                enemyController.SetPlayerOnCombat(true);
                //playerController.OnCombat(true);
            }
        }
        else if (playerController.onCombat)
        {
            Debug.Log("set not on combat");

            enemyController.SetPlayerOnCombat(false);

        }
        */


    }


}
