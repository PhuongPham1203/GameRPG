using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton 

    public static Inventory instance;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!!!");
            return;

        }
        instance = this;

    }
    #endregion
    public List<SourceItemSlot> items = new List<SourceItemSlot>();
    public int space = 50;
    public GameObject ButtonActionWithObj ;
    public bool Add(SourceItemSlot item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("not enough room");
                return false;
            }
            items.Add(item);
            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();

            }
        }
        return true;
    }

    public void Remove(SourceItemSlot item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();

        }

    }


    
}
