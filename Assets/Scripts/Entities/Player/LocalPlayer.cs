using UnityEngine;

public class LocalPlayer : NetworkEntity
{
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.AddPlayer(this);
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner) return;
        GetComponentInChildren<Camera>().enabled = false;
        GetComponentInChildren<AudioListener>().enabled = false;
    }

    void Update()
    {
        if (!IsOwner) return;
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        Direction = InputManager.Instance.MoveVector;
        ((Movement)NetworkComponents["Movement"]).Direction = InputManager.Instance.MoveVector;
    }
}
