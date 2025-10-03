using System;
using Unity.Netcode;
using UnityEngine;

public class Movement : NetworkComponent
{
    [SerializeField] private float Speed = 1000f;
    [NonSerialized] private Rigidbody2D rb;
    [NonSerialized] public Vector2 Direction = new(0, 0);



    // Network Sync Data
    private NetworkVariable<Vector3> netPosition = new(writePerm: NetworkVariableWritePermission.Server);

    void Awake()
    {
        RefName = "Movement";
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnNetworkSpawn()
    {
        netPosition.OnValueChanged += SyncPos;
    }

    void Start()
    {

    }

    void Update()
    {
        // Sync Server Data to Client
        if (IsServer)
        {
            netPosition.Value = transform.position;
        }

        // Client Side Update
        if (IsOwner)
        {
            Move(Direction);
        }
    }



    /// <summary>
    /// Handles Movement and Updates/Syncs Transform.Position
    /// </summary>
    public Vector3 Move(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            return transform.position;
        }

        // Instant Client Movement
        if (IsClient && !IsServer)
        {
            rb.AddForce(Time.deltaTime * Speed * ((WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"]).GetTileDataWalkSpeed("Ground", Entity.transform.position) * dir, ForceMode2D.Impulse);
        }

        // Update Server
        SubmitMoveServerRpc(dir);

        return transform.position;
    }

    [ServerRpc]
    public void SubmitMoveServerRpc(Vector2 dir)
    {
        // Update Server Position
        rb.AddForce(Time.deltaTime * Speed * ((WorldTileMap)WorldManager.Instance.NetworkComponents["WorldTileMap"]).GetTileDataWalkSpeed("Ground", Entity.transform.position) * dir, ForceMode2D.Impulse);
        netPosition.Value = transform.position;
    }

    /// <summary>
    /// Networking Delegate for netPos
    /// </summary>
    private void SyncPos(Vector3 prev, Vector3 curr)
    {
        transform.position = curr;
    }
}