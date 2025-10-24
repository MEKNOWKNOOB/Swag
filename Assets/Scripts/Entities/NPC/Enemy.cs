using UnityEditor.SearchService;
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
    }

    void Update()
    {
        if (!IsOwner) return;
    }

    void FixedUpdate()
    {
        if (!IsServer) return;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            ((Movement)NetworkComponents["Movement"]).Direction = ((Pathing)NetworkComponents["Pathing"]).PathToTarget(player.transform.position, 9, 1000);
        else
            ((Movement)NetworkComponents["Movement"]).Direction = new Vector2(0, 0);
    }
}
