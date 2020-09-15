using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyController
{

    private void Update()
    {
        /*
        FOVDetectTarget();
        if (alertEnemy == AlertEnemy.OnTarget) // have target
        {
            // Action with Target When OnCombat
            distance = Vector3.Distance(target.position, transform.position);
            if(distance > distanceCanAttack){
                MoveToTarget();    
            }else if (distance <= distanceCanAttack)
            {
                animator.SetFloat("SpeedMove", 0);

                if (canAction)
                {
                    
                    Attack(1);
                }
            }

        }
        else if (alertEnemy == AlertEnemy.Warning) // Warning
        {
            // Action When Waring
        }
        else if (alertEnemy == AlertEnemy.Idle) //Idle
        {
            // Action When Idle

        }
        */

        if (curTimeWait > 0)
        {
            curTimeWait -= Time.deltaTime;

        }

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
                    if (MoveToPosition(posionTryMoveTo, 1f, runSpeed))
                    {
                        timeTryMoveToPos = 0;
                    }
                }



            }
        }




    }

    protected override void RunListAttack()
    {

        if (currentAttackDone)
        {
            currentAttackDone = false;
        }

        switch (currentListAttack)
        {

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



                if (curTimeWait > 0)
                {
                    MoveLockTargetWalkAround(directionShoulGo, 0.5f, moveSpeed);

                }
                else
                {
                    currentAttackDone = true;
                    animator.SetBool("Block", false);

                }

                break;
            case 3:
                // code

                AttackThirdTime();

                break;
            case 4:
                // code

                if (curTimeWait > 0)
                {
                    MoveLockTargetWalkAround(directionShoulGo, 0.5f, moveSpeed);

                }
                else
                {
                    currentAttackDone = true;
                    animator.SetBool("Block", false);


                }
                break;

        }
        if (!currentAttackDone)
        {
            return;
        }

        currentListAttack += 1;
        if (currentListAttack >= numberListAttack)
        {
            currentListAttack = startListAttack;
        }

    }

    protected override void AttackFirstTime()
    {
        //base.AttackFirstTime();
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
    }


    protected override void AttackSecondTime()
    {
        //base.AttackSecondTime();

        if (distance < distanceCanAttack)
        {

            currentAttackDone = Attack(currentListAttack);
            animator.SetFloat("SpeedMove", 0);

        }
        else
        {
            MoveToPosition(target.position, 0.5f, moveSpeed);

        }

        
        if (currentAttackDone)
        {
            //startListAttack = 1;
            Vector3 direc = (transform.position - PlayerManager.instance.player.transform.position).normalized;
            direc.x += Random.Range(-0.7f,0.7f);
            direc.z += Random.Range(-0.7f, 0.7f);
            directionShoulGo = direc.normalized;

            curTimeWait = timeToNextAction[currentListAttack+1];

            animator.SetBool("Block",true);

        }
        

    }

    protected override void AttackThirdTime()
    {
        //base.AttackThirdTime();

        if (distance < distanceCanAttack)
        {

            currentAttackDone = Attack(2);
            animator.SetFloat("SpeedMove", 0);

        }
        else
        {
            MoveToPosition(target.position, 0.5f * 1.5f, moveSpeed * 1.5f);

        }

        if (currentAttackDone)
        {
            //startListAttack = 1;
            Vector3 direc = (transform.position - PlayerManager.instance.player.transform.position).normalized;
            direc.x += Random.Range(-0.7f, 0.7f);
            direc.z += Random.Range(-0.7f, 0.7f);
            directionShoulGo = direc.normalized;
            
            curTimeWait = timeToNextAction[currentListAttack + 1];

            animator.SetBool("Block", true);


        }

    }

    public override void HitBox(int numberHit)
    {
        
        //base.HitBox(numberHit);

        //Quaternion rot ;
        switch (numberHit)
        {

            case 11:
                // code
                audioEnemy.PlaySoundOfEnemy("Attack1_1");
                //Instantiate(hitBox[0], parentVfxEnemy);

                hitBox[1].enabled = true;
                //SetHitBox();
                break;
            case 12:
                // code
                audioEnemy.PlaySoundOfEnemy("Attack1_2");
                //Instantiate(hitBox[1], parentVfxEnemy);
                hitBox[1].enabled = true;

                break;
            case 2:
                // code
                //Instantiate(hitBox[2], parentVfxEnemy);
                audioEnemy.PlaySoundOfEnemy("Attack2");
                hitBox[0].enabled = true;

                break;
            case 3:
                // code

                //Instantiate(hitBox[2], parentVfxEnemy);
                audioEnemy.PlaySoundOfEnemy("Attack3");
                hitBox[1].enabled = true;

                break;


        }


    }

    

}
