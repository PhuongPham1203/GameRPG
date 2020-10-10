using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivateBoss : MonoBehaviour
{
    public EnemyController enemyController;
    // Start is called before the first frame update


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 24)// player
        {
            if (enemyController != null )
            {
                if(enemyController.alertEnemy == AlertEnemy.Idle)
                {
                    enemyController.SetAlentCombat(AlertEnemy.OnTarget);

                    if (AudioManager.instance.IsPlayTheme("OnCombat_Weindigo"))
                    {

                    }
                    else
                    {
                        AudioManager.instance.PlaySoundOfTheme("OnCombat_Weindigo");

                        if (AudioManager.instance.IsPlayTheme("OnCombat"))
                        {
                            AudioManager.instance.StopSoundOfTheme("OnCombat");
                        }
                    }
                }
            }

        }
    }

}
