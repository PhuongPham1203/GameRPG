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

        if (alertEnemy == AlertEnemy.OnTarget)
        {
            distance = Vector3.Distance(target.position, transform.position);


            RunListAttack();

            /*
            // Action with Target When OnCombat
            distance = Vector3.Distance(target.position, transform.position);
            if (distance > distanceCanAttack)
            {
                MoveToPosition(target.position,1f,runSpeed);
            }
            else if (distance <= distanceCanAttack)
            {
                animator.SetFloat("SpeedMove", 0);

                if (canAction)
                {

                    Attack(1);
                }
            }
            */

        }else if(alertEnemy == AlertEnemy.Die)
        {

        }



    }

    void RunListAttack()
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
                AttackThirdTime();
                break;
            case 3:
                // code
                break;
            case 4:
                // code
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
        if (curTimeWait>0)
        {
            curTimeWait -= Time.deltaTime;

        }
        else if (distance < distanceCanAttack)
        {
            
            currentAttackDone = Attack(0);
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
            
            currentAttackDone = Attack(1);
            animator.SetFloat("SpeedMove", 0);

        }
        else
        {
            MoveToPosition(target.position, 0.5f, moveSpeed);

        }

        /*
        if (currentAttackDone)
        {
            startListAttack = 1;
        }
        */
    }

    protected override void AttackThirdTime()
    {
        base.AttackThirdTime();
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

                //Quaternion rot = Quaternion.Euler(startPositionRotationLightAttack.eulerAngles + effectsLightAttack[EffectNumber].effectAttack.transform.eulerAngles);

                //Instantiate(hitBox[0], hitBox[0].transform.localPosition + centerPoint.transform.position, centerPoint.transform.rotation + hitBox[0].transform.localRotation);

                //rot = Quaternion.Euler(centerPoint.rotation.eulerAngles+ hitBox[0].transform.eulerAngles);
                //Debug.Log(centerPoint.rotation.eulerAngles + hitBox[0].transform.eulerAngles);

                //Debug.Log(centerPoint.rotation.eulerAngles);

                //Instantiate(hitBox[0], hitBox[0].transform.localPosition + centerPoint.position, hitBox[0].transform.rotation);

                Instantiate(hitBox[0], parentVfxEnemy);

                break;
            case 12:
                // code
                audioEnemy.PlaySoundOfEnemy("Attack1_2");
                //Instantiate(hitBox[1], hitBox[1].transform.localPosition + centerPoint.transform.position, hitBox[1].transform.rotation);
                //rot = Quaternion.Euler(centerPoint.rotation.eulerAngles + hitBox[1].transform.eulerAngles);
                //Instantiate(hitBox[1], hitBox[1].transform.localPosition + centerPoint.transform.position, hitBox[1].transform.rotation);
                Instantiate(hitBox[1], parentVfxEnemy);

                break;
            case 2:
                // code
                Instantiate(hitBox[2], parentVfxEnemy);

                audioEnemy.PlaySoundOfEnemy("Attack2");

                break;
            case 3:
                // code
                break;
            case 4:
                // code
                break;

        }


    }


}
