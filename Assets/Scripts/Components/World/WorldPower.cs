using UnityEngine;

public class WorldPower : NetworkComponent
{
    [SerializeField] private float percentageBase = 0.01f;
    [SerializeField] private float powerAllocateMax = 4f;

    void Awake()
    {
        RefName = "WorldPower";
    }

    void Update()
    {
        if (IsServer)
        {
            if (WorldManager.Instance.GetDay() * Random.value < percentageBase * Time.deltaTime)
            {
                ((EnemySpawner)EnemyManager.Instance.NetworkComponents["EnemySpawner"]).AvailableEnemyPower.Value += GameManager.Instance.Players.Count * Random.Range(Mathf.Pow(powerAllocateMax, WorldManager.Instance.GetDay()) / 2f, Mathf.Pow(powerAllocateMax, WorldManager.Instance.GetDay()));
            }
        }
    }
}
