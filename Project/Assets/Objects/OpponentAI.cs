using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpponentAI : MonoBehaviour {
    public int diceResult = 6;
    public int sightDistance = 20;
    public int factionID = 1;


    bool actionActive = false;
    void Update() {
        if (!Gamestate.instance.currentUnit)
            return;
        bool aiTurn = Gamestate.instance.currentUnit.GetComponent<FactionMember>().FactionID == 1;
        if (aiTurn) {
            if (Gamestate.instance.currentState == GamestateEnum.DiceRoll && !actionActive) {
                actionActive = true;
                StartCoroutine(diceRolling());
            }
            else if (Gamestate.instance.currentState == GamestateEnum.UnitAttack && !actionActive) {
                actionActive = true;
                StartCoroutine(attacking());
            }
            else if (Gamestate.instance.currentState == GamestateEnum.UnitMovement && !actionActive) {

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
        
        SelectedUnit.instance.currentUnit = Gamestate.instance.currentUnit;

        UnitInformation bestTargetUnit = null;
        float bestDistance = float.MaxValue;
        foreach(var x in inSight) {
            if (x.GetComponent<FactionMember>().FactionID == factionID)
                continue;
            float currentDistance = (x.transform.position - unit.transform.position).magnitude;
            if(currentDistance < bestDistance) {
                bestDistance = currentDistance;
                bestTargetUnit = x;
            }
        }

        if (bestTargetUnit == null) {
            var element = inReach.ElementAt(Random.Range(0, inReach.Count));
            Gamestate.instance.moveUnitTo(element);
        }
        else {
            var travelDistance = Pathfinder.instance.getReachableTilesWithDistance(bestTargetUnit.gameObject, 50);
            Dictionary<Vector3Int, Pathfinder.todoItem> distanceCalculator = new Dictionary<Vector3Int, Pathfinder.todoItem>();
            foreach (var x in travelDistance)
                distanceCalculator[x.pos] = x;

            int bestTravelDistance = int.MaxValue;
            Vector3Int bestTraveTarget = new Vector3Int();

            foreach (var r in inReach) {
                if (distanceCalculator.ContainsKey(r)) {
                    int currentDistance = distanceCalculator[r].stepsTaken;
                    if (currentDistance < bestTravelDistance) {
                        bestTravelDistance = currentDistance;
                        bestTraveTarget = r;
                    }
                }
            }



            if (bestTravelDistance == int.MaxValue) {
                HexBoard board = HexBoard.instance();
                Vector3Int targetCellID = board.GetComponent<Grid>().WorldToCell(bestTargetUnit.transform.position);
                targetCellID.z = 0;

                float bestDistanceToTarget = float.MaxValue;
                Vector3Int bestMoveTile = new Vector3Int();

                foreach (var x in inReach) {
                    float currentDistance = (targetCellID - x).magnitude;
                    if (currentDistance < bestDistanceToTarget) {
                        bestDistanceToTarget = currentDistance;
                        bestMoveTile = x;
                    }
                }

                Gamestate.instance.moveUnitTo(bestMoveTile);
            }
            else {

                Gamestate.instance.moveUnitTo(bestTraveTarget);
            }


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
            SelectedUnit.instance.currentUnit = attacktarget;
            Gamestate.instance.attackUnit(attacktarget);
        }
        actionActive = false;
    }

    IEnumerator diceRolling() {
        yield return new WaitForSeconds(1f); //thinking
        Gamestate.instance.diceRoll(Random.Range(3,7));
        actionActive = false;
    }
}