using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInformation : MonoBehaviour
{
    public int TravelDistance = 3;
    public string Description = "Loyal Servant. Serving really loyal!";
    public float Initiative = 1;
    public int maxLife = 4;
    public int attackRange = 1;
    public int currentLife = 4;
    public Sprite portrait;

    public GameObject onDeathEffect;

    bool onDestruction = false;

    public void Start() {
        Gamestate.instance.addUnit(this);
    }

    public void Update() {
        if (currentLife <= 0 && !onDestruction) {
            onDestruction = true;
            StartCoroutine(destructionAnimation());
        }

        var upDir = transform.rotation * -new Vector3(0.0f, 1.0f, 0.0f);
        float upness = Vector3.Dot(upDir, new Vector3(0, 1, 0));
        Debug.Log(gameObject.name + ": " + upness.ToString());
    }

    public void OnDestroy() {
        Gamestate.instance.removeUnit(this);
    }
    IEnumerator destructionAnimation() {
        if (GetComponent<Rigidbody>()) {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 2, 0);
        }
        if (onDeathEffect)
            onDeathEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
