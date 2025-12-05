using UnityEngine;

public class Tool : NetworkComponent
{
    public float Damage = 0.0f;
    public float Range = 0.0f;
    public int Durability = -1;
    public float UseTime = 0.0f;
    
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
        Debug.Log(Entity);
        ((Action)Entity.NetworkComponents["Action"]).Active(this, user);
    }

    /// <summary>
    /// Uses Tool
    /// </summary>
    /// <param name="user"> user is NOT the Item class, it's the actual User, Tool should work without an Item class as long its part of an Entity</param>
    /// <param name="direction"> direction of use</param>
    public void Use(NetworkEntity user, Vector2 direction)
    {
        ((Action)Entity.NetworkComponents["Action"]).Active(this, user, direction);
    }
}