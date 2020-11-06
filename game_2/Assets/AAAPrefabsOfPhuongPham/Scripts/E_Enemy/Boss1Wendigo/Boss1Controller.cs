using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : EnemyController
{
    [Header("Boss Controller")]
    public BossRangeCenter bossRangeCenter;
    public PhaseBoss phaseBossCurrent = PhaseBoss.Phase_1;
    public PhaseBoss phaseEnd = PhaseBoss.Phase_3;
    Vector3 vectorWayWalk = Vector3.zero;
    public InforAttack[] distanceAttack;
    private int loopAttack = 0;
    //public InforAttack inforAttackCurrent;


    protected override void Start()
    {

        //SetTarget(0);
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        audioEnemy = GetComponent<AudioEnemy>();
        characterStats = GetComponent<CharacterStats>();

        // set combo of this phase , run befor
        this.SetComboOfPhase(this.phaseBossCurrent);

        //Debug.Log("Over");

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
            #region Phase 1
            case 0: // ! phase 1 combo1 
                    // code

                if (MustRunRandomWalkAround(3f)) break;

                this.AttackBase();

                break;
            case 1: // phase 1 combo2
                    // code

                if (MustRunRandomWalkAround(3f)) break;

                this.AttackPhase1Combo2();


                break;
            case 2:// phase 1 combo3
                // code
                if (MustRunRandomWalkAround(2f)) break;

                this.AttackBase();
                break;
            case 3:// phase 1 combo4
                // code 
                if (MustRunRandomWalkAround(2f)) break;

                this.AttackBase();

                break;
            case 4: // phase 1 combo5
                // code 
                if (MustRunRandomWalkAround(3f)) break;

                this.AttackPhase1Combo5();
                break;
            #endregion
            #region Phase 2
            case 5: // ! phase 2 combo1
                if (MustRunRandomWalkAround(4f)) break;
                this.AttackPhase2Combo1();
                break;
            case 6: // phase 2 combo2
                if (MustRunRandomWalkAround(1f)) break;
                this.AttackBase();
                break;
            case 7: // phase 2 combo3
                //if (MustRunRandomWalkAround(2f)) break;

                if (this.timeTryMoveToPos <= 0)
                {
                    this.animator.SetInteger("InAction", 3);
                    StartCoroutine(LeaveActionDamage(1.5f));
                    this.timeTryMoveToPos = 2f;
                }


                this.AttackPhase2Combo3();
                break;
            case 8: // phase 2 combo4

                this.AttackBase();
                break;
            case 9: // phase 2 combo5
                if (MustRunRandomWalkAround(2f)) break;
                this.AttackPhase2Combo5();
                break;
            case 10: // phase 2 combo6
                if (MustRunRandomWalkAround(4f)) break;
                this.AttackPhase2Combo1();
                break;
            case 11: // phase 2 combo7
                this.AttackPhase2Combo1();
                break;
            case 12: // phase 2 combo8
                if (MustRunRandomWalkAround(2f)) break;
                this.AttackPhase2Combo1();
                break;
            #endregion
            #region Phase 3
            case 13: // ! phase 3 combo1
                if (this.timeTryMoveToPos <= 0)
                {
                    this.animator.SetTrigger("rollR");
                    this.timeTryMoveToPos = 1f;
                }

                this.AttackPhase3Combo1();
                break;
            case 14: // phase 3 combo2

                if (MustRunRandomWalkAround(2f)) break;

                this.AttackBase();
                break;
            case 15: // phase 3 combo3

                if (this.timeTryMoveToPos <= 0)
                {
                    this.animator.SetTrigger("rollR");
                    this.timeTryMoveToPos = 1f;
                }

                this.AttackPhase3Combo1();
                break;
            case 16: // phase 3 combo4 ( Dead Attack )
                //if (MustRunRandomWalkAround(3f)) break;
                if (this.timeTryMoveToPos <= 0)
                {
                    this.animator.SetTrigger("rollF");
                    this.timeTryMoveToPos = 1f;
                    //Debug.Log("Roll B");
                }
                this.AttackPhase3Combo4();
                break;
            case 17: // phase 3 combo5

                if (MustRunRandomWalkAround(2f)) break;

                this.AttackBase();
                break;
            case 18: // phase 3 combo6 ( Stun Attack )
                if (MustRunRandomWalkAround(2f)) break;

                this.AttackBase();
                break;
            case 19: // phase 3 combo7 ( Loop Attack ) 
                this.AttackBase();
                break;
            
            case 20: // phase 3 combo8 ( Dead Attack )

                if (this.loopAttack < 2 && this.isHitPlayer != IsHit.Miss) // go back Phase 3 Combo 7
                {
                    this.loopAttack++;
                    this.currentListAttack--;
                    break;
                }
                else
                {
                    this.loopAttack = 0;
                }

                //if (MustRunRandomWalkAround(2f)) break;
                this.AttackBase();
                break;
            
                #endregion
        }

    }

    private void AttackBase()
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


    // ! Phase 1

    private void AttackPhase1Combo2()
    {
        if (this.distance < this.distanceCanAttack) // Attack
        {

            // Attack
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

        this.CheckCurrentAttackDone();

    }
    private void AttackPhase1Combo5()
    {
        if (this.distance < this.distanceCanAttack) // Attack
        {

            // Attack
            // look At
            Vector3 t = this.target.position;
            t.y = this.transform.position.y;
            this.transform.LookAt(t);


            this.currentAttackDone = this.Attack(this.currentListAttack);
            try
            {

                NomManager.instance.PlayNomAnimation("HiemAnimation");
                this.audioEnemy.PlaySoundOfEnemy("AlertHiemAttack");
            }
            finally
            {

            }

        }
        else // Move to Player
        {
            this.MoveToPosition(this.target.position, 1f, this.runSpeed);
        }
        this.CheckCurrentAttackDone();


    }
    // ! Phase 2
    private void AttackPhase2Combo1()
    {
        if (this.distance < this.distanceCanAttack) // Attack
        {

            // Attack
            //this.StopLookAndMove();
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
        this.CheckCurrentAttackDone();

    }

    private void AttackPhase2Combo3()
    {
        if (this.timeTryMoveToPos > 0)
        {
            this.timeTryMoveToPos -= Time.deltaTime;
            Vector3 jumpB = -this.transform.forward;
            jumpB.y = 1;
            jumpB *= 3f;
            //Debug.Log(jumpB);
            this.characterController.Move(jumpB * Time.deltaTime);
        }
        if (this.timeTryMoveToPos <= 0)
        {
            this.currentAttackDone = this.Attack(this.currentListAttack);
        }

        this.CheckCurrentAttackDone();


    }
    private void AttackPhase2Combo5()
    {
        if (this.distance < this.distanceCanAttack) // Attack
        {

            // Attack
            // look At
            this.StopLookAndMove();
            Vector3 t = this.target.position;
            t.y = this.transform.position.y;
            this.transform.LookAt(t);


            this.currentAttackDone = this.Attack(this.currentListAttack);
            try
            {

                NomManager.instance.PlayNomAnimation("HiemAnimation");
                this.audioEnemy.PlaySoundOfEnemy("AlertHiemAttack");
            }
            finally
            {

            }

        }
        else // Move to Player
        {
            this.MoveToPosition(this.target.position, 1f, this.runSpeed);
        }
        this.CheckCurrentAttackDone();


    }

    // ! Phase 3
    private void AttackPhase3Combo1()
    {
        if (this.timeTryMoveToPos > 0) this.timeTryMoveToPos -= Time.deltaTime;

        if (this.timeTryMoveToPos <= 0)
        {
            this.inforAttackCurrent = this.distanceAttack[this.currentListAttack];
            StartCoroutine(TurnOnWeaponAbout(6f));
            this.currentAttackDone = this.Attack(this.currentListAttack);
        }

        this.CheckCurrentAttackDone();

    }

    private void AttackPhase3Combo4()
    {

        if (this.timeTryMoveToPos > 0) this.timeTryMoveToPos -= Time.deltaTime;

        if (this.timeTryMoveToPos <= 0)
        {
            this.StopLookAndMove();
            Vector3 t = this.target.position;
            t.y = this.transform.position.y;
            this.transform.LookAt(t);

            //Debug.Log("Roll B");

            this.currentAttackDone = this.Attack(this.currentListAttack);
            try
            {
                NomManager.instance.PlayNomAnimation("HiemAnimation");
                this.audioEnemy.PlaySoundOfEnemy("AlertHiemAttack");
            }
            finally
            {

            }
        }
        this.CheckCurrentAttackDone();


    }
    

    

    IEnumerator TurnOnWeaponAbout(float t)
    {
        GameObject g = this.inforAttackCurrent.hitBox.gameObject;
        g.SetActive(true);
        //this.inforAttackCurrent.hitBox.enabled = true;
        yield return new WaitForSeconds(t);
        //this.inforAttackCurrent.hitBox.enabled = false;
        g.SetActive(false);

    }
    // * End Phase
    public void AttackCreateFlyWeapon()
    {
        if (this.inforAttackCurrent.hitBox.TryGetComponent<WeaponControllerOfBoss1>(out WeaponControllerOfBoss1 w))
        {
            this.characterStats.Reduction(this.timeReduction);
            w.CreateFlyWeapon(this.GetComponent<EnemyController>());
        }
    }
    private void SetRandomWalkAround(float timeWalkAround)
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
        this.timeTryMoveToPos = timeWalkAround;
        StartCoroutine(this.WalkAround(timeWalkAround));
    }

    private bool MustRunRandomWalkAround(float timeWalkAround)
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

    public override bool Attack(int attackCombo)
    {

        if (canAction)
        {
            this.animator.SetBool("Block", false);
            this.isHitPlayer = IsHit.Miss;
            this.canAction = false;

            this.inforAttackCurrent = this.distanceAttack[this.currentListAttack];


            if (this.actionLeaveAction != null) StopCoroutine(this.actionLeaveAction);

            this.actionLeaveAction = StartCoroutine(this.CanAttackAgain(this.inforAttackCurrent.timeToNextAction)); // time can do something again

            this.animator.SetInteger("AttackCombo", this.inforAttackCurrent.combo);
            this.animator.SetTrigger("triggerAttack");

            // for Hit Box
            //this.inforAttackCurrent.hitBox.enabled = true;

            characterStats.Reduction(this.timeReduction);

            return true;
        }

        return false;
        //return base.Attack(attackCombo);
    }

    public override void Damage(float timeStun)
    {
        //base.Damage(timeStun);

    }

    public override void BlockDamage(float time)
    {
        //base.BlockDamage(time);
        if (actionLeaveAction != null) StopCoroutine(actionLeaveAction);

        actionLeaveAction = StartCoroutine(CanAttack(time)); // time can do something again
        animator.SetTrigger("BlockDamage");
        //animator.SetInteger("InAction", 6);


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
        animator.SetInteger("AttackCombo", 0);

        this.animator.SetBool("Block", true);

    }



    void SetComboOfPhase(PhaseBoss phaseBoss)
    {
        this.currentListAttack = 0;
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

    }

    public override bool ReLive()
    {
        base.ReLive();

        if (this.phaseBossCurrent == this.phaseEnd)
        {
            return false; // Boss end;
        }
        else
        {



            StartCoroutine(CanTakeDamageIn(4f));
            this.inforAttackCurrent = new InforAttack();
            this.phaseBossCurrent = this.phaseBossCurrent + 1;

            this.SetComboOfPhase(this.phaseBossCurrent);


            return true;
        }

    }

    IEnumerator CanTakeDamageIn(float t)
    {
        this.gameObject.layer = 2;
        yield return new WaitForSeconds(t);
        this.gameObject.layer = 23;

        this.animator.SetLayerWeight((int)this.phaseBossCurrent - 1, 0);
        this.animator.SetLayerWeight((int)this.phaseBossCurrent, 1);

        this.lookAt = true;



    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, this.vectorWayWalk * 5f);
    }
    */

}


