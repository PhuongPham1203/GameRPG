using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t : MonoBehaviour
{
    public GameObject pl;
    public Transform target;
    public Coroutine loadingMoveToPosition;
    public GameObject uiLoading;

    // Start is called before the first frame update
    void Start()
    {
        pl = PlayerManager.instance.player;
    }

    private void FixedUpdate()
    {   
        
        if (Input.GetKeyUp(KeyCode.P))
        {


        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 24)
        {
            //loadingMoveToPosition = StartCoroutine(Loading(1.5f, target.position));//Loading After 1.5s
            //other.transform.position = target.position;
            //Debug.Log(other.transform.position);
            AudioManager.instance.PlaySoundOfTheme("Theme Apocalypse");
        }
    }
    
}
