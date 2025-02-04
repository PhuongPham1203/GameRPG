﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stat 
{
    //[SerializeField]
    //private int baseValue;// Value base

    private List<int> modifiers = new List<int>();


    public int GetValue()
    {
        //int finalValue = baseValue;
        int finalValue = 0;
        modifiers.ForEach(x => finalValue += x);


        return finalValue;

    }
    /*
    public void SetBaseValue(int baseVar){
        baseValue = baseVar;
    }
    */
    public void AddModifier(int modifier)
    {
        if(modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }


}
