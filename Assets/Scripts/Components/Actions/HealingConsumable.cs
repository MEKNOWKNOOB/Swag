using Unity.Netcode;
using UnityEngine;

public class HealingConsumable : Action
{
    [SerializeField] public int HealAmount = 1;

    public override bool Active(Tool tool, NetworkEntity user)
    {
        //Debug.Log("Attempted to use bare hands melee");
        if (!base.Active(tool, user)) return false;
        //Debug.Log("passed first check");
        //SpawnHitboxServerRpc(tool.Damage, user.transform.position, user.gameObject.tag);
        HealSelfServerRpc(user.gameObject.name);
        return true;
    }

    public override bool Active(Tool tool, NetworkEntity user, Vector2 direction)
    {
        return Active(tool, user);
    }

    [ServerRpc]
    private void HealSelfServerRpc(string user)
    {
        // Heal the user
        LocalPlayer player = GameManager.Instance.GetPlayerByName(user);
        Health health = player.gameObject.GetComponent<Health>();
        health.ChangeHealth(HealAmount);

        // Consume the item: decrement the count, if 0 then unequip it
        ItemManager.Instance.RemoveItem(player, ItemDatasContainer.Instance.GetItemData("MushroomStew"), 1);
    }

    /*
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
    */
}
