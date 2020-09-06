using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    Text fps;
    private void Start()
    {
        fps = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime != 0)
        {
            fps.text ="FPS : "+ (int)(1 / Time.deltaTime);
        }
    }
}
