using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "CustomTile")]
public class CustomTile : Tile
{
    [SerializeField] public TileData TileData;
}
