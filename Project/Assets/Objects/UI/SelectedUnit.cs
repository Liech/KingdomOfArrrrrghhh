using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnit : MonoBehaviour
{
    public static SelectedUnit instance;
    bool clickThisFrame = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    UnitInformation selected = null;
    public UnitInformation getSelectedUnit() {
        return selected;
    }

    public void setSelectedUnit(UnitInformation unit) {
        selected = unit;
        if (UnitInfo.instance) {
            UnitInfo.instance.setUnit(unit);
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Fire2")) {
            UnitInfo.instance.setUnit(null);
        }
    }
}
