using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FactionMember))]
public class FactionColorIndicator : MonoBehaviour {
    public MeshRenderer colorTarget = null;
    public SkinnedMeshRenderer colorTarget2 = null;
    void Start() {
        if (colorTarget)
            colorTarget.material.color = GetComponent<FactionMember>().getFaction().Color;
        if (colorTarget2)
            colorTarget2.material.color = GetComponent<FactionMember>().getFaction().Color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
