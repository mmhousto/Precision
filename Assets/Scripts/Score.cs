using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    static Vector3 center = new Vector3(0f, 2f, 0f);
    static Vector3 yellow = new Vector3(0.3f, 0.3f, 0.3f);
    static Vector3 red = new Vector3(0.7f, 0.7f, 0.7f);
    static Vector3 blue = new Vector3(0.9f, 0.9f, 0.9f);
    static Vector3 black = new Vector3(1.2f, 1.2f, 1.2f);

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public static void AddPoints(Vector3 relPos) {
        var hitPos = relPos - center;
        var absX = Mathf.Abs(hitPos.x);
        var absY = Mathf.Abs(hitPos.y);
        var absZ = Mathf.Abs(hitPos.z);

        if (hitPos == Vector3.zero) {
            Debug.Log("Bullseye");

        } else if(absX <= yellow.x && absY <= yellow.y && absZ <= yellow.z) {
            Debug.Log("Yellow");

        } else if (absX <= red.x && absY <= red.y && absZ <= red.z) {
            Debug.Log("Red");

        } else if (absX <= blue.x && absY <= blue.y && absZ <= blue.z) {
            Debug.Log("Blue");

        } else if (absX <= black.x && absY <= black.y && absZ <= black.z) {
            Debug.Log("Black");
        } else {
            Debug.Log("White");

        }
    }
}
