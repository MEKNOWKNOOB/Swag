using UnityEngine;

public class Tool : NetworkComponent
{
    public int Durability = -1;
    public float DamageMultiplier = 1.0f;
    public float SpeedMultiplier = 1.0f;
    
    void Awake()
    {
        RefName = "Tool";
    }

    /// <summary>
    /// Uses Tool
    /// </summary>
    /// <param name="user"> user is NOT the Item class, it's the actual User, Tool should work without an Item class as long its part of an Entity</param>
    public void Use(NetworkEntity user)
    {
        ((Action)Entity.NetworkComponents["Action"]).Active(this, user);
    }
}