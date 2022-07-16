using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour {
    public int diceResult = 6;


    bool actionActive = false;
    void Update() {
        bool aiTurn = Gamestate.instance.currentUnit.GetComponent<FactionMember>().FactionID == 1;
        if (aiTurn) {
            if (Gamestate.instance.currentState == GamestateEnum.DiceRoll && !actionActive) {
                actionActive = true;
                StartCoroutine(destructionAnimation());
            }
            else if (Gamestate.instance.currentState == GamestateEnum.UnitAttack) {
                actionActive = true;
                StartCoroutine(attacking());
            }
            else if (Gamestate.instance.currentState == GamestateEnum.UnitMovement) {

                actionActive = true;
                StartCoroutine(moving());
            }
        }
    }

    IEnumerator moving() {
        yield return new WaitForSeconds(1f); //thinking



        actionActive = false;
    }


    IEnumerator attacking() {
        yield return new WaitForSeconds(1f); //thinking

        if (Gamestate.instance.attackable.Count == 0)
            Gamestate.instance.attackUnit(null);
        else {
            UnitInformation attacktarget = null;
            foreach (var i in Gamestate.instance.attackable) {
                attacktarget = i;
                break;
            }
            Gamestate.instance.attackUnit(attacktarget);
        }
        actionActive = false;
    }

    IEnumerator destructionAnimation() {
        yield return new WaitForSeconds(1f); //thinking
        Gamestate.instance.diceRoll(diceResult);
        actionActive = false;
    }
}