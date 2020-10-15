using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseCombo : StateMachineBehaviour
{
    SwooshVFXTrailWeapon swooshVFXTrailWeapon;
    Collider hitBox;
    public bool isRightWeapon = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    /*
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("In Light Attack 1");
        Debug.Log(animator.name);

    }
    */

    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.swooshVFXTrailWeapon = animator.GetComponent<SwooshVFXTrailWeapon>();
        if (animator.TryGetComponent<Boss1Controller>(out Boss1Controller boss1Controller))
        {
            this.hitBox = boss1Controller.inforAttackCurrent.hitBox;
        }
        //Debug.Log("Out Light Attack 1");
        if (this.isRightWeapon)
        {
            this.swooshVFXTrailWeapon.SetTrailR(0);
            
        }
        else
        {
            this.swooshVFXTrailWeapon.SetTrailL(0);
        }

        if (this.hitBox != null)
        {
            this.hitBox.enabled = false;
        }

    }
    
}
