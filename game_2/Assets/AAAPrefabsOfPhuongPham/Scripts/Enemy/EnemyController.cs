using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [Header("Information")]
    public float moveSpeed = 4f;
    public float lookRadius = 10f; // Range view look
    public float lookAngle = 45f; // Angle look

    [Header("Speed Tracking LookAt Target")]
    [Range(0.1f, 10f)]
    public double speedTrackTarget = 3f;
    public float distanceCanAttack;
    public float distance = 0f;
    [Range(2f, 15f)]
    public float timeLeaveWaring = 5f;
    protected Coroutine actionLeaveWaring;
    protected bool isInFov = false;
    public Transform target;
    protected PlayerController playerController;
    protected CharacterController characterController;
    public TextMesh textAlert;
    private string ablePlayer = "Player";

    [Header("Alent : 0-Idle 1:Warning 2-OnTarget")]
    [Range(0, 2)]
    public int alert = 0;


    // Start is called before the first frame update
    void Start()
    {
        //SetTarget(0);
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();

    }


    void LateUpdate()
    {
        if (alert == 2)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * (float)speedTrackTarget);

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * lookRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Vector3 fovLine1up = Quaternion.AngleAxis(lookAngle, transform.up) * transform.forward * lookRadius;
        Vector3 fovLine2down = Quaternion.AngleAxis(-lookAngle, transform.up) * transform.forward * lookRadius;
        Vector3 fovLine3right = Quaternion.AngleAxis(lookAngle, transform.right) * transform.forward * lookRadius;
        Vector3 fovLine4left = Quaternion.AngleAxis(-lookAngle, transform.right) * transform.forward * lookRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1up);
        Gizmos.DrawRay(transform.position, fovLine2down);
        Gizmos.DrawRay(transform.position, fovLine3right);
        Gizmos.DrawRay(transform.position, fovLine4left);

        //draw in end
        Gizmos.DrawLine(transform.position + fovLine1up, transform.position + fovLine3right);
        Gizmos.DrawLine(transform.position + fovLine3right, transform.position + fovLine2down);
        Gizmos.DrawLine(transform.position + fovLine2down, transform.position + fovLine4left);
        Gizmos.DrawLine(transform.position + fovLine4left, transform.position + fovLine1up);

        if (target != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position, (target.position - transform.position).normalized * lookRadius);

        }
    }


    #region Singleton
    public virtual void FOVDetectTarget()
    {
        //transform , lookRadius, lookAngle
        int layer = 1 << 24; // find only Player
        //int layerRay = ~(1 << 24);
        Collider[] overlaps = new Collider[4];
        int count = Physics.OverlapSphereNonAlloc(transform.position, lookRadius, overlaps, layer);
        //Debug.Log(count);
        for (int i = 0; i < count; i++)
        {
            if (overlaps[i] != null)
            {
                if (alert != 2)// !if dont have target
                {
                    Vector3 directionBetween = (overlaps[i].transform.position - transform.position).normalized;
                    float angle = Vector3.Angle(transform.forward, directionBetween);
                    if (angle < lookAngle)
                    {
                        Ray ray = new Ray(transform.position, overlaps[i].transform.position - transform.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, lookRadius))
                        {
                            if (hit.transform.CompareTag(ablePlayer))
                            {

                                if (actionLeaveWaring != null)
                                {
                                    //Debug.Log("Stop StopCoroutine (Don't have target) and set alert to 2");
                                    StopCoroutine(actionLeaveWaring);
                                    actionLeaveWaring = null;
                                }
                                SetAlentCombat(2);

                            }

                        }

                    }
                }
                else  //aready have target
                {
                    // !check if targetNow can detect or not
                    Vector3 directionBetween = (target.position - transform.position).normalized;
                    float angle = Vector3.Angle(transform.forward, directionBetween);
                    //Debug.Log(angle);
                    Ray ray = new Ray(transform.position, target.position - transform.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, lookRadius))
                    {
                        if (hit.transform != target)
                        {
                            // !Have Target but Missing , not looking target
                            SetAlentCombat(1);
                            if (actionLeaveWaring == null)
                            {
                                //StopCoroutine(actionLeaveWaring);
                                //Debug.Log("Set corotin " + timeLeaveWaring+" alert now = 1");
                                actionLeaveWaring = StartCoroutine(LeaveWaring(timeLeaveWaring));
                            }
                        }
                    }
                    //End Check


                    // ! Check if targetNow can't Detect anymore
                    Vector3 directionBetweenNew2 = (overlaps[i].transform.position - transform.position).normalized;
                    float angleNew2 = Vector3.Angle(transform.forward, directionBetweenNew2);

                    if (target == null && angleNew2 < lookRadius)
                    {
                        ray = new Ray(transform.position, overlaps[i].transform.position - transform.position);

                        if (Physics.Raycast(ray, out hit, lookRadius))
                        {
                            if (hit.transform.CompareTag(ablePlayer))
                            {
                                if (actionLeaveWaring != null)
                                {
                                    //Debug.Log("Stop StopCoroutine (Can't Detect anymore) and set alert to 2");
                                    StopCoroutine(actionLeaveWaring);
                                    actionLeaveWaring = null;
                                }
                                SetAlentCombat(2);
                            }

                        }
                    }





                }
            }
        }

        if (count == 0)
        {
            switch (alert)
            {
                case 0:// !Idle
                    break;
                case 1:// !Warning
                    if (actionLeaveWaring == null)
                    {
                        Debug.Log("Can detec in Sphere but alert = 1 : Waring");
                        SetAlentCombat(0);
                    }
                    break;
                case 2:// !OnTarget
                    if (actionLeaveWaring == null)
                    {
                        //Debug.Log("Set corotin " + timeLeaveWaring);
                        actionLeaveWaring = StartCoroutine(LeaveWaring(timeLeaveWaring));
                    }
                    SetAlentCombat(1);
                    break;
            }
        }


    }
    #endregion

    protected IEnumerator LeaveWaring(float waitTime)
    {
        /* // ! How to Use
        if (actionLeaveWaring != null)
        {
            StopCoroutine(actionLeaveWaring);
        }
        actionLeaveWaring = StartCoroutine(LeaveWaring(timeLeaveWaring));
        */
        yield return new WaitForSeconds(waitTime);
        //Debug.Log("Time end , Enemy is Idle now");

        SetAlentCombat(0);
        actionLeaveWaring = null;
        //Debug.Log(actionLeaveWaring);

    }
    public void SetAlentCombat(int setAlent)
    {
        alert = setAlent;
        SetTarget(alert);
        switch (alert)
        {

            case 0:// !Idle
                textAlert.text = "";
                playerController.OnCombat(false);
                break;
            case 1:// !Warning
                textAlert.text = "?";
                playerController.OnCombat(false);
                break;
            case 2:// !OnTarget
                textAlert.text = "!";
                playerController.OnCombat(true);
                break;
        }
    }

    public void SetTarget(int alentWarning)
    {
        switch (alert)
        {
            case 0:// !Idle

                target = null;
                break;
            case 1:// !Warning

                target = null;
                break;
            case 2:// !OnTarget

                target = PlayerManager.instance.player.transform;
                break;
        }

    }


    protected virtual void MoveToTarget()
    {
        if (alert == 2)
        {
            MoveLockTarget();
        }
        else
        {
            MoveNotLockTarget();
        }
    }
    private void MoveLockTarget()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        characterController.Move(forward * moveSpeed * Time.deltaTime);

    }

    private void MoveNotLockTarget()
    {

    }
}
