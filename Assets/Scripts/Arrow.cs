using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody arrowRB;
    private float lifeTimer = 2f;
    private float timer;
    public float speed = 20f;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        timer = lifeTimer;
        initialRotation = transform.rotation;
        arrowRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.right = arrowRB.velocity;
        Debug.Log(arrowRB.velocity);
        if (arrowRB.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(arrowRB.velocity) * initialRotation;
        }

        //life timer
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Stick();
    }

    private void Stick()
    {
        arrowRB.constraints = RigidbodyConstraints.FreezeAll;
    }
}
