using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FactionMember : MonoBehaviour
{
    public int FactionID;

    public Faction getFaction() {
        return Faction.factionMap[FactionID];
    }
}
