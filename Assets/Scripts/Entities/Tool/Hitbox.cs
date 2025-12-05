using UnityEngine;

public class Hitbox : NetworkEntity
{
    public NetworkEntity Owner;

    [Header("Hitbox")]
    [SerializeField] private float damage = 1;
    [SerializeField] private Collider2D hitBoxCollider;

    protected override void Start()
    {
        base.Start();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        NetworkEntity entity = collision.GetComponent<NetworkEntity>();
        if (entity != null && entity != Owner && entity.NetworkComponents.ContainsKey("Health"))
        {
            ((Health)entity.NetworkComponents["Health"]).ChangeHealth((int)(-damage * ((Tool)Owner.NetworkComponents["Tool"]).DamageMultiplier));
            Destroy(gameObject);
        }
    }
}
