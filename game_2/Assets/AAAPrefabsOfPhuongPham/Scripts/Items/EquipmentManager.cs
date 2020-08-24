using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    private void Awake()
    {
        if (instance != null)
        {

            Debug.LogWarning("More than one instance of EquipmentManager found!!!");
            Destroy(this);

            return;

        }
        instance = this;
    }
    #endregion

    public Equipment[] defaultItems;
    //public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
    //SkinnedMeshRenderer[] currentMeshes;
    public GameObject[] parentEquipment = new GameObject[5];
    
    

    Inventory inventory;
    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        //currentMeshes = new SkinnedMeshRenderer[numSlots];
        //EquipAllDefaultItems();

        //Invoke("EquipAllDefaultItems",1f);

    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnEquipAll();
        }
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        //UnEquip(slotIndex);
        Equipment oldItem = UnEquip(slotIndex);


        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            //inventory.Add(oldItem);
            if (!oldItem.isDefaultItem)// if Equipment is not default Equipment
            {
                inventory.Add(oldItem); // Add back Inventory

            }
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        //SetEquipmentBlendShapes(newItem, 100);

        //insert item to slot
        currentEquipment[slotIndex] = newItem;
        Instantiate(newItem.prefabObj,parentEquipment[slotIndex].transform);
        

        /*
        SkinnedMeshRenderer newMesh = Inst  antiate<SkinnedMeshRenderer>(newItem.mesh);
        
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
        */
    }

    public Equipment UnEquip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            
            if(currentEquipment[slotIndex] != null)
            {
                //Destroy(currentEquipment[slotIndex].prefabObj);
                Destroy(parentEquipment[slotIndex].GetComponentInChildren<ItemPickup>().gameObject);
            }
            

            // add item to inventory
            Equipment oldItem = currentEquipment[slotIndex];

            if (!oldItem.isDefaultItem)// if Equipment is not default Equipment
            {
                inventory.Add(oldItem); // Add back Inventory

            }
            //remove item from equipment
            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        /*
        foreach(EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight( (int)blendShape,weight );
        }
        */
    }
    public void EquipAllDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }

        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.ResetAllCurrentAndMaxValue();

        //ResetAllCurrentAndMaxValue(startHP + hp.GetValue(), startAttackDame + attackDame.GetValue(), startPosture + posture.GetValue());

    }

    void EquipDefaultItems(int positon) // ! 0=Hair 1=Clothes 2=LightWeapon 3=HeavyWeapon 4=Bow
    {
        if (positon == 2)
        {
            Equip(defaultItems[0]);
        }
        else if (positon == 3)
        {
            Equip(defaultItems[1]);

        }
    }
    public void UnEquipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            UnEquip(i);
        }
        EquipAllDefaultItems();
    }


}
