using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitiativeBoard : MonoBehaviour
{
    public GameObject uiElement;
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
        order = order.ToArray().ToList();
        List<UnitInformation> shownOrder = new List<UnitInformation>();
        for(int i = 0;i < order.Count; i++) {
            int index = (order.IndexOf(Gamestate.instance.currentUnit) + i) % order.Count;
            shownOrder.Add(order[index]);
        }
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var o in shownOrder) {
            var obj = Instantiate(uiElement, transform);
            obj.GetComponent<InitiativeOrderElement>().image.sprite = o.portrait;
            obj.GetComponent<InitiativeOrderElement>().target = o.gameObject;
            obj.GetComponent<InitiativeOrderElement>().name.GetComponent<TMPro.TextMeshProUGUI>().text = o.name;
        }
    }
}
