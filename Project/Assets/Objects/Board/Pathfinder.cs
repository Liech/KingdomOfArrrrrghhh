using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour {

    public static Pathfinder instance;
    public float sphereRadius = 0.4f;
    public int walkingDistance = 3;
    public Vector3Int possibleDir = new Vector3Int(1,0,0);
    public bool showReachable = false;

    private GameObject currentUnit;

    private void Awake() {
        instance = this;
    }
    void Start() {
        StartCoroutine(showPath());
        currentUnit = gameObject;
    }

    public void setUnit(GameObject obj) {
        currentUnit = obj;
        if (obj.GetComponent<UnitInformation>())
            walkingDistance = obj.GetComponent<UnitInformation>().TravelDistance;
    }

    //from can be used later for control zones, is not used yet
    public bool isPassable(Vector3Int to, Vector3Int from) {
        RaycastHit hit;
        HexBoard board = HexBoard.instance();
        Vector3 p1 = board.GetComponent<Grid>().CellToWorld(to) + new Vector3(0, sphereRadius + 0.2f, 0);

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.OverlapSphere(p1, sphereRadius).Length > 0) {
            return false;
        }
        return true;
    }

    IEnumerator showPath() {
        while (true) {
            setPathTiles();
            yield return new WaitForSeconds(0.1f);
        }
    }

    List<Vector3Int> possibleDirections(Vector3Int pos) {
        return new List<Vector3Int>{
            new Vector3Int(1,0,0),
            new Vector3Int(-1,0,0),
            new Vector3Int(((Mathf.Abs(pos.y%2)==0)?-1:0),1,0),
            new Vector3Int(((Mathf.Abs(pos.y%2)==0)?-1:0),-1,0),
            new Vector3Int(((Mathf.Abs(pos.y%2)==1)?1:0),1,0),
            new Vector3Int(((Mathf.Abs(pos.y%2)==1)?1:0),-1,0),            
        };
    }

    class todoItem {
        public Vector3Int pos;
        public Vector3Int from;
        public int stepsTaken = 0;
    }

    public HashSet<Vector3Int> getReachableTiles(GameObject unit, int walkingSpeed) {
        HexBoard board = HexBoard.instance();
        Vector3Int cellID = board.GetComponent<Grid>().WorldToCell(unit.transform.position);
        cellID.z = 0;

        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
        HashSet<Vector3Int> reachable = new HashSet<Vector3Int>();

        List<todoItem> todo = new List<todoItem>();
        todoItem first = new todoItem();
        first.pos = cellID;
        first.stepsTaken = 0;
        first.from = cellID;
        todo.Add(first);

        //cheap dijkstra
        while (todo.Count > 0) {
            todoItem current = todo[0];
            todo.RemoveAt(0);
            reachable.Add(current.pos);
            foreach (var dir in possibleDirections(current.pos)) {
                Vector3Int step = current.pos + dir;

                if (visited.Contains(step))
                    continue;
                visited.Add(step);

                if (!isPassable(step, current.pos))
                    continue;
                if (current.stepsTaken >= walkingSpeed)
                    continue;

                todoItem newStep = new todoItem();
                newStep.pos = step;
                newStep.from = current.pos;
                newStep.stepsTaken = current.stepsTaken + 1;
                todo.Add(newStep);
            }

            todo.OrderBy(x => x.stepsTaken);
        }
        return reachable;
    }

    void setPathTiles() {
        HexBoard board = HexBoard.instance();
        if (showReachable) {

            Vector3Int cellID = board.GetComponent<Grid>().WorldToCell(currentUnit.transform.position);
            cellID.z = 0;
            var reachable = getReachableTiles(currentUnit, walkingDistance);

            for (int x = -walkingDistance - 3; x < walkingDistance + 3; x++)
                for (int y = -walkingDistance - 3; y < walkingDistance + 3; y++)
                    board.RangeIndicatorTilemap.SetTile(cellID + new Vector3Int(x, y, 0), null);

            foreach (var r in reachable)
                board.RangeIndicatorTilemap.SetTile(r, board.rangeTile);
        }
        else
            board.RangeIndicatorTilemap.ClearAllTiles();
    }   
}
