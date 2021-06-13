using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{

    [Header("Component References")]
    //public Rigidbody playerRigidbody;
    public CharacterController controller;

    [Header("Movement Settings")]
    public float movementSpeed = 1f;
    public float turnSpeed = 0.01f;


    //Stored Values
    private Camera mainCamera;
    private Vector3 movementDirection;
    private float v = 0.0f;
    private float h = 0.0f;

    void Start()
    {
        movementSpeed = 1f;
    }

    public void SetupBehaviour()
    {
        SetGameplayCamera();
    }

    void SetGameplayCamera()
    {
        //mainCamera = CameraManager.Instance.GetGameplayCamera();
    }

    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        movementDirection = newMovementDirection;
        h = movementDirection.x;
        v = movementDirection.z;
        Debug.Log(movementDirection);
    }

    void Update()
    {
        //MoveThePlayer();
        MoveThePlayerUp();
        MoveThePlayerRight();
        //TurnThePlayer();
    }

    void MoveThePlayer()
    {
        Vector3 movement = CameraDirection(movementDirection) * movementSpeed * Time.deltaTime;
        controller.Move(movement * Time.deltaTime);
        //playerRigidbody.MovePosition(transform.position + movement);
    }

    public void setRunSpeed()
    {
        movementSpeed = 2f;
    }

    public void setWalkSpeed()
    {
        movementSpeed = 1f;
    }

    void MoveThePlayerUp()
    {
        // up/down
        if (v > 0)
        {
            movementDirection = transform.TransformDirection(Vector3.forward);//moves player forward in direction he is facing

        }
        else if (v < 0)
        {
            movementDirection = transform.TransformDirection(-Vector3.forward);//moves player back in direction he is facing
        }

        controller.Move(movementDirection * Time.deltaTime * movementSpeed);
    }

    void MoveThePlayerRight()
    {
        // left/right
        if (h > 0)
        {
            movementDirection = transform.TransformDirection(Vector3.right); //moves player to right/left in direction he is facing
        }
        else if (h < 0)
        {
            movementDirection = transform.TransformDirection(-Vector3.right); //moves player to right/left in direction he is facing
        }

        controller.Move(movementDirection * Time.deltaTime * movementSpeed);
    }

    /*void TurnThePlayer()
    {
        if(movementDirection.sqrMagnitude > 0.01f)
        {

             Quaternion rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation (CameraDirection(movementDirection)),
                                                  turnSpeed);

            playerRigidbody.MoveRotation(rotation);

        }
    }*/


    Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        
        return cameraForward * movementDirection.z + cameraRight * movementDirection.x; 
   
    }

}
