using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSounds : MonoBehaviour {
    [Header("Audio")]
    public AK.Wwise.Event TreeFalls;

    bool hasFallen = false;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        var upDir = transform.rotation * new Vector3(0.0f, 1.0f, 0.0f);
        float upness = Vector3.Dot(upDir, new Vector3(0, 1, 0));
        if (upness < 0.5) {
            if (!hasFallen) {
                hasFallen = true;
                Debug.Log("Tree falls");
                TreeFalls.Post(gameObject);
            }
        }

    }
}
