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
    private Animator animatorPlayer;
    public Transform target;//for Swing
    public Transform target2;//for Lock Target
    public Transform transformCamera;

    public float maxAngle;
    public float maxRadius = 20f;

    private bool isInFov = false;
    private bool isInFov2 = false;
    private string ableSwing = "SelectAbleSwing";
    private string ableTarget = "SelectAbleTarget";
    private Coroutine leaveAction;


    //public Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
    void Start()
    {
        player = GetComponent<PlayerController>();
        animatorPlayer = GetComponent<Animator>();
        //Camera.main.transform;
        //center = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        transformCamera = Camera.main.transform;
        //Debug.Log(Time.deltaTime);
        //isInFov = inFOV(transformCamera, target, maxAngle, maxRadius);
        isInFov = inFOVForSwing(transformCamera, maxAngle, maxRadius);// check target can Swing

        if (isInFov)
        {
            player.targetSwingDetect = target;
        }
        else
        {
            player.targetSwingDetect = null;
        }
        isInFov2 = inFOVForLockTarget(transformCamera, maxAngle, maxRadius);// check target can Swing

        if (isInFov2)
        {
            if (!animatorPlayer.GetBool("LockTarget"))
            {
                player.targetGroupCiner.m_Targets[1].target = target2;
            }
            
        }
        else
        {
            player.targetGroupCiner.m_Targets[1].target = null;

        }




    }


    public void OnDrawGizmos()
    {
        
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transformCamera.position, maxRadius);

        Vector3 fovLine1up = Quaternion.AngleAxis(maxAngle, transformCamera.up) * transformCamera.forward * maxRadius;
        Vector3 fovLine2down = Quaternion.AngleAxis(-maxAngle, transformCamera.up) * transformCamera.forward * maxRadius;
        Vector3 fovLine3right = Quaternion.AngleAxis(maxAngle, transformCamera.right) * transformCamera.forward * maxRadius;
        Vector3 fovLine4left = Quaternion.AngleAxis(-maxAngle, transformCamera.right) * transformCamera.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transformCamera.position, fovLine1up);
        Gizmos.DrawRay(transformCamera.position, fovLine2down);
        Gizmos.DrawRay(transformCamera.position, fovLine3right);
        Gizmos.DrawRay(transformCamera.position, fovLine4left);

        Gizmos.color = Color.green;


        if (target != null)
        {
            Gizmos.DrawRay(transformCamera.position, (target.position - transformCamera.position).normalized * maxRadius);

        }
        if (target2 != null)
        {
            Gizmos.DrawRay(transformCamera.position, (target2.position - transformCamera.position).normalized * maxRadius);

        }

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transformCamera.position, transformCamera.forward * maxRadius);
        
    }


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
               
                if (target == null)// if dont have target
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

                                target = overlaps[i].transform;
                                
                            }
                            
                        }

                    }


                }
                else  //aready have target
                {
                    Vector3 directionBetween = (target.position - checkingObj.position).normalized;
                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);
                    //Debug.Log(angle);
                    if (angle < maxAngleView)
                    {
                        Ray ray = new Ray(checkingObj.position, target.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView))
                        {
                            if (hit.transform != target)
                            {

                                target.gameObject.GetComponent<VisualEffect>().enabled = false;

                                target = null;
                                //havetarget = false;
                            }
                        }

                    }
                    else
                    {
                        target.gameObject.GetComponent<VisualEffect>().enabled = false;

                        target = null;
                      
                    }


                    Vector3 directionBetweenNew2 = (overlaps[i].transform.position - checkingObj.position).normalized;
                    float angleNew2 = Vector3.Angle(checkingObj.forward, directionBetweenNew2);

                    if (target == null && angleNew2 < maxRadiusView)
                    {
                        Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView))
                        {
                            if (hit.transform.CompareTag(ableSwing))
                            {
                                target = overlaps[i].transform;

                            }

                        }
                    }
                    else if (target != null && angleNew2 < angle)
                    {
                        Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView))
                        {
                            if (hit.transform.CompareTag(ableSwing))
                            {
                                target = overlaps[i].transform;

                            }

                        }
                    }




                }




            }


        }

        if (count == 0 && target != null)
        {

            target.gameObject.GetComponent<VisualEffect>().enabled = false;
            target = null;

        }

        if (target == null)
        {
            return false;
        }
        else
        {
            target.gameObject.GetComponent<VisualEffect>().enabled = true;

            return true;
        }
    }

    public bool inFOVForLockTarget(Transform checkingObj, float maxAngleView, float maxRadiusView)
    {
        
        int layer = 1 << 23;
        int layerRay = ~(1 << 24);
        
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadiusView, overlaps, layer);
        
        for (int i = 0; i < count; i++)
        {
            

            if (overlaps[i] != null)
            {
              
                if (target2 == null)// if dont have target
                {
                    
                    Vector3 directionBetween = (overlaps[i].transform.position - checkingObj.position).normalized;
                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);
                    if (angle < maxAngleView)
                    {
                        Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView, layerRay))
                        {
                            if (hit.transform.CompareTag(ableTarget))
                            {
                                //Debug.Log("sda");
                                target2 = overlaps[i].transform;

                            }

                        }

                    }


                }
                else  //aready have target
                {
                    Vector3 directionBetween = (target2.position - checkingObj.position).normalized;
                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);
                    //Debug.Log(angle);
                    if (angle < maxAngleView)
                    {
                        Ray ray = new Ray(checkingObj.position, target2.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView, layerRay))
                        {
                            if (hit.transform != target2)
                            {

                                if (animatorPlayer.GetBool("LockTarget"))
                                {
                                    player.LockTarget();
                                    
                                }
                                target2.Find("LockTarget").GetComponent<VisualEffect>().enabled = false;
                                target2 = null;
                            }
                        }

                    }
                    else
                    {
                        if (animatorPlayer.GetBool("LockTarget"))
                        {
                            player.LockTarget();

                        }
                        target2.Find("LockTarget").GetComponent<VisualEffect>().enabled = false;
                        target2 = null;
                    }


                    Vector3 directionBetweenNew2 = (overlaps[i].transform.position - checkingObj.position).normalized;
                    float angleNew2 = Vector3.Angle(checkingObj.forward, directionBetweenNew2);

                    if (target2 == null && angleNew2 < maxRadiusView)
                    {
                        Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView, layerRay))
                        {
                            if (hit.transform.CompareTag(ableTarget))
                            {
                                if (!animatorPlayer.GetBool("LockTarget"))
                                {
                                    target2.Find("LockTarget").GetComponent<VisualEffect>().enabled = false;
                                    target2 = overlaps[i].transform;

                                }

                            }

                        }
                    }
                    else if (target2 != null && angleNew2 < angle)
                    {
                        Ray ray = new Ray(checkingObj.position, overlaps[i].transform.position - checkingObj.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxRadiusView, layerRay))
                        {
                            if (hit.transform.CompareTag(ableTarget))
                            {
                                if (!animatorPlayer.GetBool("LockTarget"))
                                {

                                    target2.Find("LockTarget").GetComponent<VisualEffect>().enabled = false;
                                    target2 = overlaps[i].transform;

                                }
                                

                            }

                        }
                    }




                }


                //}


            }


        }

        if (count == 0 && target2 != null)
        {
            
            if (animatorPlayer.GetBool("LockTarget"))
            {
                player.LockTarget();

            }

            target2.Find("LockTarget").GetComponent<VisualEffect>().enabled = false;
            target2 = null;

        }

        if (target2 == null)
        {
            return false;
        }
        else
        {
            target2.Find("LockTarget").GetComponent<VisualEffect>().enabled = true;

            return true;
        }
    }



}
