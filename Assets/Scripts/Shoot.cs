using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject arrow;
    public Camera fpsCam;
    public Transform arrowSpawn;
    public float shootForce = 20f;
    private float shotTimer = 1.7f;
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
        // Shot timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0f;
            canFire = true;
        } else
        {
            canFire = false;
        }

        // Shoot Arrow
        if (Input.GetMouseButtonDown(0) && PlayerController.isDrawn == true && canFire == true)
        {
            timer = shotTimer; // reset timer
            PlayerController.setShot1(); // activates animation
            GameObject clone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation) as GameObject; // spawns arrow
            Rigidbody rb = clone.GetComponent<Rigidbody>(); // gets Rigidbody of cloned arrow
            rb.velocity = fpsCam.transform.forward * shootForce; // applies velocity to it in direction facing
            clone.transform.rotation = Quaternion.LookRotation(rb.velocity); // adds arc to arrow
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GameObject.FindWithTag("Player").GetComponent<Collider>()); // ignore collision w/ player
        }
        if (Input.GetMouseButtonUp(0))
        {
            PlayerController.setShot0(); // ends animation
        }

    }
}
