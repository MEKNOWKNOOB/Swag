using UnityEngine;
using Unity.Netcode;

public class NetBridge : NetworkBehaviour
{
    public GameObject prefab;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Instantiate(prefab).GetComponent<NetworkObject>().Spawn();
        }
    }
}
