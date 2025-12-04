using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemySpawner : NetworkComponent
{
    public NetworkVariable<float> AvailableEnemyPower = new(writePerm: NetworkVariableWritePermission.Server);
    public float maxReductionPercentage = 0; // max percent an Enemy's power usuage is discounted
    public int radiusMin = 0;
    public int radiusMax = 0;

    void Awake()
    {
        RefName = "EnemySpawner";
    }

    public override void OnNetworkSpawn()
    {

    }

    void Update()
    {

    }

    public void AttemptSpawn(int attempts)
    {
        if (AvailableEnemyPower.Value <= 0) return;

        for (int iDiff = GameManager.Instance.Difficulty; iDiff >= 0 && attempts > 0; iDiff--)
        {

            for (; attempts > 0 && AvailableEnemyPower.Value > 0; attempts--)
            {
                Enemy currEnemy = EnemyManager.Instance.EnemyContainer[iDiff][Random.Range(0, EnemyManager.Instance.EnemyContainer[iDiff].Count)];
                float powerConsume = currEnemy.Power - currEnemy.Power * Random.Range(0.0f, maxReductionPercentage);
                if (AvailableEnemyPower.Value - powerConsume >= 0)
                {
                    if (Spawn(currEnemy.Name, radiusMin, radiusMax) != null)
                    {
                        AvailableEnemyPower.Value -= powerConsume;
                    }
                }
            }
        }
    }


    public Vector3? Spawn(string enemyName, int radiusMin, int radiusMax)
    {
        Vector3 baseDirection = new(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        baseDirection.Normalize();

        int multDist = Random.Range(radiusMin, radiusMax);
        Vector3Int direction = new((int)(baseDirection.x * multDist), (int)(baseDirection.y * multDist), 0);
        Vector3Int spawnPos = ((WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"]).CellPos("Ground", GameManager.Instance.Players[Random.Range(0, GameManager.Instance.Players.Count)].transform.position) + direction;

        if (((WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"]).IsTileValid("Ground", spawnPos))
        {
            foreach (LocalPlayer player in GameManager.Instance.Players)
            {
                if ((spawnPos - ((WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"]).CellPos("Ground", player.transform.position)).sqrMagnitude < Mathf.Pow(radiusMin, 2))
                {
                    return null;
                }
            }

            SubmitSpawnServerRpc(enemyName, spawnPos);
            return ((WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"]).CellWorldPos("Ground", spawnPos);
        }

        return null;
    }

    [ServerRpc]
    public void SubmitSpawnServerRpc(string enemyName, Vector3Int spawnPos)
    {
        GameObject newEnemy = Instantiate(EnemyManager.Instance.EnemyReferences[enemyName]);
        newEnemy.transform.position = ((WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"]).CellWorldPos("Ground", spawnPos);
        newEnemy.GetComponent<NetworkObject>().Spawn();
    }
}
