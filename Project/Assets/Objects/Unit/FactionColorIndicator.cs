using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FactionMember))]
public class FactionColorIndicator : MonoBehaviour
{
    public MeshRenderer colorTarget = null;
    void Start()
    {
        if (!colorTarget)
            return;
        colorTarget.material.color = GetComponent<FactionMember>().getFaction().Color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
