using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEditor.SceneManagement;
using UnityEngine;
[CreateAssetMenu(fileName = "New Dialog", menuName = "Inventory/Dialog")]

public class Dialog : ScriptableObject
{
    [Header("This name will show in Game")]
    public string nameDialogEN = "Name Dialog";
    public string nameDialogVI = "Tên Dialog";
    [TextArea]
    public string information = "";
    public List<Sentence> sentences = new List<Sentence>();
}

[System.Serializable]
public class Sentence
{
    public string nameTalker = "";
    [Header("For English")]
    [TextArea(4, 5)]
    public string sentenceEN = "";

    [Header("For VietNam")]
    [TextArea(4, 5)]
    public string sentenceVI = "";

    public Sentence()
    {

    }
}