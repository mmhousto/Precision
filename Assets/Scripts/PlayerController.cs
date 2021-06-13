using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotationSpeed = 280.0f;
    public CharacterController controller;
    public GameObject reticle;
    public Transform head, lArm;

    private Vector3 moveDirection;
    private float horizontal, vertical;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private bool groundedPlayer, jump, isAiming, isFP = false;
    private Vector3 playerVelocity;

    void Start()
    {
        //transform.rotation = Camera.main.transform.rotation;
    }

    // resets gravity
    void UpdateGravity()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -1f;
        }
    }

    // applies gravity to character
    void ApplyGravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void Update()
    {
        MovePlayer();
        ApplyGravity();
        UpdateGravity();
        Jump();
    }

    void LateUpdate()
    {
        if(isFP)
        {

        } else
        {
            head.transform.rotation = Camera.main.transform.rotation;
        }
        

    }

    // makes player jump
    void Jump()
    {
        if (jump && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

    }

    // updates player position
    void MovePlayer()
    {
        if (isFP)
        {
            moveDirection = Vector3.forward * vertical + Vector3.right * horizontal;

            Vector3 projectCameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(projectCameraForward, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToCamera, rotationSpeed * Time.deltaTime);

            moveDirection = rotationToCamera * moveDirection;

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        }
        else
        {
            moveDirection = Vector3.forward * vertical + Vector3.right * horizontal;

            Vector3 projectCameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
            Quaternion rotationToCamera = Quaternion.LookRotation(projectCameraForward, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToCamera, rotationSpeed * Time.deltaTime);

            moveDirection = rotationToCamera * moveDirection;

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        
    }

    // gets the players movement
    public void OnMoveInput(float horizontal, float vertical)
    {
        this.horizontal = horizontal;
        this.vertical = vertical;
    }

    // sets reticle and updates view on aim
    public void OnAimInput(bool isDrawn)
    {
        reticle.SetActive(isDrawn);
        CameraManager.Instance.UpdateAimCamera(isDrawn);
        isAiming = isDrawn;
    }

    // increases running speed on run
    public void OnRunInput(bool isRunning)
    {
        if (isRunning)
        {
            moveSpeed = 2f;
        }
        else if (!isRunning)
        {
            moveSpeed = 1f;
        }
    }

    // switches to first person view/ vice-versa
    public void OnSwitchInput()
    {
        CameraManager.Instance.switchViews();
        isFP = !isFP;
    }

    // sets jump bool to true to activate Jump function
    public void OnJumpInput(bool inAir)
    {
        jump = inAir;
    }

}