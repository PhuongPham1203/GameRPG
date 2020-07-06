using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabsController : MonoBehaviour
{

    protected AllTabs allTabs;
    protected Image[] allImageTabs;
    protected Image thisImage;

    public  GameObject panel;
  
   
    private void Start()
    {
        allTabs = GetComponentInParent<AllTabs>();
        allImageTabs = allTabs.imageTabs;
        thisImage = GetComponent<Image>();


        //add point down
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnTabsSelect(); });
        trigger.triggers.Add(entry);


    }
    public void OnTabsSelect()//select tab
    {

        DisableTabs(allTabs.imageTabsSelect);
        
        Color color = thisImage.color;
        color.a = 1f;
        thisImage.color = color;

        panel.SetActive(true);

        allTabs.imageTabsSelect = thisImage;
        allTabs.panelSelect = panel;



    }

    public void DisableTabs(Image img)
    {
        Color color = img.color;
        color.a = 0.8f;
        img.color = color;
        allTabs.panelSelect.SetActive(false);
        
    }


    
}
