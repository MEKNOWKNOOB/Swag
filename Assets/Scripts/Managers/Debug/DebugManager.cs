using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DebugManager : NetworkEntity
{
    public static DebugManager Instance;

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

            SetDebugTile(Player.transform.position);
        }
        else
        {
            Player = FindObjectOfType<LocalPlayer>();

        }
    }

    public void SetDebugTile(Vector3 position)
    {
        if (DebugTilemap == null)
        {
            return;
        }



        Tile tile = DebugTiles["DebugOverlayYellow"];

        WorldTileMap worldTileMap = (WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"];
        Vector3Int v3int = worldTileMap.CellPos("Debug", position);

        DebugTilemap.SetTile(v3int, tile);

        // Vector3Int entityPos = worldTileMap.CellPos("Debug", position);
    }
}
