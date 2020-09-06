using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [Header("Time Count Down")]
    [Range(0,15f)]
    public float timeCoundDown = 0;

    public float currentTimeCountDown = 0;
    public Image UITime;

    public Button UIButton;

    Coroutine setTimeCountDown;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimeCountDown>0)
        {
            currentTimeCountDown -= Time.deltaTime;
            currentTimeCountDown = Mathf.Clamp01(currentTimeCountDown/timeCoundDown);

            UITime.fillAmount = currentTimeCountDown;
        }
    }

    public void ResetTimeCountDown()
    {
        currentTimeCountDown = timeCoundDown;

        if(setTimeCountDown != null)
        {
            StopCoroutine(setTimeCountDown);
        }

        setTimeCountDown = StartCoroutine(SetTime(timeCoundDown));
    }

    IEnumerator SetTime(float t)
    {
        UIButton.interactable = false;

        yield return new WaitForSeconds(t);

        UIButton.interactable = true;

    }

}
