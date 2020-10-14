using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Vector3 cameraLookAtIgnoreY = Vector3.zero;
    void LateUpdate() {

        this.cameraLookAtIgnoreY = Camera.main.transform.position;
        this.cameraLookAtIgnoreY.y = this.transform.position.y;
        
        this.transform.LookAt(this.cameraLookAtIgnoreY);
    }
}
