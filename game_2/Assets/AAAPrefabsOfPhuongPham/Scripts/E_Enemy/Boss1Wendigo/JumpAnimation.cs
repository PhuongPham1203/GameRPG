using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimation : StateMachineBehaviour
{
    public GameObject highGround;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        Vector3 p = animator.transform.position;
        p.y -= 2f;
        Quaternion r = animator.transform.rotation;
        this.CreateHighGround(p,r);
    }

    protected void CreateHighGround(Vector3 p ,Quaternion r){
        GameObject hg = Instantiate(this.highGround,p,r);
        Destroy(hg,4.5f);
    }
}
