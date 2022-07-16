using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTileManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HexBoard board = HexBoard.instance();
        board.HoverTilemap.ClearAllTiles();
        Plane p = new Plane(new Vector3(0, 1, 0), board.HoverTilemap.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        bool doesHit = p.Raycast(ray, out enter);
        if (doesHit) {
            Vector3 pos = ray.GetPoint(enter);
            Vector3Int cell = board.GetComponent<Grid>().WorldToCell(pos);
            cell.z = 0;
            board.HoverTilemap.SetTile(cell, board.hoverTile);

            if (Gamestate.instance.currentUnit) {
                bool yourTurn = Gamestate.instance.currentUnit.GetComponent<FactionMember>().FactionID == 0;
                if (Input.GetButtonDown("Fire1") && yourTurn) {
                    Gamestate.instance.moveUnitTo(cell);
                }
            }
        }

        
    }
}
