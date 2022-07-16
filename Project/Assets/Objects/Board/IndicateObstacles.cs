using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicateObstacles : MonoBehaviour {
    public float sphereRadius = 0.4f;
    int walkingDistance = 4;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(showObstacles());


    }

    public bool isPassable(Vector3Int pos) {
        RaycastHit hit;
        HexBoard board = HexBoard.instance();
        Vector3 p1 = board.GetComponent<Grid>().CellToWorld(pos) + new Vector3(0, sphereRadius + 0.2f, 0);

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.OverlapSphere(p1, sphereRadius).Length > 0) {
            return false;
        }
        return true;
    }
    IEnumerator showObstacles() {
        while (true) {
            HexBoard board = HexBoard.instance();
            Vector3Int cellID = board.GetComponent<Grid>().WorldToCell(transform.position);
            cellID.z = 0;
            for (int x = -walkingDistance; x < walkingDistance; x++)
                for (int y = -walkingDistance; y < walkingDistance; y++) {
                    if (Mathf.Abs(x) + Mathf.Abs(y) > walkingDistance)
                        continue;
                    Vector3Int currentCell = cellID + new Vector3Int(x, y, 0);
                    if (isPassable(currentCell))
                        board.RangeIndicatorTilemap.SetTile(currentCell, null);
                    else
                        board.RangeIndicatorTilemap.SetTile(currentCell, board.rangeTile);
                }
            yield return new WaitForSeconds(0.1f);
        }
    }

    //private void OnDrawGizmos() {
    //    HexBoard board = HexBoard.instance();
    //    Vector3Int cellID = board.GetComponent<Grid>().WorldToCell(transform.position);
    //    cellID.z = 0;
    //    for (int x = -5; x < 5; x++)
    //        for (int y = -5; y < 5; y++) {
    //
    //            Vector3Int currentCell = cellID + new Vector3Int(x, y, 0);
    //            Vector3 p1 = board.GetComponent<Grid>().CellToWorld(currentCell) + new Vector3(0, sphereRadius + 0.2f, 0);
    //            Gizmos.DrawSphere(p1, sphereRadius);
    //        }
    //}
}
