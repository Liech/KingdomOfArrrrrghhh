using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public enum GamestateEnum {
    UnitMovement, UnitAttack, DiceRoll
}
public class Gamestate : MonoBehaviour
{
    public static Gamestate instance;

    public string nextScene = "Victory";
    public List<UnitInformation> initiativeOrder = new List<UnitInformation>();

    public GamestateEnum currentState = GamestateEnum.UnitMovement;
    public UnitInformation currentUnit;
    public UnitInformation attackingUnit;
    public UnitInformation attackedUnit;
    public HashSet<UnitInformation> attackable = new HashSet<UnitInformation>();
    public GameObject CurrentUnitIndicatior = null;
    public GameObject attackIndicatorObject;

    public UnitInformation startUnit;

    private List<AttackHere> attackIndicators = new List<AttackHere>();
    private void Awake() {
        instance = this;
    }

    
    void Start() {
        instance = this;
        currentUnit = startUnit;
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
          CurrentUnitIndicatior.transform.position = currentUnit.transform.position + new Vector3(0, +2.4f, 0);

        if (currentState == GamestateEnum.UnitAttack && attackingUnit) {
            var selected = SelectedUnit.instance.getSelectedUnit();
            bool selectedExists = selected != null;
            bool attackingExists = attackingUnit != null;
            bool factionDifferent = selected && attackingUnit && selected.GetComponent<FactionMember>().FactionID != attackingUnit.GetComponent<FactionMember>().FactionID;
            bool isAttackable = attackable.Contains(selected);
            if (selectedExists && attackingExists && factionDifferent && isAttackable) {
                AttackSelectedButton.instance.transform.GetChild(0).gameObject.SetActive(true);
            }
            else {
                AttackSelectedButton.instance.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else if (!attackingUnit && currentState == GamestateEnum.UnitAttack) {
            currentState = GamestateEnum.UnitMovement;
            SelectedUnit.instance.setSelectedUnit(null);
            setNextUnitToCurrent();
        }
        else 
            AttackSelectedButton.instance.transform.GetChild(0).gameObject.SetActive(false);

        victoryCheck();
    }

    public void moveUnitTo(Vector3Int tile) {
        if (currentState != GamestateEnum.UnitMovement)
            return;
        if (currentUnitCanBeMoved()) {
            HexBoard board = HexBoard.instance();
            if (Pathfinder.instance.getReachableTiles(currentUnit.gameObject, currentUnit.TravelDistance).Contains(tile)) {
                Vector3 pos = board.GetComponent<Grid>().CellToWorld(tile);
                pos.y = board.transform.position.y;
                currentUnit.transform.position = pos;
                //currentUnit.transform.rotation = Quaternion.identity;
                currentUnit.blockRotation();

                var units = Pathfinder.instance.getUnits(currentUnit.gameObject, currentUnit.attackRange);
                if (units.Count != 0) {
                    currentState = GamestateEnum.UnitAttack;
                    DoNotAttackButton.instance.transform.GetChild(0).gameObject.SetActive(true);
                    attackable.Clear();
                    foreach (var unit in units) {
                        if (unit.GetComponent<FactionMember>().FactionID == currentUnit.GetComponent<FactionMember>().FactionID)
                            continue;
                        var indicator = Instantiate(attackIndicatorObject, unit.gameObject.transform);
                        indicator.transform.localPosition = new Vector3(0, 3, 0);
                        indicator.GetComponent<AttackHere>().target = unit;
                        attackIndicators.Add(indicator.GetComponent<AttackHere>());
                        attackable.Add(unit);
                    }
                    attackingUnit = currentUnit;
                }
                else {
                    SelectedUnit.instance.setSelectedUnit(null);
                    setNextUnitToCurrent();
                }

            }
        }
    }

    public void diceRoll(int number) {
        if (currentState != GamestateEnum.DiceRoll)
            return;
        if (attackedUnit) {
            attackedUnit.currentLife -= number;
        }
        currentState = GamestateEnum.UnitMovement;
        SelectedUnit.instance.setSelectedUnit(null);
        setNextUnitToCurrent();
    }

    public void attackUnit(UnitInformation attackedUnit) {
        if (attackingUnit == attackedUnit)
            return;
        if (attackedUnit == null) {
            currentState = GamestateEnum.UnitMovement;
            SelectedUnit.instance.setSelectedUnit(null);
            setNextUnitToCurrent();
        }
        else {
            currentState = GamestateEnum.DiceRoll;
            this.attackedUnit = SelectedUnit.instance.currentUnit;
        }

        foreach (var a in attackIndicators)
            Destroy(a.gameObject);
        attackIndicators.Clear();
        DoNotAttackButton.instance.transform.GetChild(0).gameObject.SetActive(false);
    }

    public bool currentUnitCanBeMoved() {
        return SelectedUnit.instance.currentUnit != null && SelectedUnit.instance.currentUnit == currentUnit && currentState == GamestateEnum.UnitMovement;
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



    bool victoryStarted = false;
    void victoryCheck() {
        bool opponentFound = false;
        bool ownUnitFound = false;
        foreach (var x in initiativeOrder) {
            if (x.GetComponent<FactionMember>().FactionID == 1)
                opponentFound = true;
            if (x.GetComponent<FactionMember>().FactionID == 0)
                ownUnitFound = true;
        }

        if (!opponentFound && !victoryStarted) {
            victoryStarted = true;
            StartCoroutine(switchToNextScene());
        }
        if (!ownUnitFound && !victoryStarted) {
            victoryStarted = true;
            StartCoroutine(switchToLost());
        }
    }

    IEnumerator switchToNextScene() {
        yield return new WaitForSeconds(3f); //thinking
        instance = null;
        SceneManager.LoadScene(nextScene);
        LoadScene.currentScene = nextScene;
    }
    IEnumerator switchToLost() {
        yield return new WaitForSeconds(3f); //thinking
        instance = null;
        SceneManager.LoadScene("Lost");
    }
}
