using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivateBot : TriggerActivateBoss
{
    public override void ActivateBot()
    {
        //base.ActivateBot();

        if (enemyController != null)
        {
            if (enemyController.alertEnemy != AlertEnemy.Die && enemyController.alertEnemy != AlertEnemy.OnTarget)
            {
                enemyController.SetAlentCombat(AlertEnemy.OnTarget);

                if (AudioManager.instance.IsPlayAnyTheme())
                {

                }
                else
                {
                    if (!AudioManager.instance.IsPlayTheme("OnCombat"))
                    {
                        AudioManager.instance.PlaySoundOfTheme("OnCombat");

                    }
                }


            }


        }

        this.WarningAllEnemy();
    }
}
