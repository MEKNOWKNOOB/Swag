using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTileMap : NetworkComponent
{
    [SerializeField] private SerializableDictionary<string, Tilemap> tileMapLayers;
    [SerializeField] private Dictionary<string, Tilemap> TileMapLayers;



    void Awake()
    {
        RefName = "WorldTileMap";
    }

    void Start()
    {
        TileMapLayers = tileMapLayers.ToDictionary();
    }



    // Helper
    private Vector3Int GetTilePos(string layer, Vector3 pos)
    {
        return TileMapLayers[layer].WorldToCell(pos);
    }




    // Fields
    public bool IsTileValid(string layer, Vector3 pos)
    {
        if (TileMapLayers[layer].GetTile<CustomTile>(GetTilePos(layer, pos)) == null)
        {
            return false;
        }
        return true;
    }

    public CustomTile GetTileData(string layer, Vector3 pos)
    {
        return TileMapLayers[layer].GetTile<CustomTile>(GetTilePos(layer, pos));
    }

    public float GetTileDataWalkSpeed(string layer, Vector3 pos)
    {
        if (GetTileData(layer, pos) == null)
        {
            return 1;
        }
        return GetTileData(layer, pos).walkSpeed;
    }
}
