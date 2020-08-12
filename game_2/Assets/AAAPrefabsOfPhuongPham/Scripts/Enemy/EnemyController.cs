using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [Header("Information")]
    public float moveSpeed = 4f;
    public float runSpeed = 4f;
    public float lookRadius = 10f; // Range view look
    public float lookAngle = 45f; // Angle look

    public PathCreator way;
    //[Range(0f,1f)]
    public int pathWay = 0;
    public bool flipWay = false;

    [Header("Speed Tracking LookAt Target")]
    [Range(0.1f, 10f)]
    public double speedTrackTarget = 3f;
    public float distanceCanAttack;
    public float distance = 0f;

    public float timeTryMoveToPos = 0;
    public Vector3 posionTryMoveTo;

    [Range(2f, 15f)]
    public float timeLeaveWaring = 5f;
    protected Coroutine actionLeaveWaring;
    //protected bool isInFov = false;
    public Transform target;

    protected PlayerController playerController;
    protected CharacterController characterController;
    protected Animator animator;
    protected AudioEnemy audioEnemy;

    protected CharacterStats characterStats;

    public TextMesh textAlert;
    private string ablePlayer = "Player";



    [Header("Alent : 0:die 1-Idle 2:Warning 3-OnTarget")]
    public AlertEnemy alertEnemy = AlertEnemy.Idle;
    //public int alert = (int)AlertEnemy.Idle;
    public bool canFinish = false;
    public GameObject vfxFinishBot;


    [Space]
    [Header("Combat")]
    public bool canAction = true;
    public float timeCanAction = 0.5f;
    //public float timeWait = 1f;
    public float curTimeWait = 1f;
    public List<float> timeToNextAction;

    public int numberListAttack = 1;
    public int startListAttack = 0;
    public int currentListAttack = 0;
    public bool currentAttackDone = false;

    public Vector3 directionShoulGo = Vector3.zero;

    public Transform centerPoint;

    protected Coroutine actionLeaveAction;
    protected Coroutine actionLeaveAttack;
    protected Coroutine actionFinishBot;


    [Header("Hit Box")]
    public Transform parentVfxEnemy;
    public List<GameObject> hitBox;




    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (flipWay && way != null)
        {
            pathWay = way.path.NumPoints - 1;
            //Debug.Log(pathWay);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        //SetTarget(0);
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        audioEnemy = GetComponent<AudioEnemy>();
        characterStats = GetComponent<CharacterStats>();

        numberListAttack = timeToNextAction.Count;
    }


    void LateUpdate()
    {
        /*
        if (alertEnemy == AlertEnemy.OnTarget)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * (float)speedTrackTarget);

        }
        */
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceCanAttack);
    }


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

        SetStartListAttack(0);
        SetCurrenListAttack(0);
        //Debug.Log(actionLeaveWaring);

    }
    public void SetAlentCombat(AlertEnemy setAlent)
    {
        alertEnemy = setAlent;
        if (alertEnemy == AlertEnemy.Die)
        {
            return;
        }
        SetTarget();
        switch (alertEnemy)
        {
            case AlertEnemy.Die:// ! Die

                StopAllCoroutines();
                canFinish = false;

                break;

            case AlertEnemy.Idle:// !Idle
                textAlert.text = "";
                //playerController.OnCombat(false);
                if (actionLeaveAction != null)
                {
                    StopCoroutine(actionLeaveAction);
                    actionLeaveAction = null;
                }
                canFinish = true;

                break;

            case AlertEnemy.Warning:// !Warning
                textAlert.text = "?";
                //playerController.OnCombat(false);

                if (actionLeaveAction != null)
                {
                    StopCoroutine(actionLeaveAction);
                }
                actionLeaveAction = StartCoroutine(LeaveWaring(timeLeaveWaring));

                canFinish = true;

                break;

            case AlertEnemy.OnTarget:// !OnTarget
                textAlert.text = "!";
                if (actionLeaveAction != null)
                {
                    StopCoroutine(actionLeaveAction);
                    actionLeaveAction = null;
                }

                if (characterStats.currentPosture >= characterStats.maxPosture)
                {
                    canFinish = true;
                    SetFinishVFX(true);
                }
                else
                {
                    canFinish = false;
                    SetFinishVFX(false);

                }
                if (!AudioManager.instance.IsPlaySFX("AlertDetecterTarget"))
                {
                    AudioManager.instance.PlaySoundOfPlayer("AlertDetecterTarget");

                }

                //playerController.OnCombat(true);
                break;
        }
    }
    /*
    public void SetPlayerOnCombat(bool setOnCombat)
    {
        playerController.OnCombat(setOnCombat);
    }
    */
    public void SetTarget()
    {
        switch (alertEnemy)
        {
            case AlertEnemy.Die: // Die

                target = null;
                canAction = false;
                break;
            case AlertEnemy.Idle:// !Idle

                target = null;
                break;
            case AlertEnemy.Warning:// !Warning

                target = null;
                break;
            case AlertEnemy.OnTarget:// !OnTarget
                timeTryMoveToPos = 0;
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
        /*
        else
        {
            MoveNotLockTarget();
        }
        */
    }
    private void MoveLockTarget()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        characterController.Move(forward * moveSpeed * Time.deltaTime);
        animator.SetFloat("SpeedMove", 0.5f);
    }

    public virtual void MoveLockTargetWalkAround(Vector3 way, float typeMove, float speed)
    {
        way.y = 0;
        characterController.Move(way * speed * Time.deltaTime);
        animator.SetFloat("SpeedMove", typeMove);

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(way.x, 0, way.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * (float)speedTrackTarget);


    }
    public void TryMoveToPlayerPosition(Vector3 pos)
    {
        timeTryMoveToPos = 5f;
        posionTryMoveTo = pos;

    }
    public virtual bool MoveToPosition(Vector3 positionTarget, float typeMove, float speed)
    {
        Vector3 way = (positionTarget - transform.position).normalized;
        way.y = 0;
        characterController.Move(way * speed * Time.deltaTime);
        animator.SetFloat("SpeedMove", typeMove);

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(way.x, 0, way.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * (float)speedTrackTarget);

        Vector3 a = transform.position;

        if (Math.Abs(a.x - positionTarget.x) < 1 && Math.Abs(a.z - positionTarget.z) < 1)
        {
            return true;
        }

        return false;


    }


    public virtual bool Attack(int attackCombo)
    {
        if (canAction)
        {
            canAction = false;
            /*
            if (actionLeaveAttack != null)
            {
                StopCoroutine(actionLeaveAttack);

            }
            actionLeaveAttack = StartCoroutine(CanAttack(timeToNextAction[attackCombo]));// time can Attack Again
            */
            if (target != null)
            {

                //Vector3 pos = target.po
                Vector3 v = target.position;
                v.y = transform.position.y;
                transform.LookAt(v);

            }


            if (actionLeaveAction != null)
            {
                StopCoroutine(actionLeaveAction);

            }
            actionLeaveAction = StartCoroutine(CanAttack(timeToNextAction[attackCombo])); // time can do something again

            animator.SetInteger("InAction", 2);
            animator.SetInteger("AttackCombo", attackCombo + 1);
            animator.SetInteger("ActionInCombat", 3);

            characterStats.Reduction(3f);

            return true;
        }

        return false;
    }

    public void CanFinishBot(float t)
    {
        Stun(t);

        if (actionFinishBot != null)
        {
            StopCoroutine(actionFinishBot);
        }
        actionFinishBot = StartCoroutine(CanFinishInSecond(t));

    }

    public void Stun(float t)
    {
        canAction = false;
        if (actionLeaveAction != null)
        {
            StopCoroutine(actionLeaveAction);
        }
        actionLeaveAction = StartCoroutine(CanAction(t));
    }

    protected IEnumerator CanAction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canAction = true;

    }
    protected IEnumerator CanAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canAction = true;
        animator.SetInteger("InAction", 0);

    }

    protected IEnumerator LeaveActionDamage(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetInteger("InAction", 0);

    }
    public IEnumerator LookAtAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Vector3 tranTarget = PlayerManager.instance.player.transform.position;
        tranTarget.y = transform.position.y;
        transform.LookAt(tranTarget);
    }


    public IEnumerator CanFinishInSecond(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canFinish = false;
        SetFinishVFX(false);
    }




    protected virtual void AttackFirstTime()
    {
        Debug.Log("Attack the Fist time , Start list = " + startListAttack);
    }

    protected virtual void AttackSecondTime()
    {
        Debug.Log("Attack the Second time , Start list = " + startListAttack);
    }
    protected virtual void AttackThirdTime()
    {
        Debug.Log("Attack the Third time , Start list = " + startListAttack);
    }

    public void SetStartListAttack(int number)
    {
        startListAttack = number;
        if (number == 0)
        {
            curTimeWait = 1f;//timeWait;
        }
    }

    public void SetCurrenListAttack(int number)
    {
        currentListAttack = number;
    }


    public virtual void HitBox(int numberHit)
    {
        Debug.Log("hit");
    }
    public virtual void Finish1()
    {
        //Debug.Log("Finish1");
        StopAllCoroutines();
        canAction = false;
        alertEnemy = AlertEnemy.Die;
        animator.SetInteger("InAction", 10);

    }
    public virtual void Finish2()
    {
        //Debug.Log("Finish2");
        characterStats.TakeTrueDamegeFinish(999999);
        characterStats.Die();
    }
    public virtual void EnemyDie()
    {
        StopAllCoroutines();
        SetAlentCombat(AlertEnemy.Die);
        animator.SetInteger("InAction", 8);

    }

    public virtual void Damage(float timeStun)
    {
        if (actionLeaveAction != null)
        {
            StopCoroutine(actionLeaveAction);

        }
        AudioManager.instance.PlaySoundOfPlayer("Damage");
        animator.SetTrigger("Damage");
        animator.SetInteger("InAction", 7);
        actionLeaveAction = StartCoroutine(CanAttack(timeStun)); // time can do something again
    }

    public virtual void BlockDamage(float time)
    {
        if (actionLeaveAction != null)
        {
            StopCoroutine(actionLeaveAction);

        }
        animator.SetTrigger("Damage");
        animator.SetInteger("InAction", 6);
        actionLeaveAction = StartCoroutine(CanAttack(time)); // time can do something again
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(" to1"+collision.gameObject.name);
        if (collision.gameObject.layer == 24)
        {
            
            // touch Player
            if (alertEnemy == AlertEnemy.Idle)
            {
                alertEnemy = AlertEnemy.Warning;
                LookAtAfter(0.2f);

            }
            else if (alertEnemy == AlertEnemy.Warning)
            {
                LookAtAfter(0.2f);

            }
        }
    }
    */

    public bool SetFinishVFX(bool isFinish)
    {
        vfxFinishBot.SetActive(isFinish);
        //enemyController.canFinish = isFinish;
        PlayerManager.instance.buttonFinish.SetActive(isFinish);
        return isFinish;
    }
}

public enum AlertEnemy { Die, Idle, Warning, OnTarget }
