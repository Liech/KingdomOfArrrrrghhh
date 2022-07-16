using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeOrderElement : MonoBehaviour {
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Image backgroundimage;
    public GameObject target;
    public GameObject name;
   
    public void selectTarget() {
        SelectedUnit.instance.setSelectedUnit(target.GetComponent<UnitInformation>());
    }

    public void Update() {
        if (backgroundimage && target) {
            backgroundimage.color = target.GetComponent<FactionMember>().getFaction().Color;
        }
    }
}
