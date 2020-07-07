﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeedValue = 8f;
    private float moveSpeed;
    private float currentSpeed = 0f;
    private float speedSmoothVelocity = 0f;
    private float speedSmoothTime = 0.1f;
    private float rotationSpeed = 0.1f;
    [SerializeField]
    private float gravity = 6f;

    public float jumpValue = 6f;

    //public float timeJump = 0f;
    public float timeJumpValue = 0.6f;
    public float timeDashValue = 0.75f;

    public float timeSwing = 0f;
    public float timeSwingValue = 1f;
    private bool canSwing = true;
    public Transform targetSwing;
    public Transform targetSwingDetect;

    [Space]
    [Header("Times")]

    //public float actionTime = 0f;
    public float actionTimeJump = 0f;
    public float actionTimeDash = 0f;
    public float actionTimeSwing = 99f;

    [SerializeField]
    private float typeMove = 1f;

    [Space]
    [Header("Equips")]
    [SerializeField]
    private int weaponPlayer = 1;
    [Space]
    [Header("Attack Light")]
    [SerializeField]
    private int comboAttack = 1;
    private int maxComboValue = 4;
    public bool canAction = true;
    private Coroutine actionCanAction;
    private Coroutine actionLeaveAction;
    private Coroutine actionLeaveAttackCombo;

    [Header("Input")]
    public bool isPressMove = false;
    public FloatingJoystick joystickMovePlayer;
    public FloatingJoystick joystickMoveCamera;
    public Vector2 XZ = Vector2.zero;

    [Space]
    [Header("Camera")]
    public CinemachineVirtualCamera LookCamera1;
    public CinemachineFreeLook LookCamera3;
    public GameObject targetGroup;
    public CinemachineTargetGroup targetGroupCiner ;


    private Transform maincameraTranform;


    private CharacterController characterController;
    private Animator animatorPlayer;

    public Text fps;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        characterController = GetComponent<CharacterController>();
        animatorPlayer = GetComponent<Animator>();
        targetGroupCiner = targetGroup.GetComponent<CinemachineTargetGroup>();
        maincameraTranform = Camera.main.transform;
        moveSpeed = moveSpeedValue;


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime != 0)
        {
            fps.text = (1 / Time.deltaTime).ToString();
        }

        if (timeSwing > 0)
        {
            timeSwing -= Time.deltaTime;
            Vector3 tg = new Vector3(targetSwing.position.x, transform.position.y, targetSwing.position.z);
            transform.LookAt(tg);
            
        }
        if (animatorPlayer.GetInteger("InAction") == 1 && timeSwing <= 0)
        {
            MoveToTargetSwing();

        }
        else
        {
            if (canAction && isPressMove)
            {

                XZ = new Vector2(joystickMovePlayer.Horizontal, joystickMovePlayer.Vertical);
                //if (animatorPlayer.GetInteger("InAction") == 0)
                //{
                animatorPlayer.SetFloat("x", XZ.x);
                animatorPlayer.SetFloat("z", XZ.y);

                //}

                MovePlayer();

            }


            if (!characterController.isGrounded && animatorPlayer.GetInteger("InAction") == 0)
            {
                characterController.Move(new Vector3(0, -gravity * Time.deltaTime, 0));

            }
            else if (animatorPlayer.GetInteger("InAction") == 3)
            {
                characterController.Move(new Vector3(0, jumpValue * Time.deltaTime, 0));
            }
        }
        

    }

    private void FixedUpdate()
    {





    }
    private void LateUpdate()
    {

        if (animatorPlayer.GetBool("LockTarget"))
        {
            /*
            Vector3 tftarget = targetGroup.transform.position;

            Vector3 tg = new Vector3(tftarget.x, transform.position.y, tftarget.z);
            transform.LookAt(tg);
            */
            Transform tftarget = targetGroup.transform;
            Vector3 direction = (tftarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);



        }
    }

    public void PressToMovePlayer(bool move)
    {
        isPressMove = move;
    }

    public void MovePlayer()
    {

        if (animatorPlayer.GetInteger("InAction") == 3 || animatorPlayer.GetInteger("InAction") == 6)
        {


        }
        else
        {
            animatorPlayer.SetInteger("InAction", 0);
        }
        if (animatorPlayer.GetBool("LockTarget"))
        {
            MoveLockTarget();
        }
        else
        {
            MoveNotLockTarget();
        }


    }

    private void MoveNotLockTarget()
    {
        //Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Vector2 moveInput = XZ;
        Vector3 forward = maincameraTranform.forward;
        Vector3 right = maincameraTranform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * XZ.y + right * XZ.x).normalized;

        if (desiredMoveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed);
        }

        float targetSpeed = moveSpeed * XZ.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        characterController.Move(desiredMoveDirection * currentSpeed * Time.deltaTime);

        if (animatorPlayer.GetInteger("InAction") != 3)
        {
            animatorPlayer.SetFloat("SpeedMove", typeMove * XZ.magnitude, speedSmoothTime, Time.deltaTime);

        }

    }

    private void MoveLockTarget()
    {
        //Vector2 moveInput = XZ;
        Vector3 forward = maincameraTranform.forward;
        Vector3 right = maincameraTranform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * XZ.y + right * XZ.x).normalized;


        //transform.Translate(new Vector3(XZ.x*moveSpeed, gravityVector,XZ.y*moveSpeed));
        characterController.Move(desiredMoveDirection * moveSpeed * Time.deltaTime);
        //Debug.Log("Move");

    }

    public void ResetXZ()
    {
        XZ = Vector2.zero;
        animatorPlayer.SetFloat("x", 0);
        animatorPlayer.SetFloat("z", 0);
        animatorPlayer.SetFloat("SpeedMove", 0);
    }



    // Controller Camera

    public void MoveCamera()
    {
        LookCamera3.m_XAxis.m_InputAxisValue = joystickMoveCamera.Horizontal;
        LookCamera3.m_YAxis.m_InputAxisValue = -joystickMoveCamera.Vertical;
    }

    public void ResetXYCamera()
    {
        LookCamera3.m_XAxis.m_InputAxisValue = 0;
        LookCamera3.m_YAxis.m_InputAxisValue = 0;
    }
    public void LockTarget()
    {
        //if(targetGroup.)
        if (animatorPlayer.GetBool("LockTarget"))
        {
            animatorPlayer.SetBool("LockTarget", false);
            LookCamera3.m_XAxis.Value = LookCamera1.transform.eulerAngles.y;//GetComponent<Transform>().rotation.y;
            LookCamera3.m_YAxis.Value = 0.5f;

            LookCamera1.enabled = false;
            LookCamera3.enabled = true;

        }
        else if(targetGroupCiner.m_Targets[1].target!=null)
        {
            //Debug.Log(targetGroupCiner.m_Targets[1].target);
            //Debug.Log(targetGroupCiner.m_Targets.Length);
            LookCamera1.enabled = true;
            LookCamera3.enabled = false;
            animatorPlayer.SetBool("LockTarget", true);

        }
    }

    public void CrouchPlayer()
    {
        if (animatorPlayer.GetBool("Crouch"))
        {
            animatorPlayer.SetBool("Crouch", false);
            OnCombat(false);
            //moveSpeed = moveSpeedValue ;
        }
        else
        {
            animatorPlayer.SetBool("Crouch", true);
            OnCombat(true);
            //moveSpeed = moveSpeedValue/2;
        }
    }

    public void JumpPlayer()
    {
        if (canAction && animatorPlayer.GetBool("OnGround"))
        {
            animatorPlayer.SetInteger("InAction", 3);
            //timeJump = timeJumpValue;
            if (actionLeaveAction != null)
            {
                StopCoroutine(actionLeaveAction);
            }
            actionCanAction = StartCoroutine(LeaveJump(timeJumpValue));

            //animatorPlayer.SetTrigger("Jump");
            animatorPlayer.SetBool("Crouch", false);
            if (!animatorPlayer.GetBool("InCombat"))
            {
                OnCombat(false);
            }
            //moveSpeed = moveSpeedValue;
        }
    }

    public void Dash()
    {
        if (canAction && animatorPlayer.GetBool("OnGround"))
        {
            canAction = false;
            if (actionLeaveAction != null)
            {
                StopCoroutine(actionLeaveAction);
            }
            actionCanAction = StartCoroutine(LeaveDash(timeDashValue));
            animatorPlayer.SetTrigger("Dash");
            animatorPlayer.SetInteger("InAction", 4);

            animatorPlayer.SetBool("Crouch", false);
            if (!animatorPlayer.GetBool("InCombat"))
            {
                OnCombat(false);
            }
        }
    }

    public void Block(bool block)
    {
        /*
            if (actionLeaveAction != null)
            {
                StopCoroutine(actionLeaveAction);
            }
            */

        if ((animatorPlayer.GetInteger("InAction")==2 || canAction) && animatorPlayer.GetInteger("InAction") != 3)
        {
            if (actionLeaveAction != null)
            {
                StopCoroutine(actionLeaveAction);
            }
            OnCombat(block);
            
            //animatorPlayer.SetBool("Block", block);
            if (block)
            {

                animatorPlayer.SetInteger("InAction", 6);

            }
            else
            {

                animatorPlayer.SetInteger("InAction", 0);
            }

        }
        


    }

    public void ChangeWeapon()
    {
        if (weaponPlayer == 2)
        {
            weaponPlayer = 1;
            animatorPlayer.SetInteger("Weapon", weaponPlayer);
        }
        else
        {
            weaponPlayer = 2;
            animatorPlayer.SetInteger("Weapon", weaponPlayer);
        }

    }

    public void SwingPlayer()
    {
        if (canAction && targetSwingDetect!=null && animatorPlayer.GetInteger("InAction") != 3)
        {
            if (actionLeaveAction != null)
            {
                //Debug.Log("stop");
                StopCoroutine(actionLeaveAction);
            }
            targetSwing = targetSwingDetect;
            canAction = false;
            //animatorPlayer.SetBool("isSwing", true);
            animatorPlayer.SetInteger("InAction", 1);
            animatorPlayer.SetTrigger("SwingStart");
            timeSwing = timeSwingValue;

        }
    }

    public void Attack()
    {
        if (canAction)
        {

            if (animatorPlayer.GetInteger("InAction") == 3)// player jump
            {

            }
            else
            {
                canAction = false;

                if (actionLeaveAction != null)
                {
                    StopCoroutine(actionLeaveAction);
                    StopCoroutine(actionLeaveAttackCombo);
                }
                actionLeaveAction = StartCoroutine(LeaveAttack(2f));
                actionLeaveAttackCombo = StartCoroutine(LeaveAttackCombo(2f));

                StartCoroutine(CanAcion(0.6f));
                #region Singleton 
                /*
                switch (comboAttack)
                {
                    case 1:
                        if (actionLeaveAction != null)
                        {
                            StopCoroutine(actionLeaveAction);
                            StopCoroutine(actionLeaveAttackCombo);
                        }
                        actionLeaveAction = StartCoroutine(LeaveAttack(2f));
                        actionLeaveAttackCombo = StartCoroutine(LeaveAttackCombo(2f));

                        StartCoroutine(CanAcion(0.5f));
                        break;
                    case 2:
                        if (actionLeaveAction != null)
                        {
                            StopCoroutine(actionLeaveAction);
                            StopCoroutine(actionLeaveAttackCombo);
                        }
                        actionLeaveAction = StartCoroutine(LeaveAttack(2f));
                        actionLeaveAttackCombo = StartCoroutine(LeaveAttackCombo(2f));
                        StartCoroutine(CanAcion(0.5f));

                        break;
                    case 3:
                        if (actionLeaveAction != null)
                        {
                            StopCoroutine(actionLeaveAction);
                            StopCoroutine(actionLeaveAttackCombo);
                        }
                        actionLeaveAction = StartCoroutine(LeaveAttack(2f));
                        actionLeaveAttackCombo = StartCoroutine(LeaveAttackCombo(2f));
                        StartCoroutine(CanAcion(0.5f));
                        break;
                    case 4:
                        if (actionLeaveAction != null)
                        {
                            StopCoroutine(actionLeaveAction);
                            StopCoroutine(actionLeaveAttackCombo);
                        }
                        actionLeaveAction = StartCoroutine(LeaveAttack(2f));
                        actionLeaveAttackCombo = StartCoroutine(LeaveAttackCombo(2f));
                        StartCoroutine(CanAcion(0.5f));
                        break;
                }
                */
                #endregion

                animatorPlayer.SetBool("Crouch", false);
                if (!animatorPlayer.GetBool("InCombat"))
                {
                    OnCombat(false);
                }
                animatorPlayer.SetInteger("InAction", 2);
                animatorPlayer.SetInteger("AttackCombo", comboAttack);

                if (comboAttack == maxComboValue)
                {
                    comboAttack = 1;
                }
                else
                {
                    comboAttack += 1;
                }


            }

        }

    }
    private IEnumerator LeaveDash(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        animatorPlayer.SetInteger("InAction", 0);
        canAction = true;

    }
    private IEnumerator LeaveJump(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        animatorPlayer.SetInteger("InAction", 0);

    }
    private IEnumerator LeaveAttack(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        animatorPlayer.SetInteger("InAction", 0);

    }
    private IEnumerator LeaveAttackCombo(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        comboAttack = 1;
        animatorPlayer.SetInteger("AttackCombo", comboAttack);
    }

    private IEnumerator CanAcion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canAction = true;

    }

    private void MoveToTargetSwing()
    {
        if (targetSwing != null)
        {
            float step = moveSpeedValue * 5 * Time.deltaTime;

            Vector3 direction = transform.position - targetSwing.position;
            direction.Normalize();

            characterController.Move(-direction * step);
        }

        
        //Debug.Log("Intarget"+ -direction*step);

    }

    private void OnCombat(bool combat)
    {
        if (combat)
        {
            moveSpeed = moveSpeedValue / 4;
            typeMove = 0.5f;
            //animatorPlayer.SetBool("InCombat",true);
        }
        else
        {
            moveSpeed = moveSpeedValue;
            typeMove = 1;
            //animatorPlayer.SetBool("InCombat", false);


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 22)
        {
            targetSwing.gameObject.layer = 2;
            targetSwing = null;
            canAction = true;
            //animatorPlayer.SetBool("isSwing", false);
            animatorPlayer.SetInteger("InAction",0);
            
            //other.GetComponent<BoxCollider>().enabled = false;
            //Debug.Log("Intarget");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}
