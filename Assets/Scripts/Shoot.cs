using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {

    public GameObject arrow;
    public Transform arrowSpawn;
    public float shootForce = 10f;
    public Animator anim;
    public Slider power;

    private float shotTimer = 1.75f;
    private float timer;
    public static bool canFire = true;
    private float shotStrength = 0.0f;
    private bool pullingBack = false;

    

    // Start is called before the first frame update
    void Start() {
        timer = 0f;
    }

    public void OnAttackInput(bool isPullingBack)
    {
        if(isPullingBack && canFire == true)
        {
            pullingBack = true;
            
        } else if (isPullingBack == false && canFire == true)
        {
            pullingBack = false;
            var pullBack = Mathf.Clamp(shotStrength, 0f, 5f); // clamps shotStrength if greater than 5
            power.value = shotStrength;

            timer = shotTimer; // reset timer

            GameObject clone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation) as GameObject; // spawns arrow
            Rigidbody rb = clone.GetComponent<Rigidbody>(); // gets Rigidbody of cloned arrow
            rb.velocity = Camera.main.transform.forward * shootForce * pullBack; // applies velocity to it in direction facing
            clone.transform.rotation = Quaternion.LookRotation(rb.velocity); // adds arc to arrow (rotates arrow down)
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GameObject.FindWithTag("Player").GetComponent<Collider>()); // ignore collision w/ player
        } else
        {
            pullingBack = false;
        }
    }
        //StartCoroutine(EndShot());

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

        if (pullingBack && canFire)
        {
            shotStrength += 0.02f;
            anim.SetFloat("ShotStrength", shotStrength);
            power.value = shotStrength;
        } else if (canFire == false)
        {
            shotStrength = 0f;
            anim.SetFloat("ShotStrength", shotStrength);
            power.value = shotStrength;
            StartCoroutine(EndShot());
        }

        /*// Get Pull Back / Shot Strength
        if (Input.GetMouseButton(0) && OldPlayerController.isDrawn == true && canFire == true) {
            shotStrength += 0.02f;
            power.value = shotStrength;
            anim.SetFloat("ShotStrength", shotStrength);
            Debug.Log(shotStrength);
            
        }

        // Shoot Arrow
        if (Input.GetMouseButtonUp(0) && OldPlayerController.isDrawn == true && canFire == true) {
            var pullBack = Mathf.Clamp(shotStrength, 0f, 5f); // clamps shotStrength if greater than 5
            shotStrength = 0.0f; // reset shot strength
            power.value = shotStrength;

            OldPlayerController.setShot1(); // activates animation
            anim.SetFloat("ShotStrength", shotStrength);

            timer = shotTimer; // reset timer

            GameObject clone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation) as GameObject; // spawns arrow
            Rigidbody rb = clone.GetComponent<Rigidbody>(); // gets Rigidbody of cloned arrow
            rb.velocity = fpsCam.transform.forward * shootForce * pullBack; // applies velocity to it in direction facing
            clone.transform.rotation = Quaternion.LookRotation(rb.velocity); // adds arc to arrow (rotates arrow down)
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GameObject.FindWithTag("Player").GetComponent<Collider>()); // ignore collision w/ player

            StartCoroutine(EndShot());

        }

        // resets shot strength
        if (OldPlayerController.isDrawn == false)
        {
            shotStrength = 0.0f;
            power.value = shotStrength;
        }*/

    }

    // waits before ending animation
    IEnumerator EndShot() {
        anim.SetFloat("ShotStrength", 0.0f);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Shot", false);

    }
}
