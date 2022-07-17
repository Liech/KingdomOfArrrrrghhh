using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSlowly : MonoBehaviour
{
    public float rotationspeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var euler = transform.rotation.eulerAngles;
        euler.y += rotationspeed;
        transform.rotation = Quaternion.Euler(euler);
    }
}
