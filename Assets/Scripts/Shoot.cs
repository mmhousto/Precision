using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject arrow;
    public Camera fpsCam;
    public Transform arrowSpawn;
    public float shootForce = 20f;

    // Start is called before fthe first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clone = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation) as GameObject;
            //clone.transform.position = arrowSpawn.position;
            //clone.transform.rotation = transform.rotation;
            //clone.transform.right = fpsCam.transform.forward;
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.velocity = fpsCam.transform.forward * shootForce;
        }
        
    }
}
