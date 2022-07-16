using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnit : MonoBehaviour
{
    public static SelectedUnit instance;
    bool clickThisFrame = false;
    public UnitInformation currentUnit;
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
            if (unit) {
                currentUnit = unit;
                UnitInfo.instance.setUnit(unit);
                Pathfinder.instance.setUnit(unit.gameObject);
                Pathfinder.instance.showReachable = true;
            }
            else {
                currentUnit = null;
                UnitInfo.instance.setUnit(null);
                Pathfinder.instance.showReachable = false;
            }
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Fire2")) {
                setSelectedUnit(null);
        }
    }
}
