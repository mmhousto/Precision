using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class Shoot : MonoBehaviour {

    public GameObject arrow;
    public Transform arrowSpawn;
    public float shootForce = 10f;
    public Animator anim;
    public Slider power;
    private StarterAssetsInputs _input;

    private float shotTimer = 1.75f;
    private float timer;
    public static bool canFire = true;
    private float shotStrength = 0.0f;
    private bool pullingBack = false;
    private bool hasPulledBack = false;

    

    // Start is called before the first frame update
    void Start() {
        timer = 0f;
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfCanFire();
        Fire();


    }

    private void CheckIfCanFire()
    {
        // Shot timer
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0f;
            canFire = true;

        }
        else
        {
            canFire = false;

        }
    }

    private void Fire()
    {
        //Check If Pulling Back
        pullingBack = _input.hasPulledBack;
        if(pullingBack == true)
        {
            hasPulledBack = true;
            
        } else if (pullingBack == false && canFire == true && hasPulledBack == true)
        {
            hasPulledBack = false;
            var pullBack = Mathf.Clamp(shotStrength, 0f, 5f); // clamps shotStrength if greater than 5
            power.value = shotStrength;

            timer = shotTimer; // reset timer

            GameObject clone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation) as GameObject; // spawns arrow
            Rigidbody rb = clone.GetComponent<Rigidbody>(); // gets Rigidbody of cloned arrow
            rb.velocity = Camera.main.transform.forward * shootForce * pullBack; // applies velocity to it in direction facing
            clone.transform.rotation = Quaternion.LookRotation(rb.velocity); // adds arc to arrow (rotates arrow down)
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GameObject.FindWithTag("Player").GetComponent<Collider>()); // ignore collision w/ player

            anim.SetTrigger("Shot");
        } else
        {
            hasPulledBack = false;
        }
    }

    private void AnimatePullBack()
    {
        if (pullingBack && canFire)
        {
            shotStrength += 0.02f;
            anim.SetFloat("ShotStrength", shotStrength);
            power.value = shotStrength;
        }
        else if (canFire == false)
        {
            shotStrength = 0f;
            anim.SetFloat("ShotStrength", shotStrength);
            power.value = shotStrength;
        }
    }

    private void LateUpdate()
    {
        AnimatePullBack();
    }
}
