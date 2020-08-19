using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerWeapon : MonoBehaviour
{
    public EnemyController parentController;
    public Animator animator;
    public CharacterStats characterStats;
    public float timeWait =1f;
    float timeWaitPr = 0;
    public int[] combo;
    // Start is called before the first frame update
    void Start()
    {
        parentController = transform.root.GetComponent<EnemyController>();
        animator = transform.root.GetComponent<Animator>();
        characterStats = transform.root.GetComponent<CharacterStats>();
    }

    private void Update()
    {
        if (timeWaitPr > 0)
        {
            timeWaitPr -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            int c = animator.GetInteger("AttackCombo");

            if (animator.GetInteger("InAction") == 2 && timeWaitPr<=0)
            {
                foreach(int i in combo){
                    if(i == c)
                    {
                        timeWaitPr = timeWait;
                        other.gameObject.GetComponent<CharacterStats>().TakeDamege(characterStats.GetAttackDame(69));
                        break;
                    }
                }

            }
        }
    }
}
