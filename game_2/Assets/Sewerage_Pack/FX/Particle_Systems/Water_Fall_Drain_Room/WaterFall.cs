using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall : MonoBehaviour
{
    public string offsetMapName = "_BaseColorMap";
    public float fallSpeed = 1f;
    public Material mat;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        mat = GetComponent<MeshRenderer>().sharedMaterial;
    }


    /// <summary>
    /// 
    /// </summary>
    private void OnDestroy()
    {
        if (mat != null)
        {
            mat.SetTextureOffset(offsetMapName, Vector2.zero);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (mat != null)
        {
            Vector2 offset = mat.GetTextureOffset(offsetMapName);
            offset.y += Time.deltaTime * fallSpeed;
            if (offset.y > 1000.0f)
                offset.y = 1000.0f - offset.y;
            mat.SetTextureOffset(offsetMapName, offset);
        }
    }
}
