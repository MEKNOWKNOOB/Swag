using Unity.Netcode;
using UnityEngine;

public class WorldManager : NetworkEntity
{
    [Header("WorldManager")]
    private int dayLength = 60; // seconds
    public static WorldManager Instance;
    public NetworkVariable<float> SurvivalTime = new(writePerm: NetworkVariableWritePermission.Server);

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (IsServer)
        {
            SurvivalTime.Value = Time.timeSinceLevelLoad;
        }
    }

    public int GetDay()
    {
        return Mathf.FloorToInt(SurvivalTime.Value / dayLength)+1;
    }
}
