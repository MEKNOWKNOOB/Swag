using UnityEngine;

public class Enemy : NetworkEntity
{
    [Header("Enemy")]
    public int Difficulty = 0;
    public float Power = 1; // CANNOT BE ZERO

    protected override void Start()
    {
        base.Start();
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkObject.ChangeOwnership(Unity.Netcode.NetworkManager.Singleton.LocalClientId);
        }
        
        EnemyManager.Instance.AllEnemies.Add(this);
    }

    void Update()
    {
        if (!IsOwner) return;
    }

    void FixedUpdate()
    {
        if (!IsServer) return;

       ((Movement)NetworkComponents["Movement"]).Direction = ((Behavior)NetworkComponents["Behavior"]).NextMove();
    }
}
