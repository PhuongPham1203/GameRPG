using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour
{
    public static TimeLineManager instance;

    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;

    void Awake()
    {
        if (instance != null)
        {

            Debug.LogWarning("More than one instance of TimeLineManager found!!!");
            Destroy(this);
            
            return;

        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Play TimeLine Finish Boss
            Debug.Log("Play Time Line Finish Bot");
            playableDirectors[0].Play();
        }
    }
}
