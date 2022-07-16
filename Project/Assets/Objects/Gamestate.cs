using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum GamestateEnum {
    UnitMovement, UnitAttack, DiceRoll
}
public class Gamestate : MonoBehaviour
{
    public static Gamestate instance;
    
    public List<UnitInformation> initiativeOrder = new List<UnitInformation>();

    public GamestateEnum currentState = GamestateEnum.UnitMovement;
    public UnitInformation currentUnit;
    public GameObject CurrentUnitIndicatior = null;


    private void Awake() {
        instance = this;
    }

    
    void Start()
    {
        
    }

    void Update()
    {
        if (initiativeOrder.Count == 0)
            return;

        if (currentUnit == null && initiativeOrder.Count > 0) {
            initiativeOrder.OrderBy(x => x.Initiative);
            currentUnit = initiativeOrder[0];
        }

        if (CurrentUnitIndicatior)
          CurrentUnitIndicatior.transform.position = currentUnit.transform.position + new Vector3(0, +3, 0);

        if (currentState == GamestateEnum.UnitMovement) {
            if (currentUnit.GetComponent<FactionMember>().FactionID == 0) {

            }
            else {

            }
        }
        
    }

    public void moveUnitTo(Vector3Int tile) {
        if (currentUnitCanBeMoved()) {
            HexBoard board = HexBoard.instance();
            if (Pathfinder.instance.getReachableTiles(currentUnit.gameObject, currentUnit.TravelDistance).Contains(tile)) {
                Vector3 pos = board.GetComponent<Grid>().CellToWorld(tile) + new Vector3(0, 0.1f, 0);
                currentUnit.transform.position = pos;

                var units = Pathfinder.instance.getUnits(currentUnit.gameObject, currentUnit.attackRange);
                if (units.Count != 0) {
                    currentState = GamestateEnum.UnitAttack;
                    DoNotAttackButton.instance.transform.GetChild(0).gameObject.SetActive(true);
                }
                else {
                    SelectedUnit.instance.setSelectedUnit(null);
                    setNextUnitToCurrent();
                }

            }
        }
    }

    public void attackUnit(UnitInformation attackedUnit) {
        if (attackedUnit == null) {
            currentState = GamestateEnum.UnitMovement;
            SelectedUnit.instance.setSelectedUnit(null);
            setNextUnitToCurrent();
        }
        DoNotAttackButton.instance.transform.GetChild(0).gameObject.SetActive(false);
    }

    public bool currentUnitCanBeMoved() {
        return SelectedUnit.instance.currentUnit != null && SelectedUnit.instance.currentUnit == currentUnit && currentUnit.GetComponent<FactionMember>().FactionID == 0 && currentState == GamestateEnum.UnitMovement;
    }
    void setNextUnitToCurrent() {
        initiativeOrder.OrderBy(x => x.Initiative);
        int nextIndex = (initiativeOrder.IndexOf(currentUnit) + 1) % initiativeOrder.Count;
        currentUnit = initiativeOrder[nextIndex];
    }

    public void addUnit(UnitInformation info) {
        initiativeOrder.Add(info);
    }
    public void removeUnit(UnitInformation info) {
        initiativeOrder.Remove(info);
    }

}
