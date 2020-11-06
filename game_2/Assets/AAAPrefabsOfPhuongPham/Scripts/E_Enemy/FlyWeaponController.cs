using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWeaponController : MonoBehaviour
{
    public InforAttack inforAttack;
    public EnemyController enemyController;
    public float timeDestroy = 5f;
    public float speedFly = 4;
    public Vector3 wayFly = Vector3.zero;

    public bool isUp = false;
    public float speedStun = 15f;

    void Start()
    {
        Destroy(this.gameObject, this.timeDestroy);
        /*
        if (TryGetComponent<MeleeWeaponTrail>(out MeleeWeaponTrail m))
        {
            m.Emit = true;
        }
        */
    }

    void Update()
    {
        this.transform.Translate(this.wayFly * this.speedFly * Time.deltaTime, Space.World);

    }
    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.layer == 2 && other.gameObject.CompareTag("Deflect"))
        { // layer ignore Raycast
            if(this.inforAttack != null && this.inforAttack.attackTypeEffect == AttackTypeEffect.Dead){
                return;
            }
            this.GetComponent<Collider>().enabled = false;
            AudioManager.instance.PlaySoundOfPlayer("Deflect");
            other.GetComponentInParent<CharacterStats>().vfxSteel.Play();

        }
        else 
        */
        
        if (other.gameObject.layer == 24)
        {
            //this.inforAttack = transform.root.GetComponent<EnemyController>().inforAttackCurrent;
            //Debug.Log(enemyController.name);
            //Debug.Log(other.GetComponent<CharacterStats>().name);
            //Debug.Log(this.inforAttack.damageAttack);
            //Debug.Log( this.inforAttack.timeStun);
            //Debug.Log(this.inforAttack.attackTypeEffect);
            if (this.inforAttack != null)
            {
                other.GetComponent<CharacterStats>().TakeDamage(this.inforAttack.damageAttack, this.inforAttack.timeStun, this.inforAttack.attackTypeEffect, this.enemyController);

                CCStun ccStun = other.gameObject.AddComponent<CCStun>();
                //ccStun.isUp = this.isUp;
                ccStun.speedStun = this.speedStun;
                ccStun.SetTimeDestroy(this.inforAttack.timeStun);
            }

            Destroy(this.gameObject);



        }
    }


}
