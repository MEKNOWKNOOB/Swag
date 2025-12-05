using System;
using UnityEngine;

public class Hitbox : NetworkEntity
{
    public enum DamageSource
    {
        Player,
        Enemy
    }
    [NonSerialized] public string Owner;
    [NonSerialized] public float Damage;
    

    [Header("Hitbox")]
    [SerializeField] private Collider2D hitBoxCollider;
    [SerializeField] public float lifetime = 0.00001f;

    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        NetworkEntity entity = collision.GetComponent<NetworkEntity>();
        // Debug.Log("Hitbox collided with " + collision.name);
        if (entity != null && !collision.CompareTag(Owner) && entity.NetworkComponents.ContainsKey("Health"))
        {
            ((Health)entity.NetworkComponents["Health"]).ChangeHealth(-(int)Damage);
        }
    }
}
