using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkHeader : MonoBehaviour
{
    [Header("Dialog this Header keep")]
    public Dialog dialog;
    private void Awake()
    {
        Toggle t = GetComponent<Toggle>();
        t.group = transform.parent.GetComponent<ToggleGroup>();
    }
    public void StartTalk()
    {
        UIInteracWithNPC uIInteracWithNPC = MenuController.instance.uiInteracWithNPC.GetComponent<UIInteracWithNPC>();

        DialogManager dialogManager = uIInteracWithNPC.dialogManager;

        //Debug.Log(dialogManager.dialog);
        //Debug.Log(this.dialog);
        //dialogManager.SetDialog( this.dialog );


        dialogManager.StartConversation(this.dialog,TypeTalk.NormalTalk);

    }


}
