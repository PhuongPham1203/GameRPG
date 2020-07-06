using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemPickup : Interactable
{
    // Start is called before the first frame update
    public Item item;
    public override void Interact()
    {
        base.Interact();

        //PickUp()
    }

    public override void PickUp()
    {


        bool wasPickedUp = Inventory.instance.Add(item);
        Debug.Log("Pickup");
        if (wasPickedUp)
        {
            try
            {
                inventory.ButtonActionWithObj.SetActive(false);
                Destroy(gameObject);

            }
            catch (Exception e)
            {
                Debug.Log("can't Destroy");
            }

        }

    }

}
