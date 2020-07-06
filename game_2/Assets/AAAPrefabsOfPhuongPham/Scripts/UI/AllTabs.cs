using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllTabs : MonoBehaviour
{
    public Image[] imageTabs;
    public Image imageTabsSelect;
    public GameObject panelSelect;

    private void Start()
    {
        imageTabsSelect = imageTabs[0];
        Color color = imageTabsSelect.color;
        color.a = 1f;
        imageTabsSelect.color = color;

        
    }

    
    // Update is called once per frame
    
}
