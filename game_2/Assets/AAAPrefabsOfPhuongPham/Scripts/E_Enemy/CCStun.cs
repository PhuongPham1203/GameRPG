using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCStun : MonoBehaviour
{
    //public bool isUp = false;
    public float speedStun = 0f;
    public Vector3 wayStun = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        /*
        if (this.isUp)
        {
            this.transform.Translate(this.transform.up * speedStun * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(-this.transform.forward * speedStun * Time.deltaTime);

        }
        */
        if(this.wayStun == Vector3.zero){
            this.wayStun = -this.transform.forward;
        }else{
            this.transform.Translate(this.wayStun * speedStun * Time.deltaTime,Space.World);
        }
    }

    public void SetTimeDestroy(float t)
    {
        Destroy(this, t);
    }


}

