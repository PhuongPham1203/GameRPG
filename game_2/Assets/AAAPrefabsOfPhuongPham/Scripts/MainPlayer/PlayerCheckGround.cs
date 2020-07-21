using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckGround : MonoBehaviour
{
    private Animator animatorObj;
    //int layer = (1 << 8) | (1 << 9);
    
    // Start is called before the first frame update
    void Start()
    {
        animatorObj = GetComponentInParent<Animator>();
        //characterController = GetComponentInParent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == 20 || other.gameObject.layer == 21)
        {
            //Debug.Log("OnGround");
            //animatorObj.SetBool("isSwing", false);
            animatorObj.SetBool("OnGround", true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.layer == 20 || other.gameObject.layer == 21)
        {
            animatorObj.SetBool("OnGround", false);
        }
    }

}
