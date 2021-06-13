using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public GameObject inScenePlayer;

    private List<PlayerController> activePlayerControllers;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        SetupSinglePlayer();
        CameraManager.Instance.SetupManager();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void SetupSinglePlayer()
    {
        activePlayerControllers = new List<PlayerController>();

        if (inScenePlayer == true)
        {
            AddPlayerToActivePlayerList(inScenePlayer.GetComponent<PlayerController>());
        }

        SetupActivePlayers();
    }

    void AddPlayerToActivePlayerList(PlayerController newPlayer)
    {
        activePlayerControllers.Add(newPlayer);
    }

    void SetupActivePlayers()
    {
        for (int i = 0; i < activePlayerControllers.Count; i++)
        {
            //activePlayerControllers[i].SetupPlayer(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
