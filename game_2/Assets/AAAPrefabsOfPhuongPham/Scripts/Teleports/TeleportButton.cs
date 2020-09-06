using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportButton : MonoBehaviour
{
    public TeleInformation teleportTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToTeleportTarget()
    {
        Transform player = PlayerManager.instance.player.transform;

        if(SceneManager.GetActiveScene().buildIndex == teleportTarget.GetSceneIndexes().GetSceneBuiltID())
        {
            Debug.Log("In same scene");

            TeleportController[] allTeleportInScene = FindObjectsOfType<TeleportController>();

            foreach (TeleportController t in allTeleportInScene)
            {
                if(t.teleInformation.GetInstanceID() == teleportTarget.GetInstanceID())
                {
                    //player.GetComponent<CharacterController>().transform.position = t.transform.GetChild(0).position;

                    player.position = t.transform.GetChild(0).position;

                    Debug.Log("Move to " + t.transform.GetChild(0).position);

                    Debug.Log("Player pos"+ player.transform.position);
                    break;
                }
            }
            //player.transform.position 

        }
        else
        {
            Debug.Log("Need change to scene "+ teleportTarget.GetSceneIndexes().GetSceneBuiltID());

        }

    }

}
