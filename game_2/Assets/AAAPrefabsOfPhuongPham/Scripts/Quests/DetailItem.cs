using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class DetailItem
{

    public SourceItemSlot item;

    [Range(0, 99999)]
    public int numberTarget = 0;
    [Range(0, 99999)]
    private int numberCurrent = 0;

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
