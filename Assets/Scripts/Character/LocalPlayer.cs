using UnityEngine;

public class LocalPlayer : NetworkEntity
{
    protected override void Start()
    {
        base.Start();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            return;
        }
    }

    void Update()
    {
        if (!IsOwner) return;
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        ((Movement)NetworkComponents["Movement"]).Move(InputManager.instance.MoveVector);
    }
}
