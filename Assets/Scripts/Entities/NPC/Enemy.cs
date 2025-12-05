using Unity.Services.Multiplay.Authoring.Core.Assets;
using UnityEngine;

public class Enemy : NetworkEntity
{
    [Header("Enemy")]
    public int Difficulty = 0;
    public float Power = 1; // CANNOT BE ZERO
    public bool HasGivenResources = false;


    protected override void Start()
    {
        base.Start();

        Health health = GetComponent<Health>();
        if (health != null)
        {
            health.SourceHealthChange += OnDamageCallback;
        }
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
    void OnDamageCallback(int damage)
    {
        if (HasGivenResources)
        {
            return;
        }

        HasGivenResources = true;

        // Get the closest player
        LocalPlayer closest = GameManager.Instance.Players[0];
        float distance = Mathf.Infinity;
        foreach (LocalPlayer player in GameManager.Instance.Players)
        {
            float tempDist = (player.transform.position - gameObject.transform.position).sqrMagnitude;
            if (tempDist < distance)
            {
                closest = player;
                distance = tempDist;
            }
        }

        // Give them resources
        // Debug.Log(string.Format("Gave {0} of {1} to {2}", ResourceCount, ResourceName, closest.gameObject.name));
        ItemManager.Instance.AddItem(closest, ItemDatasContainer.Instance.GetItemData("Flesh"), 2);
    }
}
