using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : EnemyController
{
    [Header("Boss Controller")]
    public float timeReduction = 5f;
    public BossRangeCenter bossRangeCenter;
    public PhaseBoss phaseBossCurrent = PhaseBoss.Phase_1;
    public PhaseBoss phaseEnd = PhaseBoss.Phase_3;
    Vector3 vectorWayWalk = Vector3.zero;
    public InforAttack[] distanceAttack;

    //public InforAttack inforAttackCurrent;


    protected override void Start()
    {

        //SetTarget(0);
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        audioEnemy = GetComponent<AudioEnemy>();
        characterStats = GetComponent<CharacterStats>();

        // set combo of this phase , run befor
        this.SetComboOfPhase(this.phaseBossCurrent);

    }
    private void Update()
    {
        if (this.canAction)
        {
            if (this.alertEnemy == AlertEnemy.OnTarget)
            {
                this.distance = Vector3.Distance(target.position, transform.position);


                this.RunListAttack();



            }
        }
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



    protected override void RunListAttack()
    {
        if (this.currentAttackDone)
        {
            this.timeTryMoveToPos = 0;

            this.distanceCanAttack = this.distanceAttack[this.currentListAttack].distanceAttack;
            this.currentAttackDone = false;

            if(this.distance < 2f)
            {
                //kick

                // look At
                Vector3 t = this.target.position;
                t.y = this.transform.position.y;
                this.transform.LookAt(t);

                //return;
            }
        }

        switch (this.currentListAttack)
        {
            #region Testing
            case -1:
                if (this.timeTryMoveToPos > 0)
                {
                    this.MoveLockTargetWalkAround(this.vectorWayWalk, 0.5f, this.moveSpeed);

                }
                else
                {
                    // Start Walk Around

                    // find find location between boss and center boss
                    Vector3 vectorTwoPoint = (this.transform.position + this.bossRangeCenter.transform.position).normalized;
                    if (vectorTwoPoint == Vector3.zero)
                    {
                        vectorTwoPoint.x = UnityEngine.Random.Range(0.1f, 0.9f);
                        vectorTwoPoint.z = UnityEngine.Random.Range(0.1f, 0.9f);
                    }

                    // way need to walk
                    //int r = Random.Range(-1,1);
                    //if (r == 0) r = -1;

                    this.vectorWayWalk = (vectorTwoPoint + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f))).normalized;
                    this.vectorWayWalk.y = 0;
                    //Debug.Log(this.vectorWayWalk);
                    this.timeTryMoveToPos = 4f;
                    StartCoroutine(this.WalkAround(4f));
                }


                break;
            #endregion
            case 0:
                // code

                this.AttackBase();

                break;
            case 1:
                // code
                // Go around in 4s
                if (this.timeTryMoveToPos == 0)
                {
                    // Start Walk Around
                    // find find location between boss and center boss
                    Vector3 vectorTwoPoint = (this.transform.position + this.bossRangeCenter.transform.position).normalized;
                    if (vectorTwoPoint == Vector3.zero)
                    {
                        vectorTwoPoint.x = UnityEngine.Random.Range(0.1f, 0.9f);
                        vectorTwoPoint.z = UnityEngine.Random.Range(0.1f, 0.9f);
                    }

                    this.vectorWayWalk = (vectorTwoPoint + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f))).normalized;
                    this.vectorWayWalk.y = 0;
                    //Debug.Log(this.vectorWayWalk);
                    this.timeTryMoveToPos = 5f;
                    StartCoroutine(this.WalkAround(5f));
                }
                else if (this.timeTryMoveToPos > 0)
                {
                    this.MoveLockTargetWalkAround(this.vectorWayWalk, 0.5f, this.moveSpeed);
                }
                else
                {

                    this.AttackPhase1Combo2();
                }
                break;
            case 2:
                // code 
                this.AttackBase();
                break;
            case 3:

                // code 
                this.AttackBase();

                break;
            case 4:
                // code 
                this.AttackPhase1Combo5();
                break;


        }

    }

    public void AttackBase()
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
            this.MoveToPosition(this.target.position, 1f, this.runSpeed);
        }

        if (this.currentAttackDone)
        {
            //this.lookAt = true;

            this.currentListAttack++;
            if (this.startListAttack + this.numberListAttack <= this.startListAttack + this.currentListAttack) //
            {
                this.currentListAttack = this.startListAttack;

            }

            //Debug.Log("currentListAttack "+this.currentListAttack);
        }
    }

    // Phase 1

    public void AttackPhase1Combo2()
    {
        if (this.distance < this.distanceCanAttack) // Attack
        {

            // Attack
            this.animator.SetTrigger("rollF");
            this.StopLookAndMove();

            // look At
            Vector3 t = this.target.position;
            t.y = this.transform.position.y;
            this.transform.LookAt(t);

            this.currentAttackDone = this.Attack(this.currentListAttack);

        }
        else // Move to Player
        {
            this.MoveToPosition(this.target.position, 1f, this.runSpeed);
        }

        if (this.currentAttackDone)
        {
            //this.lookAt = true;

            this.currentListAttack++;
            if (this.startListAttack + this.numberListAttack <= this.startListAttack + this.currentListAttack) //
            {
                this.currentListAttack = this.startListAttack;
            }


        }
    }
    public void AttackPhase1Combo5()
    {
        if (this.distance < this.distanceCanAttack) // Attack
        {

            // Attack
            this.animator.SetTrigger("rollF");
            this.StopLookAndMove();

            // look At
            Vector3 t = this.target.position;
            t.y = this.transform.position.y;
            this.transform.LookAt(t);

            this.currentAttackDone = this.Attack(this.currentListAttack);

        }
        else // Move to Player
        {
            this.MoveToPosition(this.target.position, 1f, this.runSpeed);
        }

        if (this.currentAttackDone)
        {
            //this.lookAt = true;

            this.currentListAttack++;
            if (this.startListAttack + this.numberListAttack <= this.startListAttack + this.currentListAttack) //
            {
                this.currentListAttack = this.startListAttack;

            }
        }
    }
    // Phase 2

    // Phase 3

    // End Phase

    public override bool Attack(int attackCombo)
    {

        if (canAction)
        {
            canAction = false;

            this.inforAttackCurrent = this.distanceAttack[this.currentListAttack];


            if (this.actionLeaveAction != null)
            {
                StopCoroutine(this.actionLeaveAction);
            }
            this.actionLeaveAction = StartCoroutine(this.CanAttackAgain(this.inforAttackCurrent.timeToNextAction)); // time can do something again

            this.animator.SetInteger("combo", this.inforAttackCurrent.combo);
            this.animator.SetTrigger("triggerAttack");

            // for Hit Box
            //this.inforAttackCurrent.hitBox.enabled = true;

            characterStats.Reduction(this.timeReduction);

            return true;
        }

        return false;
        //return base.Attack(attackCombo);
    }

    public void ActivavteWeapon(int i)
    {
        if (i == 1)
        {
            this.inforAttackCurrent.hitBox.enabled = true;
        }
        else
        {
            this.inforAttackCurrent.hitBox.enabled = false;
        }
    }

    public override bool MoveToPosition(Vector3 positionTarget, float typeMove, float speed)
    {
        Vector3 way = (positionTarget - transform.position).normalized;
        way.y = 0;
        characterController.Move(way * speed * Time.deltaTime);
        animator.SetFloat("z", typeMove);

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(way.x, 0, way.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * (float)speedTrackTarget);

        Vector3 a = transform.position;

        if (Math.Abs(a.x - positionTarget.x) < 1 && Math.Abs(a.z - positionTarget.z) < 1)
        {
            return true;
        }

        return false;

        //return base.MoveToPosition(positionTarget, typeMove, speed);
    }

    public override void MoveLockTargetWalkAround(Vector3 way, float typeMove, float speed)
    {
        Vector3 forward = this.transform.forward;
        Vector3 right = this.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * way.z + right * way.x).normalized;

        this.animator.SetFloat("x", desiredMoveDirection.x);
        this.animator.SetFloat("z", desiredMoveDirection.z);
        //this.animator.SetFloat("x", way.x);
        //this.animator.SetFloat("z", way.z);

        characterController.Move(desiredMoveDirection * speed * Time.deltaTime);

    }


    void StopLookAndMove()
    {
        this.animator.SetFloat("x", 0);
        this.animator.SetFloat("z", 0);

        this.lookAt = false;
    }
    IEnumerator WalkAround(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        timeTryMoveToPos = -1;
    }

    protected IEnumerator CanAttackAgain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.canAction = true;
        this.lookAt = true;
        animator.SetInteger("combo", 0);

    }

    void SetComboOfPhase(PhaseBoss phaseBoss)
    {

        int number = 0;
        bool f = true;
        foreach (InforAttack i in this.distanceAttack)
        {
            if (i.phaseBoss == phaseBoss)
            {
                number++;
                if (f)
                {
                    f = false;
                    this.startListAttack = Array.IndexOf(this.distanceAttack, i);
                    this.currentListAttack = this.startListAttack;
                }
            }

        }

        this.numberListAttack = number;
        this.currentAttackDone = true;

        // Set Layer Animator - Phase

        for (int i = 1; i < this.animator.layerCount; i++)
        {
            if ((int)this.phaseBossCurrent == i)
            {
                this.animator.SetLayerWeight(i, 1);
            }
            else
            {
                this.animator.SetLayerWeight(i, 0);
            }
        }
        //Debug.Log(this.startListAttack);
        //Debug.Log(this.currentListAttack);
        //Debug.Log(this.numberListAttack);

    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, this.vectorWayWalk * 5f);
    }
    */

}

public enum PhaseBoss { Phase_1 = 1, Phase_2 = 2, Phase_3 = 3, Phase_4 = 4 }

[System.Serializable]
public class InforAttack
{

    public PhaseBoss phaseBoss = PhaseBoss.Phase_1;
    public int combo = 1;
    public int damageAttack = 0;
    public float timeStun = 0;
    public float wayStun = 0;
    public float distanceAttack = 2f;
    public float timeToNextAction = 0f;

    public Collider hitBox;
}

