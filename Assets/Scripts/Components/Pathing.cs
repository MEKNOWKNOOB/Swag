using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Pathing : NetworkComponent
{
    public static int DistanceSquared(Vector3Int a, Vector3Int b)
    {
        int dx = a.x - b.x;
        int dy = a.y - b.y;
        int dz = a.z - b.z;
        return dx * dx + dy * dy + dz * dz;
    }

    public static Vector3Int[] Directions =
    {
        new(0,1,0), // up left
        new(1,0,0), // up right
        new(0,-1,0), // down right
        new(-1,0,0), // down left

        new(1,1,0), // up
        new(1, -1,0), // right
        new(-1,-1,0), // down
        new(-1,1,0) // left
    };

    void Awake()
    {
        RefName = "Pathing";
    }

    void Start()
    {

    }

    /// <summary>
    /// BFS with priority queue path search
    /// </summary>
    /// <param name="targetPos">Position of target</param>
    /// <param name="targetRadiusSquared">Determines if pathing can return early when reaching goal + radius</param>
    /// <param name="maxIterations">Max search levels for BFS</param>
    /// <returns>World Move Direction</returns>
    public Vector2 PathToTarget(Vector3 targetPos, float targetRadiusSquared, int maxIterations) /*, Dictionary<string, float> customHeuristics)*/
    {
        WorldTileMap worldTileMap = (WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"];

        // Avaiable Tiles
        PriorityQueue<Vector3Int> aTiles = new();
        // Reversed Constructed Paths 
        Dictionary<Vector3Int, (Vector3Int prevCell, Vector3Int initDir)> cPath = new();

        Vector3Int entityPos = worldTileMap.CellPos("Ground", Entity.transform.position);
        Vector3Int goalPos = worldTileMap.CellPos("Ground", targetPos);

        Vector3Int closestTilePos = entityPos;
        aTiles.Enqueue(entityPos, 0);
        cPath[entityPos] = (entityPos, Vector3Int.zero);

        // BFS with priority queue
        for (float currScore = 1; aTiles.Count > 0 && currScore < maxIterations; currScore++)
        {
            Vector3Int currTilePos = aTiles.Dequeue();
            if (currTilePos == goalPos)
            {
                if (DebugManager.Instance != null)
                {
                    DebugManager.Instance.SetDebugTile(worldTileMap.CellWorldPos("Ground", currTilePos), DebugManager.DebugLayers.Pathing, DebugManager.DebugTileTypes.Red);
                }
                return MoveDirection(cPath[currTilePos].initDir);
            }
            if (DistanceSquared(goalPos, closestTilePos) < targetRadiusSquared)
            {
                if (DebugManager.Instance != null)
                {
                    DebugManager.Instance.SetDebugTile(worldTileMap.CellWorldPos("Ground", closestTilePos), DebugManager.DebugLayers.Pathing, DebugManager.DebugTileTypes.Blue);
                }
                return MoveDirection(cPath[closestTilePos].initDir);
            }

            Vector3Int nextTilePos;
            float nextTileCost;
            List<(bool isValid, float cost)> neighborTiles = new();

            // loop through the 4 adjacent tile driections, update them in neighbortiles
            for (int i = 0; i < 4; i++)
            {
                nextTilePos = currTilePos + Directions[i];
                nextTileCost = worldTileMap.GetTileDataCost("Ground", nextTilePos);

                // if next tile has already been checked
                // if next tile is invalid
                if (cPath.ContainsKey(nextTilePos) || nextTileCost > 999)
                {
                    neighborTiles.Add((false, 999));
                    continue;
                }

                // push next tile to queue
                aTiles.Enqueue(nextTilePos, Mathf.Pow(nextTileCost, 2) + Mathf.Pow(currScore, 2) + DistanceSquared(goalPos, nextTilePos));
                if (cPath[currTilePos].initDir == Vector3Int.zero)
                {
                    cPath[nextTilePos] = (currTilePos, Directions[i]);
                }
                else
                {
                    cPath[nextTilePos] = (currTilePos, cPath[currTilePos].initDir);
                }

                if (DistanceSquared(goalPos, closestTilePos) > DistanceSquared(goalPos, nextTilePos))
                {
                    closestTilePos = nextTilePos;
                }

                // update neighborTiles
                neighborTiles.Add((true, nextTileCost));
            }

            // loop through the 4 cardinal tile driections
            // add check for adjacent tile costs
            for (int i = 0; i < 4; i++)
            {
                // if neighbor tiles are valid
                if (!(neighborTiles[i].isValid && neighborTiles[(i + 1) % 4].isValid))
                {
                    continue;
                }

                nextTilePos = currTilePos + Directions[i + 4];
                nextTileCost = worldTileMap.GetTileDataCost("Ground", nextTilePos);

                // if next tile has already been checked
                // if next tile is invalid
                if (cPath.ContainsKey(nextTilePos) || nextTileCost > 999)
                {
                    continue;
                }

                // push next tile to queue
                aTiles.Enqueue(nextTilePos, Mathf.Pow(nextTileCost, 2) + Mathf.Pow(currScore + 0.5f, 2) + DistanceSquared(goalPos, nextTilePos));
                if (cPath[currTilePos].initDir == Vector3Int.zero)
                {
                    cPath[nextTilePos] = (currTilePos, Directions[i + 4]);
                }
                else
                {
                    cPath[nextTilePos] = (currTilePos, cPath[currTilePos].initDir);
                }

                if (DistanceSquared(goalPos, closestTilePos) > DistanceSquared(goalPos, nextTilePos))
                {
                    closestTilePos = nextTilePos;
                }
            }
        }
        return MoveDirection(cPath[closestTilePos].initDir); // no path found
    }

    /// <summary>
    /// Converts CellPosDirection into WorldDirection
    /// </summary>
    private Vector2 MoveDirection(Vector3Int initDir)
    {
        if (Directions[0] == initDir)
        {
            return new Vector2(-1, 1);
        }
        else if (Directions[1] == initDir)
        {
            return new Vector2(1, 1);
        }
        else if (Directions[2] == initDir)
        {
            return new Vector2(1, -1);
        }
        else if (Directions[3] == initDir)
        {
            return new Vector2(-1, -1);
        }
        else if (Directions[4] == initDir)
        {
            return new Vector2(0, 1);
        }
        else if (Directions[5] == initDir)
        {
            return new Vector2(1, 0);
        }
        else if (Directions[6] == initDir)
        {
            return new Vector2(0, -1);
        }
        else if (Directions[7] == initDir)
        {
            return new Vector2(-1, 0);
        }
        return Vector2.zero;
    }
}
