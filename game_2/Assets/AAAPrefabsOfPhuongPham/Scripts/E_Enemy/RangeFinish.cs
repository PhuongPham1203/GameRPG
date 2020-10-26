using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFinish : MonoBehaviour
{
    private EnemyController enemyController;

    //public GameObject buttonFinishBot;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = transform.GetComponentInParent<EnemyController>();
    }

    /*

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(" to2" + other.gameObject.name);
        if (other.gameObject.layer == 24)
        {
            // In Range Finish Bot
            if (enemyController.canFinish)
            {
                if (enemyController.alertEnemy == AlertEnemy.Idle)
                {
                    //vfxFinishBot.SetActive(true);
                    //enemyController.canFinish = true;
                    SetFinishVFX(true);
                }
                else if (enemyController.alertEnemy == AlertEnemy.Warning)
                {
                    //vfxFinishBot.SetActive(true);
                    //enemyController.canFinish = true;

                    SetFinishVFX(true);

                }

            }
            


        }
    }
    */

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(" to2" + other.gameObject.name);
        if (other.gameObject.layer == 24)
        {
            // In Range Finish Bot
            //vfxFinishBot.SetActive(false);
            //enemyController.canFinish = false;

            if (enemyController.canFinish)
            {
                //enemyController.canFinish = false;
                enemyController.SetFinishVFX(false);
                //Debug.Log("Set vfx");
            }
        }
    }




}
