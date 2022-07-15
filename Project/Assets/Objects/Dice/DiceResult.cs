using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DiceResult : MonoBehaviour {
    public List<Transform> DicePlanes;


    Rigidbody body;
    bool currentlyRolling = false;

    // Start is called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (body.velocity.magnitude < 1e-4 && currentlyRolling) {
            Debug.Log("Rolling Dice Result: " + (getBestPlane() + 1).ToString());
            currentlyRolling = false;
        }
        if (body.velocity.magnitude > 1)
            currentlyRolling = true;
    }

    int getBestPlane() {
        var myDir = new Vector3(0.0f, 1.0f, 0.0f);
        float bestMatchValue = float.PositiveInfinity;
        int bestIndex = 0;
        for (int i = 0; i < DicePlanes.Count; i++) {
            var planeDir = DicePlanes[i].rotation * -new Vector3(0.0f, 1.0f, 0.0f);
            float matchValue = Vector3.Dot(myDir, planeDir);
            if (matchValue < bestMatchValue) {
                bestIndex = i;
                bestMatchValue = matchValue;
            }
        }

        return bestIndex;
    }
}
