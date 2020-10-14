using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomManager : MonoBehaviour
{
    public Animation animation;
    public static NomManager instance;
    void Awake()
    {
        if (instance != null)
        {
            //Destroy(this.gameObject);
            Debug.LogWarning("More than one instance of NomManager found!!!");
            Destroy(this);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {

            this.animation.Play("HiemAnimation");

        }
    }


    public void PlayNomAnimation(string nameNomChar)
    {
        this.animation.Play(nameNomChar);
    }

}
