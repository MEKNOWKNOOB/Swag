using UnityEngine;

public class LocalPlayer : NetworkEntity
{
    protected override void Start()
    {
        base.Start();
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

        ((Movement)NetworkComponents["Movement"]).Direction = InputManager.Instance.MoveVector;
        //((Health)NetworkComponents["Health"]) = ;
    }
}
