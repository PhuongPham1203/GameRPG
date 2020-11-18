using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieController : EnemyController
{
    private void Start()
    {
        playerController = PlayerManager.instance.player.GetComponent<PlayerController>();
        audioEnemy = GetComponent<AudioEnemy>();
        characterStats = GetComponent<CharacterStats>();

        // set combo of this phase , run befor
        this.SetComboOfPhase(this.phaseBossCurrent);

    }
    private void Update()
    {
        /*
        if (curTimeWait > 0)
        {
            curTimeWait -= Time.deltaTime;

        }
        */
        if (canAction)
        {


            if (alertEnemy == AlertEnemy.OnTarget)
            {
                distance = Vector3.Distance(target.position, transform.position);


                RunListAttack();



            }
            else if (alertEnemy == AlertEnemy.Idle)
            {
                if (way != null)
                {

                    Vector3 point = way.path.GetPoint(pathWay);
                    //Debug.Log(pathWay);
                    if (this.MoveToPosition(point, 0.5f, moveSpeed))
                    {
                        if (flipWay)
                        {
                            pathWay -= 1;
                            if (pathWay < 0)
                            {
                                pathWay = way.path.NumPoints - 1;
                            }
                        }
                        else
                        {
                            pathWay += 1;
                            if (pathWay >= way.path.NumPoints)
                            {
                                pathWay = 0;
                            }
                        }

                    }
                }
            }
            else if (alertEnemy == AlertEnemy.Warning)
            {
                if (timeTryMoveToPos > 0)
                {
                    timeTryMoveToPos -= Time.deltaTime;
                    this.posionTryMoveTo = PlayerManager.instance.player.transform.position;
                    if (MoveToPosition(posionTryMoveTo, 2f, runSpeed))
                    {
                        timeTryMoveToPos = 0;
                    }
                }



            }
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

        switch (currentListAttack)
        {

            case 0:
                // code
                if (MustRunRandomWalkAround(1f)) break;
                this.AttackBase();
                break;
            case 1:
                // code

                if (MustRunRandomWalkAround(2f)) break;
                //this.AttackBase();
                this.AttackPhase1Combo2();
                break;
            case 2:
                // code
                if (MustRunRandomWalkAround(1f)) break;
                this.AttackBase();

                break;


        }


    }


    protected override void AttackBase()
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

    void AttackPhase1Combo2()
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

        this.CheckCurrentAttackDone();
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
    protected override void SetComboOfPhase(PhaseBoss phaseBoss)
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

}
