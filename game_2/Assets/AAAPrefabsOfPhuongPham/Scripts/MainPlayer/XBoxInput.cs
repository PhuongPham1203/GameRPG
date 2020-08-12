using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class XBoxInput : MonoBehaviour
{
    [Space]
    [Header("Camera")]
    public CinemachineFreeLook LookCamera3;
    public bool isNotPressScreen = true;
    public PlayerController playerController;
    // Start is called before the first frame update

    public Button bt;

    // Update is called once per frame
    void Update()
    {
        if (isNotPressScreen)
        {
            LookCamera3.m_XAxis.m_InputAxisValue = Input.GetAxis("Mouse X");
            LookCamera3.m_YAxis.m_InputAxisValue = Input.GetAxis("Mouse Y");
        }
        if (Input.GetButtonDown("A Button"))
        {
            playerController.JumpPlayer();
            
        }

        if (Input.GetButtonDown("B Button"))
        {
            playerController.Dash();
        }

        if(Input.GetButtonDown("X Button"))
        {
            
        }

    }
    public void PressOnScreen(bool press)
    {
        isNotPressScreen = !press;
    }

    public void TestClick()
    {
        Debug.Log("Click");
    }

}

/*
    float rTriggerFloat = Input.GetAxis("Right Trigger");
    float lTriggerFloat = Input.GetAxis("Left Trigger");
    bool leftBumper = Input.GetButton("Left Bumper");
    bool rightBumper = Input.GetButton("Right Bumper");
    bool backButton = Input.GetButton("Back");
    bool startButton = Input.GetButton("Start");
    bool aButton = Input.GetButton("A Button");
    bool bButton = Input.GetButton("B Button");
    bool xButton = Input.GetButton("X Button");
    bool yButton = Input.GetButton("Y Button");
    float dpadHorizontal = Input.GetAxis("Dpad Horizontal") * movementSpeed;
    float dpadVertical = Input.GetAxis("Dpad Vertical") * movementSpeed;
    float moveHL = Input.GetAxis("Horizontal") * movementSpeed;
    float moveVL = Input.GetAxis("Vertical") * movementSpeed;
    float moveHR = Input.GetAxis("Mouse X") * movementSpeed;
    float moveVR = Input.GetAxis("Mouse Y") * movementSpeed;
*/