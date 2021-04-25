using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float posX, posY = 0.0f;
    private float center = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "arrow")
        {
            float linePosX = col.transform.position.x;
            float linePosY = col.transform.position.y;
            Debug.Log("X: " + linePosX + "Y: " + linePosY);
            Debug.Log("Collision with Target"); 
        }
    }
}
