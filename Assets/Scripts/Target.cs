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
            Vector3 relPos = gameObject.transform.InverseTransformPoint(col.transform.position);
            float linePosX = col.transform.localPosition.x;
            float linePosY = col.transform.localPosition.y;
            Score.AddPoints(relPos);
        }
    }
}
