using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        CameraManager.Instance.SetupManager();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
