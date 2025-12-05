using UnityEngine;

public class Harvestable : NetworkEntity
{
    public string ResourceName = "Unknown";
    public int ResourceCount = 1;

    protected bool HasGivenResources = false;

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
    }

    void Update()
    {
        if (!IsOwner) return;
    }

    void FixedUpdate()
    {
        if (!IsServer) return;
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
        Debug.Log(string.Format("Gave {0} of {1} to {2}", ResourceCount, ResourceName, closest.gameObject.name));
        ItemManager.Instance.AddItem(closest, ItemDatasContainer.Instance.GetItemData(ResourceName), ResourceCount);
    }
}
