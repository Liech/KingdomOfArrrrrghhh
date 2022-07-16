using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpponentAI : MonoBehaviour {
    public int diceResult = 6;
    public int sightDistance = 20;


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

        var unit = Gamestate.instance.currentUnit;
        var inSight = Pathfinder.instance.getUnits(unit.gameObject, 20);
        var inReach = Pathfinder.instance.getReachableTiles(unit.gameObject, unit.TravelDistance);

        UnitInformation best = null;
        float bestDistance = float.MaxValue;
        foreach(var x in inSight) {
            float currentDistance = (x.transform.position - unit.transform.position).magnitude;
            if(currentDistance < bestDistance) {
                bestDistance = currentDistance;
                best = x;
            }
        }

        if (best == null) {
            var element = inReach.ElementAt(Random.Range(0,inReach.Count));
            Gamestate.instance.moveUnitTo(element);
        }


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