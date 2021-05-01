using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody arrowRB;
    private float lifeTimer = 6f;
    private float timer;
    private bool hit;
    Animator anim;
    private float rotateZ;

    // Start is called before the first frame update
    void Start()
    {
        timer = lifeTimer;
        arrowRB = GetComponent<Rigidbody>();
        hit = false;
        //anim = GetComponent<Animator>();
        //anim.SetBool("inAir", true);
        rotateZ = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.right = arrowRB.velocity;
        rotateZ += 2.0f;

        //life timer
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Destroy(gameObject);
        }
        transform.Rotate(new Vector3(0f, 0f, rotateZ));

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
        
        Debug.Log(collision.collider.tag);
        
    }
}
