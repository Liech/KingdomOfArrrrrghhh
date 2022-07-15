using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionOnKeypress : MonoBehaviour
{
    Vector3 position;
    public KeyCode triggerKey = KeyCode.Return;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(triggerKey)) {
            transform.position = position;
            if (transform.GetComponent<Rigidbody>()) {
                var a = transform.GetComponent<Rigidbody>();
                a.velocity = new Vector3();
                a.angularVelocity = new Vector3();
            }
        }

    }
}
