using Unity.Netcode;
using UnityEngine;

public class BareHandsMelee : Action
{
    [SerializeField] private GameObject hitboxPrefab;
    public override bool Active(Tool tool, NetworkEntity user)
    {
        //Debug.Log("Attempted to use bare hands melee");
        if(!base.Active(tool, user)) return false;
        //Debug.Log("passed first check");
        SpawnHitboxServerRpc(tool.Damage, user.transform.position, user.gameObject.tag);
        return true;
    }

    public override bool Active(Tool tool, NetworkEntity user, Vector2 direction)
    {
        if(!base.Active(tool, user)) return false;
        SpawnHitboxServerRpc(tool.Damage, user.transform.position + new Vector3(direction.x, direction.y, 0) * tool.Range, user.gameObject.tag);
        return true;
    }

    [ServerRpc]
    private void SpawnHitboxServerRpc(float damage, Vector3 pos, string user)
    {
        //Debug.Log("Spawned bare hands melee hitbox");
        //Debug.Log("second melee hitbox");
        GameObject hitboxObj = Instantiate(hitboxPrefab, pos, Quaternion.identity);
        Hitbox hitbox = hitboxObj.GetComponent<Hitbox>();
        hitbox.Owner = user;
        hitbox.Damage = damage;
    }
}
