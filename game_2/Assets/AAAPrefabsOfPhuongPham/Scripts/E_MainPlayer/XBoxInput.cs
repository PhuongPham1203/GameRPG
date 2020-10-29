using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Net;

public class XBoxInput : MonoBehaviour
{
    [Space]
    [Header("Camera")]
    public CinemachineFreeLook LookCamera3;
    public bool isNotPressScreen = true;
    public PlayerController playerController;
    // Start is called before the first frame update



    public GameObject buttonFinish;
    public Button buttonCheck;
    public GameObject buttonSwing;

    private bool isDpadNotPress = true;
    private bool isButtonBlockNotPress = true;

    // Update is called once per frame
    void Update()
    {
        if (isNotPressScreen)
        {


            LookCamera3.m_XAxis.m_InputAxisValue = Input.GetAxis("Mouse X");// < 0.15 && Input.GetAxis("Mouse X") > -0.15 ? 0 : Input.GetAxis("Mouse X");
            LookCamera3.m_YAxis.m_InputAxisValue = Input.GetAxis("Mouse Y");// < 0.15 && Input.GetAxis("Mouse Y") > -0.15 ? 0 : Input.GetAxis("Mouse Y");


        }


        if (Input.GetButtonUp("Right Bumper"))
        {
            if (buttonFinish.activeSelf)
            {
                playerController.FinishBot();
            }
            else
            {
                playerController.Attack();
            }
        }

        if (Input.GetButtonUp("TriggerAxisRight"))
        {
            playerController.LockTarget();
        }

        if (Input.GetButtonUp("TriggerAxisLeft"))
        {
            playerController.CrouchPlayer();
        }

        if (isButtonBlockNotPress && (Input.GetButtonDown("Left Bumper") || Input.GetMouseButtonDown(1)))
        {
            playerController.isPressBlock = true;
            isButtonBlockNotPress = false;
            //playerController.Block(true);
        }
        else if (Input.GetButtonUp("Left Bumper") || Input.GetMouseButtonUp(1))
        {
            //playerController.isPressBlock = false;

            isButtonBlockNotPress = true;
            playerController.Block(false);
        }

        if (Input.GetAxis("Left Trigger") > 0.5f)
        {
            if (buttonSwing.activeSelf)
            {
                playerController.SwingPlayer();

            }
        }

        if (isDpadNotPress)
        {
            if (Input.GetAxis("Dpad Vertical") < -0.5f)
            {
                playerController.ChangeWeapon();
                isDpadNotPress = false;
            }
            else if (Input.GetAxis("Dpad Vertical") > 0.5f)
            {
                isDpadNotPress = false;
            }

            if (Input.GetAxis("Dpad Horizontal") < -0.5f)
            {
                //playerController.ChangeWeapon();
                isDpadNotPress = false;
            }
            else if (Input.GetAxis("Dpad Horizontal") > 0.5f)
            {
                isDpadNotPress = false;
            }


        }
        else if (Input.GetAxis("Dpad Vertical") == 0 && Input.GetAxis("Dpad Horizontal") == 0)
        {
            isDpadNotPress = true;
        }



        if (Input.GetButtonDown("A Button"))
        {
            playerController.JumpPlayer();
        }

        if (Input.GetButtonDown("B Button"))
        {
            playerController.Dash();
        }

        if (Input.GetButtonDown("X Button"))
        {
            buttonCheck.onClick.Invoke();
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
 * Input.GetButtonUp("TriggerAxisRight")
 * Input.GetButtonUp("TriggerAxisLeft")
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