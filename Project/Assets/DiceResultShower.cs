using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceResultShower : MonoBehaviour
{

    public static DiceResultShower instance;

    private void Awake() {
        instance = this;
    }
    
    public void showNumber(int diceResult) {
        for(int i= 0;i < 6; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(diceResult - 1).gameObject.SetActive(true);
        StartCoroutine(hideDiceResults());
    }

    IEnumerator hideDiceResults() {
        yield return new WaitForSeconds(3f); //thinking
        for (int i = 0; i < 6; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
