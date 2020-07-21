using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    //[SerializeField]
    //private PlayerStats plStats;

    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        instance = this;
        //plStats = player.GetComponent<PlayerStats>();
    }

    #endregion

    /*
    public void MoveToPoint(Vector3 p)
    {
        player.transform.position = p;
    }
    
    public void MovePlayerToPosition()
    {
        Debug.Log("beforre : " + player.gameObject.transform.position);

        player.gameObject.transform.position = new Vector3(1f, 2f, 3f);//plStats.teleportNearest;
        Debug.Log("after : "+ player.gameObject.transform.position);

    }

    
    private void FixedUpdate()
    {
        
        if (Input.GetKeyUp(KeyCode.P))
        {

            MovePlayerToPosition();
            //MoveToPoint(1, 1, 1);

        }
    }
    */


}