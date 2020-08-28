using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("Text Display")]
    public Text textDisplay;
    public string[] sentences;
    private int index=0;
    public float typingSpeed = 0.02f;

    public GameObject continueButton;
    public GameObject dialogTextTalk;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (this.textDisplay.text == sentences[this.index])
        {
            Debug.Log("Spam\n SetActivate");
            continueButton.SetActive(true);
        }
        */
    }

    IEnumerator Type()
    {
        this.textDisplay.text = "";
        foreach(char letter in this.sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        Debug.Log("Can press continue");
        continueButton.SetActive(true);

    }

    public void NextSentence()
    {

        continueButton.SetActive(false);


        if (index < sentences.Length - 1)
        {
            this.index++;
            this.textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            this.textDisplay.text = "";
            //continueButton.SetActive(false);
            dialogTextTalk.SetActive(false);

        }
    }


}
