using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public int totalNumber = 1;
    protected Inventory inventory;
    private void Start()
    {
        inventory = Inventory.instance;
    }

    // Start is called before the first frame update
    public virtual void Interact()
    {
        //Debug.Log("interactable" + transform.name);
        this.PickUp();
    }
    public virtual void PickUp()
    {
        //Debug.Log("PickUp" + transform.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            inventory.ButtonActionWithObj.SetActive(true);
            Button btn = inventory.ButtonActionWithObj.GetComponent<Button>();
            
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(Interact);

            //inventory.ButtonActionWithObj.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 24)
        {
            Button btn = inventory.ButtonActionWithObj.GetComponent<Button>();

            btn.onClick.RemoveAllListeners();

            inventory.ButtonActionWithObj.SetActive(false);
        }
    }

}
