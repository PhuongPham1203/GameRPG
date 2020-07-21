using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(Random.Range(0, 10.0f), Random.Range(0, 10.0f), Random.Range(0, 10.0f));
        transform.position = position;
    }

    private void FixedUpdate()
    {
        
    }
}
