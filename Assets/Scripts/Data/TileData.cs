using UnityEngine;

[System.Serializable]
public class TileData
{
    public TileData(TileData other)
    {
        walkable = other.walkable;
        walkSpeed = other.walkSpeed;
        pathingCost = other.pathingCost;
        maxHealth = other.maxHealth;
    }

    [SerializeField] public bool walkable;
    [SerializeField] public float walkSpeed;
    [SerializeField] public float pathingCost;
    [SerializeField] public float maxHealth;
}
