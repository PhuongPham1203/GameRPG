using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t : MonoBehaviour
{
    

    void Update()
    {
        this.transform.LookAt(PlayerManager.instance.player.transform.position);
    }

}
