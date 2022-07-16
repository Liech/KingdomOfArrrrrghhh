using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInformation : MonoBehaviour
{
    public int TravelDistance = 3;
    public string Description = "Loyal Servant. Serving really loyal!";
    public float Initiative = 1;
    public int maxLife = 4;
    public int currentLife = 4;
    public Sprite portrait;


    public void Start() {
        Gamestate.instance.addUnit(this);
    }

    public void OnDestroy() {
        Gamestate.instance.removeUnit(this);
    }
}
