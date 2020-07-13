using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TargetController : MonoBehaviour
{
    
    public Transform targetObj;
    // Start is called before the first frame update
    /*
    void Start()
    {
        //targetObj = transform.Find("target");
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //
            targetObj.GetComponent<VisualEffect>().enabled = false;
            //targetObj.GetComponent<BoxCollider>().enabled = false;
            //GetComponentInParent<SphereCollider>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            targetObj.gameObject.layer = 22;

            //targetObj.GetComponent<BoxCollider>().enabled = true;
            //GetComponent<SphereCollider>().enabled = false;

        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
