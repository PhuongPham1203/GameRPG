using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t : MonoBehaviour
{
    public GameObject obja;
    public GameObject objb;
    public Transform trana;
    public Transform tranb;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("obj a "+ obja.GetInstanceID());
        Debug.Log("obj b " + objb.GetInstanceID());

        Debug.Log("tran a " + trana.GetInstanceID());

        objb = obja;

        Debug.Log("obj b " + objb.GetInstanceID());

        Debug.Log("tran b " + tranb.GetInstanceID());

        Debug.Log("tran b " + tranb.GetInstanceID());

    }



}
