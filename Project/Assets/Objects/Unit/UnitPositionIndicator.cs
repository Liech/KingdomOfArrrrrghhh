using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitPositionIndicator : MonoBehaviour
{
    HexBoard board;

    // Start is called before the first frame update
    void Start()
    {
        board = HexBoard.instance();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int cellID = board.GetComponent<Grid>().WorldToCell(transform.position);
        //cellID.z = 0;
        var cell = board.highlightTilemap.GetTile(cellID);        
        board.highlightTilemap.SetTile(cellID, board.highlightTile);
    }
}
