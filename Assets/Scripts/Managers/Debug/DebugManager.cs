using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DebugManager : NetworkEntity
{
    public static DebugManager Instance;
    public enum DebugTileTypes
    {
        Blue,
        Green,
        Red,
        Yellow
    }

    public enum DebugLayers
    {
        Pathing
    }

    [SerializeField] private SerializableDictionary<string, Tile> debugTiles;
    [SerializeField] protected Dictionary<string, Tile> DebugTiles;

    [SerializeField] protected Tilemap DebugTilemap;
    [SerializeField] protected LocalPlayer Player;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        DebugTiles = debugTiles.ToDictionary();
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Player != null)
        {
            if (debugTiles == null)
            {
                WorldTileMap worldTileMap = (WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"];
                bool success = worldTileMap.TileMapLayers.TryGetValue("Debug", out DebugTilemap);
                if (!success)
                {
                    Debug.LogWarning("DebugManager: Unable to get the Debug tilemap.");
                }

                
            }

            // SetDebugTile(Player.transform.position, DebugLayers.Pathing, DebugTileTypes.Blue);
        }
        else
        {
            Player = FindObjectOfType<LocalPlayer>();

        }
    }

    public Tile GetTileByEnum(DebugTileTypes tileType)
    {
        switch (tileType)
        {
            case DebugTileTypes.Blue:
                return DebugTiles["DebugOverlayBlue"];
            case DebugTileTypes.Green:
                return DebugTiles["DebugOverlayGreen"];
            case DebugTileTypes.Red:
                return DebugTiles["DebugOverlayRed"];
            case DebugTileTypes.Yellow:
                return DebugTiles["DebugOverlayYellow"];
            default:
                Debug.Log(string.Format("Unable to find debug tile of type: {0}, returning tile DebugOverlayBlue instead", tileType.ToString()));
                return DebugTiles["DebugOverlayBlue"];
        }
    }

    public string GetLayerByEnum(DebugLayers layerType)
    {
        switch (layerType)
        {
            case DebugLayers.Pathing:
                return "DebugPathing";
            default:
                Debug.Log(string.Format("Unable to find debug layer of type: {0}, returning layer DebugPathing instead", layerType.ToString()));
                return "DebugPathing";
        }
    }

    public void SetDebugTile(Vector3 position, DebugLayers layerType, DebugTileTypes tileType)
    {
        if (DebugTilemap == null)
        {
            return;
        }

        Tile tile = GetTileByEnum(tileType);
        string layer = GetLayerByEnum(layerType);

        // Tile tile = DebugTiles["DebugOverlayYellow"];

        Tilemap tilemap;
        WorldTileMap worldTileMap = (WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"];
        bool success = worldTileMap.TileMapLayers.TryGetValue(layer, out tilemap);
        if (!success)
        {
            Debug.LogWarning("DebugManager: Unable to get the Debug tilemap.");
        }

        //WorldTileMap worldTileMap = (WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"];
        Vector3Int v3int = worldTileMap.CellPos(layer, position);

        tilemap.SetTile(v3int, tile);

        // Vector3Int entityPos = worldTileMap.CellPos("Debug", position);
    }
}
