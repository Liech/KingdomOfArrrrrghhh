using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DiceGrabbing : MonoBehaviour
{
    public float momentumFactor = 0.4f;
    public float maximumMomentum = 5f;

    public float minimumHeight = 3;
    
    //https://www.patreon.com/posts/unity-3d-drag-22917454
    private Vector3 offset;
    private float   zCoord;
    private Vector3 momentum;
    private bool currentlyGrabbing = false;
    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - getMouseAsWorldPoint();
    }

    void Update()
    {
        
    }

    void OnMouseDown() {
        if (Gamestate.instance.currentState != GamestateEnum.DiceRoll)
            return;
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - getMouseAsWorldPoint();
        transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        currentlyGrabbing = true;
    }

    private void OnMouseUp() {
        if (Gamestate.instance.currentState != GamestateEnum.DiceRoll)
            return;
        if (currentlyGrabbing) {
            currentlyGrabbing = false;
            if (momentum.magnitude > maximumMomentum) {
                momentum = momentum.normalized * maximumMomentum;
            }
            body.velocity = momentum;
        }
    }

    void OnMouseDrag() {
        if (Gamestate.instance.currentState != GamestateEnum.DiceRoll)
            return;
        var worldPoint = getMouseAsWorldPoint();
        Vector3 oldpos = transform.position;
        transform.position =  worldPoint + offset;
        if (transform.position.y < minimumHeight)
            transform.position = new Vector3(transform.position.x, minimumHeight, transform.position.z);
        body.velocity = new Vector3();
        momentum = (transform.position - oldpos) * momentumFactor;
    }

    private Vector3 getMouseAsWorldPoint() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void OnDrawGizmos() {
        var pos = transform.position;
        pos.y = minimumHeight;
        Gizmos.DrawCube(pos, new Vector3(0.1f, 0.1f, 0.1f));
    }
}
