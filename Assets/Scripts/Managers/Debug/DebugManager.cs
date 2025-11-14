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
    [SerializeField] protected Dictionary<(int, DebugLayers), List<Vector3Int>> DebugTileGroups;

    [SerializeField] protected Tilemap DebugTilemap;
    [SerializeField] protected LocalPlayer Player;

    [SerializeField] protected bool debugEnabled = true;
    private bool debugDown = false;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        DebugTiles = debugTiles.ToDictionary();
        DebugTileGroups = new Dictionary<(int, DebugLayers), List<Vector3Int>>();
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (InputManager.Instance != null)
        {
            if (!debugDown && InputManager.Instance.DebugBool)
            {
                debugDown = true;
                ToggleDebugEnabled();
            }
            else if (debugDown && !InputManager.Instance.DebugBool)
            {
                debugDown = false;
            }
        }

        if (!debugEnabled)
        {
            ClearDebug();
        }
    }

    public void EnableDebug()
    {
        debugEnabled = true;
    }

    public void DisableDebug()
    {
        debugEnabled = false;

        ClearDebug();
    }

    protected void ClearDebug()
    {
        ClearAllDebugTiles();
    }

    protected void ClearAllDebugTiles()
    {
        List<(int id, DebugLayers layerType)> keys = new List<(int id, DebugLayers layerType)>(DebugTileGroups.Keys);

        // Clean up all of the debugs
        foreach ((int id, DebugLayers layerType) in keys)
        {
            ClearDebugTiles(id, layerType);
        }
    }

    public void ToggleDebugEnabled()
    {
        if (debugEnabled)
        {
            DisableDebug();
        }
        else
        {
            EnableDebug();
        }
    }

    public bool GetDebugEnabled()
    {
        return debugEnabled;
    }

    public void GenerateDebugTileGroup(int id, DebugLayers layerType)
    {
        List<Vector3Int> tileGroup;
        if (DebugTileGroups.TryGetValue((id, layerType), out tileGroup)) { return; }

        tileGroup = new List<Vector3Int>();

        DebugTileGroups[(id, layerType)] = tileGroup;
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

    public void ClearDebugTiles(int id, DebugLayers layerType)
    {
        List<Vector3Int> tileGroup;
        if (!DebugTileGroups.TryGetValue((id, layerType), out tileGroup)) { return; }

        string layer = GetLayerByEnum(layerType);

        Tilemap tilemap;
        WorldTileMap worldTileMap = (WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"];
        bool success = worldTileMap.TileMapLayers.TryGetValue(layer, out tilemap);
        if (!success)
        {
            Debug.LogWarning(string.Format("DebugManager: Unable to get the {0} tilemap.", layer));
        }

        foreach (Vector3Int v3int in tileGroup)
        {
            tilemap.SetTile(v3int, null);
        }

        tileGroup.Clear();

        DebugTileGroups.Remove((id, layerType));
    }

    public void SetDebugTiles(int id, List<Vector3> positions, DebugLayers layerType, DebugTileTypes tileType)
    {
        foreach (Vector3 pos in positions)
        {
            SetDebugTile(id, pos, layerType, tileType);
        }
    }

    public void SetDebugTiles(int id, List<Vector3Int> positions, DebugLayers layerType, DebugTileTypes tileType)
    {
        WorldTileMap worldTileMap = (WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"];
        string layer = GetLayerByEnum(layerType);

        foreach (Vector3Int cellPos in positions)
        {
            Vector3 pos = worldTileMap.CellWorldPos(layer, cellPos);
            SetDebugTile(id, pos, layerType, tileType);
        }
    }

    public void SetDebugTile(int id, Vector3 position, DebugLayers layerType, DebugTileTypes tileType)
    {
        if (!debugEnabled)
        {
            return;
        }

        List<Vector3Int> tileGroup;
        if (!DebugTileGroups.TryGetValue((id, layerType), out tileGroup))
        {
            GenerateDebugTileGroup(id, layerType);
            DebugTileGroups.TryGetValue((id, layerType), out tileGroup);
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

        tileGroup.Add(v3int);

        tilemap.SetTile(v3int, tile);

        // Vector3Int entityPos = worldTileMap.CellPos("Debug", position);
    }
}
