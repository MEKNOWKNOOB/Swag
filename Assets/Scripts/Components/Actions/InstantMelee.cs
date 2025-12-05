using Unity.Netcode;
using UnityEngine;

public class InstantMelee : Action
{
    [SerializeField] private GameObject hitboxPrefab;
    public override bool Active(Tool tool, NetworkEntity user)
    {
        if(!base.Active(tool, user)) return false;
        SpawnHitboxServerRpc(tool.Damage, user.transform.position, user.gameObject.tag);
        return true;
    }

    [ServerRpc]
    private void SpawnHitboxServerRpc(float damage, Vector3 pos, string user)
    {
        GameObject hitboxObj = Instantiate(hitboxPrefab, pos, Quaternion.identity);
        Hitbox hitbox = hitboxObj.GetComponent<Hitbox>();
        hitbox.Owner = user;
        hitbox.Damage = damage;
    }
}
