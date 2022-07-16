using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintLog : MonoBehaviour
{
    static HintLog instance;
    public void Awake() {
        instance = this;
    }

    public static void Log(string s) {
        instance.GetComponent<TMPro.TextMeshProUGUI>().SetText(s);
    }

    public void Update() {
        if (!Gamestate.instance.currentUnit)
            return;
        bool yourTurn = false;
        yourTurn = Gamestate.instance.currentUnit.GetComponent<FactionMember>().FactionID == 0;

        if (Gamestate.instance.currentState == GamestateEnum.UnitMovement) {
            if (yourTurn) {
                Log("Your Turn. Select one of your Units and Move");
            }
            else
                Log("The opponents turn");
        }
        if (Gamestate.instance.currentState == GamestateEnum.UnitAttack) {
            if (yourTurn) {
                Log("You can Attack. Chose a Unit with a sword over its head.");
            }
            else
                Log("Your opponent decides to attack");
        }
        if (Gamestate.instance.currentState == GamestateEnum.DiceRoll) {
            if (yourTurn) {
                Log("Roll the dice with your mouse. Reset it with space.");
            }
            else
                Log("Your oppenent Rolls the dice");
        }


    }
}
