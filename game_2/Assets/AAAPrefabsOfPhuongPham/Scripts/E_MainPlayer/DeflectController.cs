using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectController : MonoBehaviour
{
    public Collider deflectCollider;
    public bool canDeflectAgain = true;
    private void Awake()
    {
        if (this.deflectCollider != null)
        {
            this.deflectCollider = GetComponent<Collider>();
        }
        this.deflectCollider.enabled = false;
    }

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

}
