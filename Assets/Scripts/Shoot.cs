using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject arrow;
    public Camera fpsCam;
    public Transform arrowSpawn;
    public float shootForce = 20f;
    private float shotTimer = 1.5f;
    private float timer;
    private bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // shot timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0f;
            canFire = true;
        } else
        {
            canFire = false;
        }

        if (Input.GetMouseButtonDown(0) && PlayerController.isDrawn == true && canFire == true)
        {
            timer = shotTimer;
            PlayerController.setShot1();
            GameObject clone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation) as GameObject;
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.velocity = fpsCam.transform.forward * shootForce;
        }
        if (Input.GetMouseButtonUp(0))
        {
            PlayerController.setShot0();
        }

    }
}
