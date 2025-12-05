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

    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        NetworkEntity entity = collision.GetComponent<NetworkEntity>();
        if (entity != null && !collision.CompareTag(Owner) && entity.NetworkComponents.ContainsKey("Health"))
        {
            // Debug.Log("Hitbox collided with " + collision.name);
            ((Health)entity.NetworkComponents["Health"]).ChangeHealth(-(int)Damage);
        }
    }
}
