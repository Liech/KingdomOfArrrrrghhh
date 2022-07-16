using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeOrderElement : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public GameObject target;
    public GameObject name;
   
    public void selectTarget() {
        SelectedUnit.instance.setSelectedUnit(target.GetComponent<UnitInformation>());
    }
}
