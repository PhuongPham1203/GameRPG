using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.XR.WSA.Input;

public class DialogManager : MonoBehaviour
{
    [Header("Name and TextTalk Display")]
    public Text nameTalker;
    public Text textDisplay;

    [Header("Dialog")]
    public Dialog dialog ;

    //public string[] sentences;
    public List<Sentence> sentences = new List<Sentence>();
    private int index=0;
    public float typingSpeed = 0.02f;

    private TypeTalk typeTalk = TypeTalk.NormalTalk;

    public GameObject continueButton;
    public GameObject dialogTextTalk;

    private void Start()
    {
        //this.dialog = new Dialog();
    }
    /*
    public void SetDialog(Dialog d)
    {
        this.dialog = d;
    }
    */
    IEnumerator Type()
    {
        // for name talker
        this.nameTalker.text = this.sentences[index].nameTalker;

        this.textDisplay.text = "";

        // index_lang : 0 - EN , 1 - VI
        int index_lang = PlayerPrefs.GetInt("_language_index",0); 

        /*
        if (index_lang == 0) // en
        {
            foreach (char letter in this.sentences[index].sentenceEN.ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        else if (index_lang == 1)
        {
            foreach (char letter in this.sentences[index].sentenceVI.ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        */
        foreach (char letter in this.sentences[index].sentencesArray[index_lang].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        Debug.Log("Can press continue");
        continueButton.SetActive(true);

    }
    public void SkipSentence()
    {
        this.StopAllCoroutines();
        this.textDisplay.text = "";
        continueButton.SetActive(false);
        dialogTextTalk.SetActive(false);

        // done conversation
        if (this.typeTalk == TypeTalk.NormalTalk)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);// button close
            this.transform.GetChild(1).gameObject.SetActive(true);// list talk
        }
        else if (this.typeTalk == TypeTalk.QuestTalkBefore)
        {
            this.transform.parent.GetChild(2).gameObject.SetActive(true); // Window (Quest) 
            this.gameObject.SetActive(false); // Window (Talk)
        }else if (this.typeTalk == TypeTalk.QuestTalkAfter)
        {
            this.gameObject.SetActive(false); // Window (Talk)

        }
    }
    public void NextSentence()
    {

        continueButton.SetActive(false);

        if (index < this.sentences.Count - 1)
        {
            this.index++;
            this.textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            this.SkipSentence();
            /*
            this.textDisplay.text = "";
            continueButton.SetActive(false);
            dialogTextTalk.SetActive(false);

            // done conversation
            if(this.typeTalk == TypeTalk.NormalTalk)
            {
                this.transform.GetChild(0).gameObject.SetActive(true);// button close
                this.transform.GetChild(1).gameObject.SetActive(true);// list talk
            }else if(this.typeTalk == TypeTalk.QuestTalkBefore)
            {
                transform.parent.GetChild(2).gameObject.SetActive(true); // Window (Quest) 
                this.gameObject.SetActive(false); // Window (Talk)
            }else if (this.typeTalk == TypeTalk.QuestTalkAfter)
            {
                this.gameObject.SetActive(false); // Window (Talk)

            }
            */

        }
    }

    public void StartConversation(Dialog d, TypeTalk type)
    {
        Debug.Log("Start Conversation ");

        this.typeTalk = type;

        this.dialog = d;

        if(this.typeTalk == TypeTalk.NormalTalk)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);// button close
            this.transform.GetChild(1).gameObject.SetActive(false);// list talk
        }


        this.index = 0;
        this.sentences = this.dialog.sentences;

        dialogTextTalk.SetActive(true);
        StartCoroutine(Type());

    }

    
}

public enum TypeTalk { NormalTalk,QuestTalkBefore, QuestTalkAfter }