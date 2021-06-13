using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    //getter setter for FP
    public bool FirstPerson
    {
        get
        {
            return firstPerson;
        }
        set
        {
            firstPerson = value;
        }
    }

    private bool firstPerson;
    private bool isAiming = false;
    public GameObject fppCam, tppCam, aimCam;
    private GameObject activeCamera;

    public void SetupManager()
    {
        if (FirstPerson == false)
        {
            setTPView();
        }
        else if (FirstPerson == true)
        {
            setFPView();
        }
    }

    public void UpdateAimCamera(bool value)
    {
        if (FirstPerson == false)
        {
            isAiming = value;

            if (isAiming == true)
            {
                setAimView();
            }
            else if (isAiming == false)
            {
                setTPView();
            }
        }
    }

    public void switchViews()
    {
        FirstPerson = !FirstPerson;
        SetupManager();
    }

    public void setAimView()
    {
        SetCameraObjectNewState(tppCam, false);
        SetCameraObjectNewState(fppCam, false);
        SetCameraObjectNewState(aimCam, true);
    }

    public void setFPView()
    {
        SetCameraObjectNewState(tppCam, false);
        SetCameraObjectNewState(aimCam, false);
        SetCameraObjectNewState(fppCam, true);
    }

    public void setTPView()
    {
        SetCameraObjectNewState(tppCam, true);
        SetCameraObjectNewState(fppCam, false);
        SetCameraObjectNewState(aimCam, false);
    }

    void SetCameraObjectNewState(GameObject cameraObject, bool newState)
    {
        cameraObject.SetActive(newState);
    }

    public Transform GetGameplayCamera()
    {
        if (FirstPerson == false)
        {
            activeCamera = tppCam;
        } else if (FirstPerson == true)
        {
            activeCamera = fppCam;
        }
        return activeCamera.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        FirstPerson = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
