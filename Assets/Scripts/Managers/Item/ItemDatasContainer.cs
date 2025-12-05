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
        AddItemData(new ItemData("TestItem1", ItemType.Debug));
        AddItemData(new ItemData("TestItem2", ItemType.Debug));
        AddItemData(new ItemData("TestItem3", ItemType.Debug));

        AddItemData(new ItemData("TestWeapon", ItemType.Tool));
        AddItemData(new ItemData("BareHands", ItemType.Tool));
    }

    public void AddItemData(ItemData itemData)
    {
        ItemDatas.Add(itemData);
        ItemDatasByName.Add(itemData.ItemName, itemData);
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
