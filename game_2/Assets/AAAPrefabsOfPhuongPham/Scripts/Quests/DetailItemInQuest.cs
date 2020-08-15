using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class DetailItemInQuest
{

    public SourceItemSlot item;

    [Range(0, 99999)]
    public int numberTarget = 0;
    [SerializeField]
    [Range(0, 99999)]
    private int numberCurrent = 0;

    public void SetNumberCurrent(int number)
    {
        if(number < 0)
        {
            number = 0;
        }
        numberCurrent = number;
    }
    public bool AddItem(int number)
    {
        numberCurrent += number;
        if (numberCurrent >= numberTarget)
        {
            return true;
        }

        return false;
    }

    public int GetItem()
    {
        return numberCurrent;
    }

    public bool IsDone()
    {

        if (numberCurrent>=numberTarget)
        {
            return true;
        }

        return false;
    }

}
