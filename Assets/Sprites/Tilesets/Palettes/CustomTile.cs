using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "CustomTile")]
public class CustomTile : Tile
{
    [Header("TileData", order = 0)]
    [SerializeField] public bool walkable;
    [SerializeField] public float walkSpeed;
    [SerializeField] public float pathingCost;
    [SerializeField] public float maxHealth;
}
