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

        }
    }

    void Update()
    {
        if (!IsOwner) return;
    }

    void FixedUpdate()
    {
        if (!IsServer) return;

        ((Movement)NetworkComponents["Movement"]).Direction = Vector2.zero;
    }
}
