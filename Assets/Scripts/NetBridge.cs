using UnityEngine;
using Unity.Netcode;

public class NetBridge : MonoBehaviour
{
    public GameObject prefab;

    public void Start()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Instantiate(prefab).GetComponent<NetworkObject>().Spawn();
        }
    }
}
