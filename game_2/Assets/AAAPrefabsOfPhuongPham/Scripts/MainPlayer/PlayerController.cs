using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeedValue = 8f;

    [SerializeField]
    private float moveSpeedValueOnCombat = 3f;
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
    //private bool canSwing = true;
    public Transform targetSwing;
    public Transform targetSwingDetect;
    public GameObject buttonSwing;

    [Space]
    [Header("Times")]

    //public float actionTime = 0f;
    public float actionTimeJump = 0f;
    public float actionTimeDash = 0f;
    public float actionTimeSwing = 99f;

    [SerializeField]
    private float typeMove = 1f;
    public bool onCombat = false;

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

    public ParticleSystem weaponIn;
    public ParticleSystem weaponOut;

    [SerializeField]
    private GameObject weapon1;
    [SerializeField]
    private GameObject weapon2;

    [SerializeField]
    private float timeLeaveLightAttack = 2f;
    [SerializeField]
    private float timeCanLightAttackAction = 0.6f;

    [SerializeField]
    private float timeLeaveHeavyAttack = 4f;
    [SerializeField]
    private float timeCanHeavyAttackAction = 1.6f;
    [SerializeField]
    private float timeDisableWeapon = 5f;

    [SerializeField]
    private bool isPressBlock = false;

    private Coroutine actionCanAction;
    private Coroutine actionLeaveAction;
    private Coroutine actionLeaveAttackCombo;

    private Coroutine actionDisableWeapon;


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
    public CinemachineTargetGroup targetGroupCiner;


    private Transform maincameraTranform;


    public CharacterController characterController;
    public Animator animatorPlayer;

    public Text fps;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        //characterController = GetComponent<CharacterController>();
        //animatorPlayer = GetComponent<Animator>();
        //targetGroupCiner = targetGroup.GetComponent<CinemachineTargetGroup>();
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

        if (isPressBlock)
        {
            Block(isPressBlock);
        }
    }
    public void PlayerDie()
    {
        StopAllCoroutines();
        canAction = false;
    }
    public void PlayerStun()
    {
        canAction = false;
        StopAllCoroutines();
        animatorPlayer.SetInteger("InAction",9);
        StartCoroutine(CanAcion(2f));
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
        if (animatorPlayer.GetBool("LockTarget")) // if On Lock Target Enemy
        {
            animatorPlayer.SetBool("LockTarget", false);
            LookCamera3.m_XAxis.Value = LookCamera1.transform.eulerAngles.y;
            LookCamera3.m_YAxis.Value = 0.4f;

            LookCamera1.enabled = false;
            LookCamera3.enabled = true;

        }
        else if (targetGroupCiner.m_Targets[1].target != null) // if have target in fond
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
            //actionCanAction = StartCoroutine(LeaveJump(timeJumpValue));
            actionLeaveAction = StartCoroutine(LeaveJump(timeJumpValue));

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
            //actionCanAction = StartCoroutine(LeaveDash(timeDashValue));
            actionLeaveAction = StartCoroutine(LeaveDash(timeDashValue));

            animatorPlayer.SetTrigger("Dash");
            animatorPlayer.SetInteger("InAction", 4);

            animatorPlayer.SetBool("Crouch", false);
            if (!animatorPlayer.GetBool("InCombat"))
            {
                OnCombat(false);
            }
        }
    }


    public void SwingPlayer()
    {
        if (canAction && targetSwingDetect != null && animatorPlayer.GetInteger("InAction") != 3)
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
                }
                if (actionLeaveAttackCombo != null)
                {
                    StopCoroutine(actionLeaveAttackCombo);
                }

                SetActivateWeapon(weaponPlayer);


                if (weaponPlayer == 1)
                {//Light Weapon


                    

                    actionLeaveAction = StartCoroutine(LeaveAttack(timeLeaveLightAttack));
                    actionLeaveAttackCombo = StartCoroutine(LeaveAttackCombo(timeLeaveLightAttack));

                    StartCoroutine(CanAcion(timeCanLightAttackAction));
                }
                else// Heavy Weapon
                {
                    

                    actionLeaveAction = StartCoroutine(LeaveAttack(timeLeaveHeavyAttack));
                    actionLeaveAttackCombo = StartCoroutine(LeaveAttackCombo(timeLeaveHeavyAttack));

                    StartCoroutine(CanAcion(timeCanHeavyAttackAction));
                }

                animatorPlayer.SetTrigger("TriggerAttack");


                animatorPlayer.SetBool("Crouch", false);
                /*
                if (!animatorPlayer.GetBool("InCombat"))
                {
                    OnCombat(false);
                }
                */
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

    public void Block(bool block)
    {
        /*
            if (actionLeaveAction != null)
            {
                StopCoroutine(actionLeaveAction);
            }
            */


        if (( /*animatorPlayer.GetInteger("InAction") == 2 || */ canAction) && animatorPlayer.GetInteger("InAction") != 3)
        {
            if (actionLeaveAction != null)
            {
                StopCoroutine(actionLeaveAction);
            }
            //OnCombat(block);

            //animatorPlayer.SetBool("Block", block);
            if (block)// Block
            {
                SetActivateWeapon(weaponPlayer);
                StopCoroutine(actionDisableWeapon);

                //OnCombat(block);
                moveSpeed = moveSpeedValueOnCombat;
                typeMove = 0.5f;
                animatorPlayer.SetInteger("InAction", 6);
            }
            else // Not Block
            {
                if (actionDisableWeapon != null)
                {
                    StopCoroutine(actionDisableWeapon);
                }
                actionDisableWeapon = StartCoroutine(OutWeapon(weaponPlayer, timeDisableWeapon));

                if (!onCombat) // Not in Combat
                {
                    moveSpeed = moveSpeedValue;
                    typeMove = 1f;
                }
                animatorPlayer.SetInteger("InAction", 0);
                isPressBlock = block;
            }

        }
        else if (block)
        {
            isPressBlock = block;
        }
        else if (!block)
        {
            isPressBlock = block;
        }



    }

    public void Damage()
    {
        animatorPlayer.SetTrigger("TriggerDamage");

        if (actionCanAction != null)
        {
            StopCoroutine(actionCanAction);

        }


        canAction = false;
        actionCanAction = StartCoroutine(CanAcion(0.5f));
    }

    void SetActivateWeapon(int weapon)
    {


        if (actionDisableWeapon != null)
        {
            StopCoroutine(actionDisableWeapon);
        }
        actionDisableWeapon = StartCoroutine(OutWeapon(weapon, timeDisableWeapon));
        
        
        if (weapon == 1)
        {
            if (!weapon1.activeSelf)
            {
                weaponIn.Play();
            }

            weapon1.SetActive(true);
            weapon2.SetActive(false);
            
        }
        else
        {
            if (!weapon2.activeSelf)
            {
                weaponIn.Play();
            }

            weapon1.SetActive(false);
            weapon2.SetActive(true);

        }


    }

    private IEnumerator OutWeapon(int type, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (type == 1)
        {
            weapon1.SetActive(false);
            weaponOut.Play();
        }
        else
        {
            weapon2.SetActive(false);
            weaponOut.Play();
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

    public void OnCombat(bool combat)
    {

        onCombat = combat;
        if (combat)
        {
            moveSpeed = moveSpeedValueOnCombat;
            typeMove = 0.5f;
            //animatorPlayer.SetBool("InCombat",true);
        }
        else
        {
            moveSpeed = moveSpeedValue;
            typeMove = 1f;
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
            animatorPlayer.SetInteger("InAction", 0);

            //other.GetComponent<BoxCollider>().enabled = false;
            //Debug.Log("Intarget");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}
