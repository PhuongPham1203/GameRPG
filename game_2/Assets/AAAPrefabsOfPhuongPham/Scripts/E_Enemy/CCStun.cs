using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCStun : MonoBehaviour
{
    public bool isUp = false;
    public float speedStun = 0f;
    
    // Update is called once per frame
    void Update()
    {
        if (this.isUp)
        {
            this.transform.Translate(this.transform.up * speedStun * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(-this.transform.forward * speedStun * Time.deltaTime);

        }
    }

    public void SetTimeDestroy(float t)
    {
        Destroy(this, t);
    }


}

