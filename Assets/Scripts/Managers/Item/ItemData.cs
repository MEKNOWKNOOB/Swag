using System;
using UnityEngine;

public enum ItemType
{
    None,
    Debug,
    Tool,
    Material
}

[Serializable]
public class ItemData
{
    [SerializeField] public ItemType ItemType = ItemType.None;
    [SerializeField] public string ItemName = "UnknownItem";
    [SerializeField] public GameObject ItemPrefab = null;
    [SerializeField] public string ItemDisplayName = "Unknown Item";
    [SerializeField] public Sprite ItemSprite;

    public ItemData(string itemName, ItemType itemType, string itemDisplayName)
    {
        this.ItemName = itemName;
        this.ItemType = itemType;
        this.ItemDisplayName = itemDisplayName;
    }
}
