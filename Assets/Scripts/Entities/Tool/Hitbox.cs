using UnityEngine;

public class Hitbox : NetworkEntity
{
    [Header("Hitbox")]
    [SerializeField] private float damage = 1;
    [SerializeField] private BoxCollider2D hitBoxCollider;

    protected override void Start()
    {
        base.Start();
    }


}
