using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : EnemyController
{
    [Header("Boss Controller")]
    public BossRangeCenter bossRangeCenter;
    PhaseBoss phaseBoss = PhaseBoss.Phase_1;
    Vector3 vectorWayWalk = Vector3.zero;

    private void Update()
    {
        if (this.canAction)
        {
            if (this.alertEnemy == AlertEnemy.OnTarget)
            {
                this.distance = Vector3.Distance(target.position, transform.position);

                switch (this.phaseBoss)
                {
                    case PhaseBoss.Phase_1:
                        // code
                        this.RunListAttack();

                        break;
                    case PhaseBoss.Phase_2:
                        // code

                        break;
                    case PhaseBoss.Phase_3:
                        // code

                        break;

                }
            }
        }
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

    protected override void RunListAttack()
    {
        if (this.currentAttackDone)
        {
            this.currentAttackDone = false;
        }

        switch (this.currentListAttack)
        {
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
                        vectorTwoPoint.x = Random.Range(0.1f, 0.9f);
                        vectorTwoPoint.z = Random.Range(0.1f, 0.9f);
                    }

                    // way need to walk
                    int r = Random.Range(-1,1);
                    if (r == 0) r = -1;
                    
                    this.vectorWayWalk = r*(vectorTwoPoint + new Vector3( Random.Range(-1, 1), 0, Random.Range(-1, 1) ) ).normalized;
                    this.vectorWayWalk.y = 0;
                    Debug.Log(this.vectorWayWalk);
                    this.timeTryMoveToPos = 4f;
                    StartCoroutine(this.WalkAround(4f));
                }


                break;

            case 0:
                // code

                AttackFirstTime();
                break;
            case 1:
                // code

                AttackSecondTime();
                break;
            case 2:
                // code 

                break;

        }

    }

    protected override void AttackFirstTime()
    {
        base.AttackFirstTime();
        /*
        if (distance < distanceCanAttack)
        {

            currentAttackDone = Attack(currentListAttack);
            animator.SetFloat("SpeedMove", 0);

        }
        else
        {
            MoveToPosition(target.position, 1f, runSpeed);

        }


        if (currentAttackDone)
        {
            startListAttack = 1;
        }
        */
    }

    IEnumerator WalkAround(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        timeTryMoveToPos = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, this.vectorWayWalk * 5f);
    }

}

public enum PhaseBoss { Phase_1, Phase_2, Phase_3, Phase_4 }
