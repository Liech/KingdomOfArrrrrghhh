using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wobble : MonoBehaviour
{
    Quaternion startRotation;
    Quaternion addition;
    public float speed = 0.1f;
    public float factor = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    float time = 0;
    void FixedUpdate()
    {
        time += speed;
        addition = new Quaternion(Mathf.Sin(time) * factor, Mathf.Cos(time) * factor, Mathf.Sin(-time) * factor, Mathf.Cos(-time) * factor);
        transform.rotation = new Quaternion(startRotation.x+ addition.x, startRotation.y+ addition.y, startRotation.z + addition.z, startRotation.w + addition.w);
    }
}
