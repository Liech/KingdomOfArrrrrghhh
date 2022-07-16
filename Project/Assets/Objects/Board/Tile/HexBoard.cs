using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexBoard : MonoBehaviour
{
    public Tilemap assetTiles;

    public TileBase highlightTile;
    public Tilemap highlightTilemap;

    public Tilemap RangeIndicatorTilemap;
    public TileBase rangeTile;
    public TileBase notMyRangeTile;

    public Tilemap OwnershipTilemap;
    public TileBase ownerTile;

    public Tilemap HoverTilemap;
    public TileBase hoverTile;


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
