using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 1000f;

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float yaw = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float pitch = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= pitch;
        xRotation = Mathf.Clamp(xRotation, -75f, 55f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * yaw);
    }
}