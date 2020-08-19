using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObjOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
