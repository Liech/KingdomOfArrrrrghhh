using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexBoard : MonoBehaviour
{
    public TileBase highlightTile;
    public Tilemap highlightTilemap;

    private static HexBoard inst = null;
    public static HexBoard instance() {
        return inst;
    }
    void Awake()
    {
        inst = this;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
