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
