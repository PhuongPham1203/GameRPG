using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllers : MonoBehaviour
{
    public PlayerController playerController;
    public Animator animator;
    private LineRenderer lr;
    public GameObject startRope;
    private bool isSwing = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        
            DrawRope();
        
        
    }

    public void CreateCab()
    {
        //Debug.Log("Create cab");
        isSwing = true;
    }
    public void StopCab()
    {
        isSwing = false;
        lr.positionCount = 0;

    }
    void DrawRope()
    {
        if (isSwing)
        {
            lr.positionCount = 2;
            lr.SetPosition(index: 0, startRope.transform.position);
            lr.SetPosition(index: 1, playerController.targetSwing.transform.position);
        }
        
        
    }
}
