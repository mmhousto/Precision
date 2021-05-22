using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody arrowRB;
    //Animator anim;
    private float lifeTimer = 6f;
    private float timer;
    private bool hit;
    float rotationSpeed = 45;
    Vector3 eulerAngleVelocity;

    // Start is called before the first frame update
    void Start()
    {
        timer = lifeTimer;
        arrowRB = GetComponent<Rigidbody>();
        hit = false;

        //Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)
        eulerAngleVelocity = new Vector3(0, 0, rotationSpeed);
        //anim = GetComponent<Animator>();
        //anim.SetBool("inAir", true);
    }

    // Update is called once per frame
    void Update()
    {
        
        //life timer
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Destroy(gameObject);
        }

    }

    void LateUpdate()
    {
        if (arrowRB.velocity != Vector3.zero && hit == false)
        {
            transform.rotation = Quaternion.LookRotation(arrowRB.velocity);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //anim.SetBool("inAir", false);
        arrowRB.isKinematic = true;
        hit = true;
        if(collision.collider.tag == "target")
        {
            timer = lifeTimer;
        }
       
        
    }
}
