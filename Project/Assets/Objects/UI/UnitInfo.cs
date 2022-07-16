using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitInfo : MonoBehaviour
{
    public static UnitInfo instance;
    public GameObject walkDistance;
    public GameObject description;
    public GameObject name;
    public GameObject life;
    public GameObject portrait;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void setUnit(UnitInformation unit) {
        if (unit == null) {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else {
            walkDistance.GetComponent<TextMeshProUGUI>().SetText(unit.TravelDistance.ToString());
            description.GetComponent<TextMeshProUGUI>().SetText(unit.Description);
            name.GetComponent<TextMeshProUGUI>().SetText(unit.gameObject.name);
            life.GetComponent<TextMeshProUGUI>().SetText(unit.currentLife.ToString() + "/" + unit.maxLife.ToString());
            transform.GetChild(0).gameObject.SetActive(true);
            portrait.GetComponent<UnityEngine.UI.Image>().sprite = unit.portrait;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
