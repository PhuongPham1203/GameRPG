using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabsController : MonoBehaviour
{
    public GameObject[] allTabs ;
    public GameObject[] allHeaderTabs ;
    private void Start()
    {
        


    }
    public void OnTabsSelect(int numberTab)//select tab
    {
        
        allTabs[numberTab].transform.SetSiblingIndex(allTabs.Length);
        
    }

    


    
}
