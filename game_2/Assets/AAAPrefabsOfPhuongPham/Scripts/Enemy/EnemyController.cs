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
    protected Animator animator;

    public TextMesh textAlert;
    private string ablePlayer = "Player";



    [Header("Alent : 0:die 1-Idle 2:Warning 3-OnTarget")]
    public AlertEnemy alertEnemy = AlertEnemy.Idle;
    //public int alert = (int)AlertEnemy.Idle;

    [Space]
    [Header("Combat")]
    public bool canAction = true;
    public float timeCanAction = 0.5f;
    public float timeCanAttack = 2f;
    protected Coroutine actionLeaveAction;
    protected Coroutine actionLeaveAttack;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

        //SetTarget(0);
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        

    }


    void LateUpdate()
    {
        if (alertEnemy == AlertEnemy.OnTarget)
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

        Gizmos.color = new Color(245f,99f,28f,96f);
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
        Collider[] overlaps = new Collider[5];
        int count = Physics.OverlapSphereNonAlloc(transform.position, lookRadius, overlaps, layer);
        //Debug.Log("Number Player Find : " +count);
        for (int i = 0; i < count; i++)
        {
            if (overlaps[i] != null)
            {
                if (alertEnemy != AlertEnemy.OnTarget)// !if dont have target
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
                                SetAlentCombat(AlertEnemy.OnTarget);

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
                            SetAlentCombat(AlertEnemy.Warning);
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
                                SetAlentCombat(AlertEnemy.OnTarget);
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

        if (count == 0)
        {
            switch (alertEnemy)
            {
                case AlertEnemy.Die: // Die

                    break;

                case AlertEnemy.Idle:// !Idle
                    break;

                case AlertEnemy.Warning:// !Warning

                    if (actionLeaveWaring == null)
                    {
                        Debug.Log("Can detec in Sphere but alert = 2 : Waring");
                        SetAlentCombat(AlertEnemy.Idle);
                    }
                    break;

                case AlertEnemy.OnTarget:// !OnTarget

                    if (actionLeaveWaring == null)
                    {
                        //Debug.Log("Set corotin " + timeLeaveWaring);
                        actionLeaveWaring = StartCoroutine(LeaveWaring(timeLeaveWaring));
                    }
                    SetAlentCombat(AlertEnemy.Warning);

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

        SetAlentCombat(AlertEnemy.Idle);
        actionLeaveWaring = null;
        //Debug.Log(actionLeaveWaring);

    }
    public void SetAlentCombat(AlertEnemy setAlent)
    {
        alertEnemy = setAlent;
        SetTarget();
        switch (alertEnemy)
        {
            case AlertEnemy.Die:// ! Die

                break;

            case AlertEnemy.Idle:// !Idle
                textAlert.text = "";
                playerController.OnCombat(false);
                break;

            case AlertEnemy.Warning:// !Warning
                textAlert.text = "?";
                playerController.OnCombat(false);
                break;

            case AlertEnemy.OnTarget:// !OnTarget
                textAlert.text = "!";
                playerController.OnCombat(true);
                break;
        }
    }

    public void SetTarget()
    {
        switch (alertEnemy)
        {
            case AlertEnemy.Die: // Die

                target = null;
                break;
            case AlertEnemy.Idle:// !Idle

                target = null;
                break;
            case AlertEnemy.Warning:// !Warning

                target = null;
                break;
            case AlertEnemy.OnTarget:// !OnTarget

                target = PlayerManager.instance.player.transform;
                break;
        }

    }


    protected virtual void MoveToTarget()
    {
        if (alertEnemy == AlertEnemy.OnTarget)
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
        animator.SetFloat("SpeedMove",0.5f);
    }

    private void MoveNotLockTarget()
    {

    }

    public virtual void Attack(int attackCombo)
    {
        if (canAction)
        {
            canAction = false;
            if (actionLeaveAttack != null)
            {
                StopCoroutine(actionLeaveAttack);

            }
            actionLeaveAttack = StartCoroutine(CanAttack(timeCanAttack));// time can Attack Again
            if (actionLeaveAction != null)
            {
                StopCoroutine(actionLeaveAction);

            }
            actionLeaveAction = StartCoroutine(CanAcion(timeCanAttack)); // time can do something again

            animator.SetInteger("InAction", 2);
            animator.SetInteger("AttackCombo", attackCombo);
            animator.SetInteger("ActionInCombat", 3);
        }
        
    }
    protected IEnumerator CanAcion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canAction = true;

    }
    protected IEnumerator CanAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetInteger("InAction", 0);

    }

}

public enum AlertEnemy { Die, Idle, Warning, OnTarget }
