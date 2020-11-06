using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectController : MonoBehaviour
{
    public PlayerController playerController;
    float timeFromDeflectToBlock;
    float timeDeflectCountDown;
    //public Collider deflectCollider;
    //public bool canDeflectAgain = true;
    /*
    private void Awake()
    {
        if (this.deflectCollider != null)
        {
            this.deflectCollider = GetComponent<Collider>();
        }
        this.deflectCollider.enabled = false;
    }
    */
    void Start()
    {
        if (this.playerController == null)
        {
            this.playerController = GetComponent<PlayerController>();

        }
    }

    void Update()
    {
        if (this.playerController.deflectStatus == DeflectStatus.Deflect)
        {
            if (timeFromDeflectToBlock > 0)
            {
                this.timeFromDeflectToBlock -= Time.deltaTime;
            }
            else
            {
                this.playerController.deflectStatus = DeflectStatus.Block;

            }
        }
        if (this.timeDeflectCountDown > 0)
        {
            this.timeDeflectCountDown -= Time.deltaTime;
        }

    }

    public void Deflect(float timeDeflect, float timeCountDown)
    {
        if (this.timeDeflectCountDown > 0) // in countdown cant deflect
        {

        }
        else
        {
            this.playerController.deflectStatus = DeflectStatus.Deflect;
            this.timeFromDeflectToBlock = timeDeflect;
            this.timeDeflectCountDown = timeCountDown;
        }
    }

    /*
    public void Deflect(float timeCanDeflectAgain, float timeToDisableDeflect)
    {
        if (this.canDeflectAgain && !this.deflectCollider.enabled)
        {
            this.canDeflectAgain = false;
            this.deflectCollider.enabled = true;

            StartCoroutine(DeflectAgain(timeCanDeflectAgain));
            StartCoroutine(DisableDeflectColliderAffter(timeToDisableDeflect));
        }
    }
    IEnumerator DeflectAgain(float t)
    {
        yield return new WaitForSeconds(t);
        this.canDeflectAgain = true;
    }
    IEnumerator DisableDeflectColliderAffter(float t)
    {
        //Debug.Log("InDeFlect");
        yield return new WaitForSeconds(t);
        //Debug.Log("OutDeFlect");
        this.deflectCollider.enabled = false;
    }

    */

}
