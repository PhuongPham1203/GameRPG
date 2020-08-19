using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    [Space]
    [Header("UX / UI")]
    public GameObject UITarget;
    public Image hpTarget;
    public Image postureTarget;
    public Text nameTarget;
    public GameObject buttonFinish;
    //[SerializeField]
    //private PlayerStats plStats;

    #region Singleton
    public static PlayerManager instance;
    void Awake()
    {
        if (instance != null)
        {
            //Destroy(this.gameObject);
            Debug.LogWarning("More than one instance of PlayerManager found!!!");

            return;
        }
        instance = this;
        //player = GameObject.FindGameObjectWithTag("Player");
        //QualitySettings.vSyncCount = 2;

        //Application.targetFrameRate = 30; 
        //Application.targetFrameRate = 45;

        //plStats = player.GetComponent<PlayerStats>();
    }

    #endregion

    void Start()
    {
        
        //QualitySettings.SetQualityLevel(5, true);
    }
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