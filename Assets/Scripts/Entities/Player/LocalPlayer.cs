using UnityEngine;

public class LocalPlayer : NetworkEntity
{
    public Tool activeTool = null;

    protected override void Start()
    {
        base.Start();

        GameManager.Instance.AddPlayer(this);
    }

    public override void OnNetworkSpawn()
    {
        gameObject.transform.position = GameManager.Instance.RespawnPoint;
        if (IsOwner) return;
        GetComponentInChildren<Camera>().enabled = false;
        GetComponentInChildren<AudioListener>().enabled = false;
    }

    void Update()
    {
        if (!IsOwner) return;

        if (InputManager.Instance.Hotbar1Bool)
        {
            ItemManager.Instance.ActivateHotbarSlot(this, 0);
        }
        else if (InputManager.Instance.Hotbar2Bool)
        {
            ItemManager.Instance.ActivateHotbarSlot(this, 1);
        }
        else if (InputManager.Instance.Hotbar3Bool)
        {
            ItemManager.Instance.ActivateHotbarSlot(this, 2);
        }

        if (InputManager.Instance.AttackBool)
        {
            // If tool exists, use it
            activeTool?.Use(this, Direction);
        }
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        if (InputManager.Instance.MoveVector != Vector2.zero) Direction = InputManager.Instance.MoveVector;
        ((Movement)NetworkComponents["Movement"]).Direction = InputManager.Instance.MoveVector;
    }
}
