using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody arrowRB;
    private float lifeTimer = 2f;
    private float timer;
    private bool hit;

    // Start is called before the first frame update
    void Start()
    {
        timer = lifeTimer;
        arrowRB = GetComponent<Rigidbody>();
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.right = arrowRB.velocity;

        //life timer
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Destroy(gameObject);
        }

        if (arrowRB.velocity != Vector3.zero && hit == false)
        {
            transform.rotation = Quaternion.LookRotation(arrowRB.velocity);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        arrowRB.isKinematic = true;
        hit = true;
        if(collision.collider.tag == "target")
        {
            timer = lifeTimer;
        }
        
        Debug.Log(collision.collider.tag);
        
    }
}
