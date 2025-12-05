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


        Vector2 currDirection = ((Behavior)NetworkComponents["Behavior"]).NextDirection();
        if(currDirection != Vector2.zero) Direction = currDirection;
        ((Movement)NetworkComponents["Movement"]).Direction = currDirection;
        if(((Behavior)NetworkComponents["Behavior"]).NextAttack()) ((Tool)NetworkComponents["Tool"]).Use(this, Direction);
    }
}
