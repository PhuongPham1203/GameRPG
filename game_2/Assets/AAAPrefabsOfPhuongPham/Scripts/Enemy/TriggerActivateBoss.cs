using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivateBoss : MonoBehaviour
{
    public EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 24)// player
        {
            if (enemyController != null )
            {
                if(enemyController.alertEnemy == AlertEnemy.Idle)
                {
                    enemyController.SetAlentCombat(AlertEnemy.OnTarget);

                    if (AudioManager.instance.IsPlayTheme("OnCombat"))
                    {

                    }
                    else
                    {
                        AudioManager.instance.PlaySoundOfTheme("OnCombat");
                        

                    }
                }
            }

        }
    }

}
