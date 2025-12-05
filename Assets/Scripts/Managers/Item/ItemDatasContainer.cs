using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatasContainer : MonoBehaviour
{
    public static ItemDatasContainer Instance;

    [SerializeField] public List<ItemData> ItemDatas = new List<ItemData>();
    [NonSerialized] public Dictionary<string, ItemData> ItemDatasByName = new Dictionary<string, ItemData>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void InitializeItemDatas()
    {
        AddItemData(new ItemData("TestItem1", ItemType.Debug, "Test Item 1"));
        AddItemData(new ItemData("TestItem2", ItemType.Debug, "Test Item 2"));
        AddItemData(new ItemData("TestItem3", ItemType.Debug, "Test Item 3"));

        //AddItemData(new ItemData("TestWeapon", ItemType.Tool));
        //AddItemData(new ItemData("BareHands", ItemType.Tool));

        AddItemData(new ItemData("Wood", ItemType.Material, "Wood"));
        AddItemData(new ItemData("Metal", ItemType.Material, "Metal"));
        AddItemData(new ItemData("Flesh", ItemType.Material, "Flesh"));
        AddItemData(new ItemData("Fungus", ItemType.Material, "Fungus"));

        // Go through and add all of the itemDatas that were only in the list
        foreach (ItemData itemData in ItemDatas)
        {
            AddItemData(itemData);
        }

        //AddItemData(new ItemData("Sword", ItemType.Tool, "Sword"));
        //AddItemData(new ItemData("MushroomStew", ItemType.Tool, "Mushroom Stew"));
    }

    public void AddItemData(ItemData itemData)
    {
        if (!ItemDatas.Contains(itemData))
        {
            ItemDatas.Add(itemData);
        }

        if (!ItemDatasByName.ContainsKey(itemData.ItemName))
        {
            ItemDatasByName.Add(itemData.ItemName, itemData);
        }
    }

    public ItemData GetItemData(string itemDataName)
    {
        return ItemDatasByName.GetValueOrDefault(itemDataName, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
