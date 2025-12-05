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
        safe = other.safe;
    }

    [SerializeField] public bool walkable = true;
    [SerializeField] public float walkSpeed = 1;
    [SerializeField] public float pathingCost = 1;
    [SerializeField] public float maxHealth = 1;
    [SerializeField] public bool safe = false;
}
