using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotAttackButton : MonoBehaviour
{
    public static DoNotAttackButton instance;
    private void Awake() {
        instance = this;
    }
    public void doNotAttack() {
        Gamestate.instance.attackUnit(null);
    }
}
