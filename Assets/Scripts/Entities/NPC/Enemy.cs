using UnityEngine;

public class Enemy : NetworkEntity
{
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
        
        EnemyManager.Instance.Enemies.Add(this);
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
