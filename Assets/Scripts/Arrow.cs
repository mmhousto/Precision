using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody arrowRB;
    private float lifeTimer = 2f;
    private float timer;
    public float speed = 20f;
    private bool hit = false;
    private Animator anim;

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
        //transform.position += transform.right * speed * Time.deltaTime;
        //arrowRB.velocity -= arrowRB.velocity * Time.deltaTime;
        //transform.rotation = Quaternion.LookRotation(arrowRB.velocity);

        //life timer
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Destroy(gameObject);
        }
        
        if (!hit)
        {

        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hit = true;
        Stick();
    }

    private void Stick()
    {
        arrowRB.constraints = RigidbodyConstraints.FreezeAll;
    }
}
