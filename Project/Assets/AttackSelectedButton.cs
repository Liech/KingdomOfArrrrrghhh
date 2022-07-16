using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSelectedButton : MonoBehaviour
{
    public static AttackSelectedButton instance;


    public void Awake() {
        instance = this;
    }

    public void attackSelectedPress() {
        Gamestate.instance.attackUnit(SelectedUnit.instance.currentUnit);
    }
}
