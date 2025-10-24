using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathing : NetworkComponent
{
    public static Vector3Int[] Directions =
    {
        new(1,0,0),
        new(-1,0,0),
        new(0,1,0),
        new(0,-1,0),

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

    public Vector2 PathToTarget(Vector3 targetPos, int targetRadius) /*, Dictionary<string, float> customHeuristics)*/
    {
        WorldTileMap worldTileMap = (WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"];

        // Avaiable Tiles
        PriorityQueue<Vector3Int> aTiles = new();
        // Reversed Constructed Paths 
        Dictionary<Vector3Int, Vector3Int> cPath = new();

        Vector3Int entityPos = worldTileMap.CellPos("Ground", Entity.transform.position);
        Vector3Int goalPos = worldTileMap.CellPos("Ground", targetPos);
        float currScore = 0;

        aTiles.Enqueue(entityPos, currScore);
        cPath[entityPos] = entityPos;

        // BFS with priority queue
        while (aTiles.Count > 0)
        {
            Vector3Int currTilePos = aTiles.Dequeue();
            if (currTilePos == goalPos)
            {
                return DeconstructPath(cPath, currTilePos, entityPos);
            }

            // update current level/distance
            currScore += worldTileMap.GetTileDataCost("Ground", currTilePos);
            // determine which cardinal tiles are available
            bool[] diaganols = { false, false, false, false };

            // loop through the 4 adjacent tile driections
            Vector3Int nextTilePos;
            float nextTileCost;
            for (int i = 0; i < 4; i++)
            {
                nextTilePos = currTilePos + Directions[i];
                // if next tile has already been checked
                if (cPath.ContainsKey(nextTilePos))
                {
                    continue;
                }
                nextTileCost = worldTileMap.GetTileDataCost("Ground", nextTilePos);
                // if next tile is invalid
                if (nextTileCost > 999)
                {
                    continue;
                }
                // push next tile to queue
                aTiles.Enqueue(nextTilePos, nextTileCost + currScore);
                cPath[nextTilePos] = currTilePos;
                diaganols[i] = true;
            }

            // diagnols
            // should fix it not check neighbor scores
            nextTilePos = currTilePos + Directions[4];
            nextTileCost = worldTileMap.GetTileDataCost("Ground", nextTilePos);
            if (!cPath.ContainsKey(nextTilePos) && nextTileCost <= 999 && diaganols[0] && diaganols[2])
            {
                aTiles.Enqueue(nextTilePos, nextTileCost + currScore + 0.5f);
                cPath[nextTilePos] = currTilePos;
            }
            
            nextTilePos = currTilePos + Directions[5];
            nextTileCost = worldTileMap.GetTileDataCost("Ground", nextTilePos);
            if (!cPath.ContainsKey(nextTilePos) && nextTileCost <= 999 && diaganols[0] && diaganols[3])
            {
                aTiles.Enqueue(nextTilePos, nextTileCost + currScore + 0.5f);
                cPath[nextTilePos] = currTilePos;
            }
            
            nextTilePos = currTilePos + Directions[6];
            nextTileCost = worldTileMap.GetTileDataCost("Ground", nextTilePos);
            if (!cPath.ContainsKey(nextTilePos) && nextTileCost <= 999 && diaganols[1] && diaganols[3])
            {
                aTiles.Enqueue(nextTilePos, nextTileCost + currScore + 0.5f);
                cPath[nextTilePos] = currTilePos;
            }
            
            nextTilePos = currTilePos + Directions[7];
            nextTileCost = worldTileMap.GetTileDataCost("Ground", nextTilePos);
            if(!cPath.ContainsKey(nextTilePos) && nextTileCost <= 999 && diaganols[1] && diaganols[2])
            {
                aTiles.Enqueue(nextTilePos, nextTileCost + currScore + 0.5f);
                cPath[nextTilePos] = currTilePos;
            }



        }

        return Vector2.zero; // no path found
    }

    private Vector2 DeconstructPath(Dictionary<Vector3Int, Vector3Int> cPath, Vector3Int entry, Vector3Int home)
    {
        Vector3Int currPos = entry;
        while (cPath[currPos] != home)
        {
            // print(currPos);
            currPos = cPath[currPos];
        }

        Vector3Int pathDirection = currPos - home;
        if (Directions[0] == pathDirection)
        {
            return new Vector2(1, 1);
        }
        else if (Directions[1] == pathDirection)
        {
            return new Vector2(-1, -1);
        }
        else if (Directions[2] == pathDirection)
        {
            return new Vector2(-1, 1);
        }
        else if (Directions[3] == pathDirection)
        {
            return new Vector2(1, -1);
        }
        else if (Directions[4] == pathDirection)
        {
            return new Vector2(0, 1);
        }
        else if (Directions[5] == pathDirection)
        {
            return new Vector2(1, 0);
        }
        else if (Directions[6] == pathDirection)
        {
            return new Vector2(0, -1);
        }
        else if (Directions[7] == pathDirection)
        {
            return new Vector2(-1, 0);
        }
        return Vector2.zero;
    }
}
