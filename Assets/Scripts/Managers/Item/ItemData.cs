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
    [NonSerialized] public Type ActionType = null;

    public ItemData(string itemName, ItemType itemType)
    {
        this.ItemName = itemName;
        this.ItemType = itemType;
    }
}
