using System;
using Unity.Netcode;
using UnityEngine;

public class Movement : NetworkComponent
{
    [SerializeField] private float Speed = 5f;
    private Rigidbody2D rb;

    void Awake()
    {
        RefName = "Movement";
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    /// <summary>
    /// Handles Movement and Updates/Syncs Transform.Position
    /// </summary>
    public Vector3 Move(Vector2 dir)
    {
        // Instant Client Movement
        rb.AddForce(dir * Speed, ForceMode2D.Impulse);
        // Update Server
        SubmitMoveServerRpc(dir);

        return transform.position;
    }

    [ServerRpc]
    public void SubmitMoveServerRpc(Vector2 dir)
    {
        // Update Server Position
        rb.AddForce(dir * Speed, ForceMode2D.Impulse);
        UpdateMoveClientRpc(transform.position);
    }

    [ClientRpc]
    private void UpdateMoveClientRpc(Vector3 newPos)
    {
        // Update Client Position - Lerp Smoothness and to Avoid Rubberbanding
        transform.position = Vector3.Lerp(transform.position, newPos, 0.9f);
    }
}