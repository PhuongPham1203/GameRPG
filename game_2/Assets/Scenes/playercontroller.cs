using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 6f;
    private float currentSpeed = 0f;
    private float speedSmoothVelocity = 0f;
    private float speedSmoothTime = 0.1f;
    private float rotationSpeed = 0.1f;
    private float gravity = 3f;

    private Transform maincameraTranform;


    private CharacterController characterController;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        maincameraTranform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpeed();
    }

    private void MoveSpeed()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 forward = maincameraTranform.forward;
        Vector3 right = maincameraTranform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = (forward * moveInput.y + right * moveInput.x).normalized;
        Vector3 gravityVector = Vector3.zero;
        if (!characterController.isGrounded)
        {
            gravityVector.y -= gravity;

        }

        if (desiredMoveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(desiredMoveDirection),rotationSpeed);
        }

        float targetSpeed = moveSpeed * moveInput.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        characterController.Move(desiredMoveDirection * currentSpeed * Time.deltaTime);

        characterController.Move(gravityVector * Time.deltaTime);

        animator.SetFloat("MovermentSpeed",1f*moveInput.magnitude,speedSmoothTime,Time.deltaTime);

    }
}
