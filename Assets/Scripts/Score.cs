using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Score : MonoBehaviour {

    static Vector3 center = new Vector3(0f, 2f, 0f);
    static Vector3 yellow = new Vector3(0, 0.3f, 0.4f);
    static Vector3 red = new Vector3(0f, 0.7f, 0.8f);
    static Vector3 blue = new Vector3(0f, 0.9f, 1.2f);
    static Vector3 black = new Vector3(0f, 1.4f, 1.6f);
    public static int score;
    public TextMeshProUGUI scoreLbl;
    private static double absY, absZ;

    // Start is called before the first frame update
    void Start() {
        score = 0;
        scoreLbl.SetText("Score: {0}", score);
    }

    // Update is called once per frame
    void Update() {
        scoreLbl.SetText("Score: {0}", score);
    }

    public static void AddPoints(Vector3 relPos) {
        var hitPos = relPos - center;
        if(hitPos.y < 0 || hitPos.z < 0)
        {
            absY = Mathf.Abs(hitPos.y);
            absZ = Mathf.Abs(hitPos.z);
        } else
        {
            absY = hitPos.y - 0.1;
            absZ = hitPos.z - 0.1;
        }

        Debug.Log(hitPos);
        if (hitPos == Vector3.zero) {
            Debug.Log("Bullseye");
            score += 100;

        } else if(absY <= yellow.y && absZ <= yellow.z) {
            Debug.Log("Yellow");
            score += 90;

        } else if (absY <= red.y && absZ <= red.z) {
            Debug.Log("Red");
            score += 70;

        } else if (absY <= blue.y && absZ <= blue.z) {
            Debug.Log("Blue");
            score += 50;

        } else if (absY <= black.y && absZ <= black.z) {
            Debug.Log("Black");
            score += 30;

        } else {
            Debug.Log("White");
            score += 10;

        }
    }
}
