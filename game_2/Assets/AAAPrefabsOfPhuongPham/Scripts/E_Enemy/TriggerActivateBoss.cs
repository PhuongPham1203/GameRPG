using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivateBoss : MonoBehaviour
{
    public EnemyController enemyController;
    // Start is called before the first frame update

    void Start()
    {
        this.enemyController = transform.root.GetComponent<EnemyController>();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 24)// player
        {

            this.ActivateBot();

        }
    }
    public virtual void ActivateBot()
    {
        if (enemyController != null)
        {
            if (enemyController.alertEnemy != AlertEnemy.Die && enemyController.alertEnemy != AlertEnemy.OnTarget)
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

    public void WarningAllEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10, (1 << 23));
        foreach (var hitCollider in hitColliders)
        {
            //hitCollider.SendMessage("AddDamage");
            EnemyController e = hitCollider.transform.GetComponent<EnemyController>();

            if (e.alertEnemy == AlertEnemy.Idle)
            {
                e.alertEnemy = AlertEnemy.Warning;

                Vector3 pos = PlayerManager.instance.player.transform.position;
                e.TryMoveToPlayerPosition(pos);
                //Debug.Log(e.alertEnemy);
            }
        }
    }

}
