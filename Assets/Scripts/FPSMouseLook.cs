using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouseLook : MonoBehaviour
{

    public float mouseSensitivity = 1000f;

    public Transform playerBody;
    public Transform lArm, head;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        

        float pitch = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= pitch;
        xRotation = Mathf.Clamp(xRotation, -80f, 45f); // clamps min and max rotation

        lArm.localRotation = Quaternion.Euler(xRotation, 50f, 0f); // rotates arm up and down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // rotates camera up and down
        //head.rotation = Quaternion.Euler(xRotation, 0f, 0f); // rotates head up and down

        float yaw = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * yaw); // rotates player left and right

        //transform.localRotation = head.localRotation;
    }
}