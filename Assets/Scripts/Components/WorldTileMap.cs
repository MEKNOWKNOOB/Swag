using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTileMap : NetworkComponent
{
    [SerializeField] private SerializableDictionary<string, Tilemap> tileMapLayers;

    [SerializeField] public Dictionary<string, Tilemap> TileMapLayers;
    [SerializeField] public Dictionary<string, Dictionary<Vector3Int, TileData>> MapLayerData;



    void Awake()
    {
        RefName = "WorldTileMap";
        TileMapLayers = tileMapLayers.ToDictionary();

        // Assign TileData
        MapLayerData = new Dictionary<string, Dictionary<Vector3Int, TileData>>();
        foreach (var layer in TileMapLayers)
        {
            MapLayerData[layer.Key] = new Dictionary<Vector3Int, TileData>();
            foreach (var pos in layer.Value.cellBounds.allPositionsWithin)
            {
                CustomTile tile = layer.Value.GetTile<CustomTile>(pos);
                if (tile != null)
                {
                    MapLayerData[layer.Key][pos] = new TileData(tile.TileData);
                    print(pos);
                }
            }
        }
    }

    void Start()
    {

    }



    // Helper
    private Vector3Int CellPos(string layer, Vector3 pos)
    {
        return TileMapLayers[layer].WorldToCell(pos);
    }




    // Fields
    public bool IsTileValid(string layer, Vector3 pos)
    {
        if (!MapLayerData[layer].ContainsKey(CellPos(layer, pos)))
        {
            print("Not on valid Tile");
            return false;
        }
        return true;
    }

    public TileData GetTileData(string layer, Vector3 pos)
    {
        if(!IsTileValid(layer, pos))
        {
            return null;
        }
        return MapLayerData[layer][CellPos(layer, pos)];
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
