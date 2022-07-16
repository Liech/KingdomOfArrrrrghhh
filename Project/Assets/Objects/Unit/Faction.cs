using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    public static Dictionary<int, Faction> factionMap = new Dictionary<int, Faction>();

    public int FactionID = 0;
    public Color Color = Color.red;

    public void Awake() {
        factionMap[FactionID] = this;
    }

}
