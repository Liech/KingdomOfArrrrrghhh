using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitiativeBoard : MonoBehaviour
{
    public GameObject uiElement;
    List<UnitInformation> currentOrder = null;
    void Start() {
        StartCoroutine(showInitiative());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator showInitiative() {
        while (true) {
            updateInitiative();
            yield return new WaitForSeconds(1f);
        }
    }


    void updateInitiative() {
        var order = Gamestate.instance.initiativeOrder;
        if (order.Count == 0 || Gamestate.instance.currentUnit == null) {

            foreach (Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }
            return;
        }
        order = order.ToArray().ToList();
        List<UnitInformation> shownOrder = new List<UnitInformation>();
        for(int i = 0;i < order.Count; i++) {
            int index = (order.IndexOf(Gamestate.instance.currentUnit) + i) % order.Count;
            if (index < 0 || index >= order.Count)
                index = 0;
            shownOrder.Add(order[index]);
        }

        bool same = true;

        if (currentOrder == null)
            same = false;
        else if (shownOrder.Count != currentOrder.Count)
            same = false;
        else {
            for(int i = 0;i < shownOrder.Count; i++) {
                if (shownOrder[i] != currentOrder[i])
                    same = false;
            }
        }
        

        if (!same) {
            currentOrder = shownOrder;
            foreach (Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }
            int count = 0;
            foreach (var o in shownOrder) {
                var obj = Instantiate(uiElement, transform);
                obj.GetComponent<InitiativeOrderElement>().image.sprite = o.portrait;
                obj.GetComponent<InitiativeOrderElement>().target = o.gameObject;
                obj.GetComponent<InitiativeOrderElement>().name.GetComponent<TMPro.TextMeshProUGUI>().text = o.name;
                count++;
                if (count > 5)
                    return;
            }
        }
    }
}
