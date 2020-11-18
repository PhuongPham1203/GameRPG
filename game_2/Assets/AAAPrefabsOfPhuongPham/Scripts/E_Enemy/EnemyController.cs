using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UIElements;
using PathCreation;

public class EnemyController : MonoBehaviour
{
    [Header("Information")]
    public float moveSpeed = 4f;
    public float runSpeed = 4f;
    public float lookRadius = 10f; // Range view look
    public float lookAngle = 45f; // Angle look

    public PathCreator way;
    //[Range(0f,1f)]
    protected int pathWay = 0;
    public bool flipWay = false;

    [Header("Speed Tracking LookAt Target")]
    [Range(0.1f, 10f)]
    public double speedTrackTarget = 3f;
    public bool lookAt = true;
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
    public Collider triggerActivateEnemy;
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
    public float timeReduction = 5f;

    public int numberListAttack = 1;
    public int startListAttack = 0;
    public int currentListAttack;
    public bool currentAttackDone;

    public Vector3 directionShoulGo;// = Vector3.zero;

    public Transform centerPoint;

    public Coroutine actionLeaveAction;
    protected Coroutine actionLeaveAttack;
    protected Coroutine actionFinishBot;


    [Header("Hit Box")]
    //public Transform parentVfxEnemy;
    public List<Collider> hitBox;
    public InforAttack inforAttackCurrent;
    public InforAttack[] distanceAttack;
    public IsHit isHitPlayer;//= new IsHit.Miss;

    [Header("Boss Controller")]
    public BossRangeCenter bossRangeCenter;
    public PhaseBoss phaseBossCurrent = PhaseBoss.Phase_1;
    public PhaseBoss phaseEnd = PhaseBoss.Phase_3;
    protected Vector3 vectorWayWalk = Vector3.zero;

    protected int loopAttack = 0;


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
    protected virtual void Start()
    {

        //SetTarget(0);
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        audioEnemy = GetComponent<AudioEnemy>();
        characterStats = GetComponent<CharacterStats>();

        numberListAttack = timeToNextAction.Count;
        //Debug.Log(this.gameObject.name+"Orig");

    }


    void LateUpdate()
    {

        if (alertEnemy == AlertEnemy.OnTarget && this.lookAt)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * (float)speedTrackTarget);

        }

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

        if (alertEnemy == AlertEnemy.Die)
        {
            return;
        }
        alertEnemy = setAlent;
        SetTarget();
        switch (setAlent)
        {
            case AlertEnemy.Die:// ! Die
                this.triggerActivateEnemy.enabled = false;
                StopAllCoroutines();
                canFinish = false;
                Debug.Log("Set alert Die " + gameObject.name);
                // if this Die send all item this character keep to QuestManager
                QuestManager.instance.TriggerQuestManager(characterStats.itemKeep, TypeQuest.KillEnemy);

                // Item Drop
                foreach (GameObject g in this.characterStats.itemDrop)
                {
                    GameObject item = Instantiate(g);
                    item.transform.position = this.transform.position;
                    Vector3 pos = item.transform.position;
                    pos.y += 0.5f;
                    item.transform.position = pos;
                }

                break;

            case AlertEnemy.Idle:// !Idle
                //Debug.Log("Set alert Idle " + gameObject.name);
                this.triggerActivateEnemy.enabled = true;

                this.animator.SetFloat("x", 0);
                this.animator.SetFloat("z", 0);

                textAlert.text = "";
                if (actionLeaveAction != null)
                {
                    StopCoroutine(actionLeaveAction);
                    actionLeaveAction = null;
                }
                canFinish = true;
                this.canAction = true;
                break;

            case AlertEnemy.Warning:// !Warning
                //Debug.Log("Set alert Warning " + gameObject.name);
                this.triggerActivateEnemy.enabled = true;
                textAlert.text = "?";

                if (actionLeaveAction != null)
                {
                    StopCoroutine(actionLeaveAction);
                }
                actionLeaveAction = StartCoroutine(LeaveWaring(timeLeaveWaring));

                canFinish = true;
                this.canAction = true;
                break;

            case AlertEnemy.OnTarget:// !OnTarget
                //Debug.Log("Set alert OnTarget " + gameObject.name);
                this.triggerActivateEnemy.enabled = false;

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
                    this.canAction = true;

                    canFinish = false;
                    SetFinishVFX(false);

                }
                if (!AudioManager.instance.IsPlaySFX("AlertDetecterTarget"))
                {
                    AudioManager.instance.PlaySoundOfPlayer("AlertDetecterTarget");

                }

                break;
        }
    }

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


    protected virtual void RunListAttack()
    {

    }
    protected virtual void MoveToTarget()
    {
        if (alertEnemy == AlertEnemy.OnTarget)
        {
            MoveLockTarget();
        }
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
    protected virtual bool MustRunRandomWalkAround(float timeWalkAround)
    {
        if (this.isHitPlayer != IsHit.Miss)
        {
            return false;
            //this.isHitPlayer = IsHit.Miss;
        }
        else if (this.timeTryMoveToPos <= 0)
        {
            this.SetRandomWalkAround(timeWalkAround);
            //break;
            this.isHitPlayer = IsHit.Block;
            return true;
        }
        else if (this.timeTryMoveToPos > 0)
        {
            this.MoveLockTargetWalkAround(this.vectorWayWalk, 0.5f, this.moveSpeed);
            //break;
            return true;
        }
        return false;
    }
    protected void SetRandomWalkAround(float timeWalkAround)
    {
        // Start Walk Around
        // find find location between boss and center boss
        Vector3 vectorTwoPoint = Vector3.zero;
        if (this.bossRangeCenter != null)
        {
            vectorTwoPoint = (this.transform.position + this.bossRangeCenter.transform.position).normalized;
        }
        else if (this.way != null)
        {
            vectorTwoPoint = (this.transform.position + this.way.GetComponent<Transform>().position).normalized;
        }else if (vectorTwoPoint == Vector3.zero)
        {
            vectorTwoPoint.x = UnityEngine.Random.Range(0.1f, 0.9f);
            vectorTwoPoint.z = UnityEngine.Random.Range(0.1f, 0.9f);
        }

        this.vectorWayWalk = (vectorTwoPoint + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f))).normalized;
        this.vectorWayWalk.y = 0;
        //Debug.Log(this.vectorWayWalk);
        this.timeTryMoveToPos = timeWalkAround;
        StartCoroutine(this.WalkAround(timeWalkAround));
    }

    protected virtual void AttackBase()
    {
        if (this.distance < this.distanceCanAttack) // Attack
        {
            this.StopLookAndMove();
            // look At
            Vector3 t = this.target.position;
            t.y = this.transform.position.y;
            this.transform.LookAt(t);

            // Attack
            this.currentAttackDone = this.Attack(this.currentListAttack);

        }
        else // Move to Player
        {
            this.MoveToPosition(this.target.position, 0.5f, this.moveSpeed);
        }

        this.CheckCurrentAttackDone();
    }


    public virtual bool Attack(int attackCombo)
    {
        if (canAction)
        {
            canAction = false;

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
    public void ActivateWeapon(int i)
    {

        if (i == 1)
        {
            this.inforAttackCurrent.hitBox.enabled = true;
        }
        else
        {
            this.inforAttackCurrent.hitBox.enabled = false;
        }

        /*
        if (this.inforAttackCurrent.combo == 8)
        {
            Debug.Log(this.inforAttackCurrent.hitBox.name + " " + this.inforAttackCurrent.hitBox.enabled);
        }
        */


    }
    protected void StopLookAndMove()
    {
        this.animator.SetFloat("x", 0);
        this.animator.SetFloat("z", 0);

        this.lookAt = false;
    }

    public void ActivateVFX()
    {
        if (this.inforAttackCurrent.particleSystemVFX != null)
        {
            this.inforAttackCurrent.particleSystemVFX.Play();
        }
    }
    protected bool CheckCurrentAttackDone()
    {
        if (this.currentAttackDone)
        {
            this.currentListAttack++;
            if (this.startListAttack + this.numberListAttack <= this.currentListAttack) //
            {
                this.currentListAttack = this.startListAttack;

            }
        }
        return this.currentAttackDone;
    }
    public void CanFinishBot(float t)
    {
        Stun(t);

        if (actionFinishBot != null) StopCoroutine(actionFinishBot);

        actionFinishBot = StartCoroutine(CanFinishInSecond(t));

    }

    public void Stun(float t)
    {

        if (this.actionLeaveAction != null) StopCoroutine(this.actionLeaveAction);

        this.canAction = false;
        this.animator.SetInteger("InAction", 10);
        this.animator.SetFloat("SpeedMove", 0);
        this.animator.SetFloat("x", 0);
        this.animator.SetFloat("z", 0);
        this.lookAt = false;

        this.animator.SetInteger("AttackCombo", 0);
        this.animator.ResetTrigger("triggerAttack");

        if (actionLeaveAction != null) StopCoroutine(actionLeaveAction);
        actionLeaveAction = StartCoroutine(CanAction(t));

    }

    protected IEnumerator WalkAround(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        timeTryMoveToPos = -1;
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
        this.canFinish = false;
        this.lookAt = true;
        SetFinishVFX(false);
        this.animator.SetInteger("InAction", 0);

        // Sub Posture 30 %

        float s = 0.3f * this.characterStats.maxPosture;
        this.characterStats.AddHPandPosture(0, -s);
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
    public void SetHitBoxFalse()
    {

        foreach (Collider c in hitBox)
        {
            c.enabled = false;

        }

        //Debug.Log("set hitbox "+e);
    }
    public virtual void Finish1()
    {
        //Debug.Log("Finish1");
        //StopAllCoroutines();
        this.canAction = false;
        this.triggerActivateEnemy.enabled = false;
        this.lookAt = false;

        if (this.actionFinishBot != null) StopCoroutine(actionFinishBot);
        if (this.actionLeaveAction != null) StopCoroutine(this.actionLeaveAction);


        if (TryGetComponent<SelectEnemy>(out SelectEnemy se))
        {
            se.enabled = false;
        }

        //SetAlentCombat(AlertEnemy.Die);
        animator.SetInteger("InAction", 10);

    }
    public virtual void Finish2()
    {
        //Debug.Log("Finish2");
        //run VFX

        //StartCoroutine(BotDie(0.5f));

        this.animator.SetInteger("InAction", 8);
        characterStats.TakeTrueDamageFinish(999999);
        characterStats.Die();
        this.lookAt = false;

    }


    public IEnumerator BotDieAfter(float t)
    {
        this.Finish1();

        yield return new WaitForSeconds(0);
        //this.animator.SetInteger("InAction",8);
        //characterStats.TakeTrueDamegeFinish(999999);
        //characterStats.Die();

        this.Finish2();

    }

    public virtual void EnemyDie()
    {
        Debug.Log("run EnemyDie");
        //StopAllCoroutines();
        SetAlentCombat(AlertEnemy.Die);
        animator.SetInteger("InAction", 8);

        //targetGroupCiner.m_Targets[1].targets

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

    public void PlayerDeflectEnemy(InforAttack inforA)
    {

        // Add Posture
        this.characterStats.AddHPandPosture(0, inforA.damageAttack / 2);
        this.isHitPlayer = IsHit.Block;
        this.characterStats.Reduction(this.timeReduction);

        this.animator.SetInteger("AttackCombo", 0);

        if (this.characterStats.currentPosture >= this.characterStats.maxPosture)
        {
            this.CanFinishBot(4f);
        }
        else
        {
            StartCoroutine(this.CanBlockAffter(1.5f));

        }
        //this.animator.SetBool("Block", true);
    }

    /*
    public void PlayerDeflectEnemy()
    {
        this.animator.SetInteger("AttackCombo", 0);
        StartCoroutine(this.CanBlockAffter(1.5f));
        //this.animator.SetBool("Block", true);
    }
    */
    protected virtual void SetComboOfPhase(PhaseBoss phaseBoss)
    {
        Debug.Log("Set combo of phase");
    }
    protected IEnumerator CanAttackAgain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.canAction = true;
        this.lookAt = true;
        animator.SetInteger("AttackCombo", 0);

        this.animator.SetBool("Block", true);

    }
    IEnumerator CanBlockAffter(float t)
    {
        yield return new WaitForSeconds(t);
        this.animator.SetBool("Block", true);
        this.canAction = true;
        this.lookAt = true;
    }


    public bool SetFinishVFX(bool isFinish)
    {
        vfxFinishBot.SetActive(isFinish);
        //enemyController.canFinish = isFinish;
        PlayerManager.instance.buttonFinish.SetActive(isFinish);
        return isFinish;
    }

    public virtual bool ReLive()
    {
        Debug.Log(this.gameObject.name + " Relive.");
        return false;
    }
}

public enum AlertEnemy { Die, Idle, Warning, OnTarget }

public enum PhaseBoss { Phase_1 = 1, Phase_2 = 2, Phase_3 = 3, Phase_4 = 4 }
public enum AttackTypeEffect { Normal, Dead, Stun };

[System.Serializable]
public class InforAttack
{
    public string name;
    public PhaseBoss phaseBoss = PhaseBoss.Phase_1;
    public int combo = 1;
    public int damageAttack = 0;
    public AttackTypeEffect attackTypeEffect = AttackTypeEffect.Normal;
    public float timeStun = 0;
    //public float wayStun = 0;
    public float distanceAttack = 2f;
    public float timeToNextAction = 0f;

    public Collider hitBox;
    public ParticleSystem particleSystemVFX;


}