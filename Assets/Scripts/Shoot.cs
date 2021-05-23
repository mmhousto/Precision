using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject arrow;
    public Camera fpsCam;
    public Transform arrowSpawn;
    public float shootForce = 10f;
    public Animator anim;
    private float shotTimer = 1.7f;
    private float timer;
    private bool canFire = true;
    private float shotStrength = 0.0f;

    // Start is called before the first frame update
    void Start() {
        timer = 0f;
    }

    // Update is called once per frame
    void Update() {
        // Shot timer
        timer -= Time.deltaTime;
        if (timer <= 0f) {
            timer = 0f;
            canFire = true;

        } else {
            canFire = false;

        }

        // Shoot Arrow
        if (Input.GetMouseButton(0) && PlayerController.isDrawn == true && canFire == true) {
            shotStrength += 0.025f;
            anim.SetFloat("ShotStrength", shotStrength);
            Debug.Log(shotStrength);
            
        }
        if (Input.GetMouseButtonUp(0) && PlayerController.isDrawn == true && canFire == true) {
            var pullBack = Mathf.Clamp(shotStrength, 0f, 4f);
            shotStrength = 0.0f;
            PlayerController.setShot1(); // activates animation
            anim.SetFloat("ShotStrength", shotStrength);
            timer = shotTimer; // reset timer
            GameObject clone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation) as GameObject; // spawns arrow
            Rigidbody rb = clone.GetComponent<Rigidbody>(); // gets Rigidbody of cloned arrow
            rb.velocity = fpsCam.transform.forward * shootForce * pullBack; // applies velocity to it in direction facing
            clone.transform.rotation = Quaternion.LookRotation(rb.velocity); // adds arc to arrow (rotates arrow down)
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GameObject.FindWithTag("Player").GetComponent<Collider>()); // ignore collision w/ player
            StartCoroutine(EndShot());

        }

    }

    IEnumerator EndShot() {
        yield return new WaitForSeconds(0.25f);
        PlayerController.setShot0();

    }
}
