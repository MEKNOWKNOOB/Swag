using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : NetworkEntity
{
    public static ItemManager Instance;

    public Dictionary<LocalPlayer, Dictionary<ItemData, int>> PlayerStorage = new Dictionary<LocalPlayer, Dictionary<ItemData, int>>();
    public Dictionary<LocalPlayer, Dictionary<int, ItemData>> PlayerHotbar = new Dictionary<LocalPlayer, Dictionary<int, ItemData>>();

    public ItemDatasContainer ItemDatas;
    public RecipesContainer Recipes;

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
    protected override void Start()
    {
        base.Start();

        ItemDatas?.InitializeItemDatas();
        Recipes?.InitializeRecipes();

        GameManager.Instance.OnPlayerAdded += AddPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(LocalPlayer player)
    {
        // When adding a player, we want to initialize for them:
        // - storage
        // - hotbar
        PlayerStorage.Add(player, new Dictionary<ItemData, int>());
        PlayerHotbar.Add(player, new Dictionary<int, ItemData>());

        // For now, let's just add a starter tool for the player to use.
        ItemDatasContainer items = ItemDatasContainer.Instance;

        AddItem(player, items.GetItemData("BareHands"), 1);
        EquipItem(player, items.GetItemData("BareHands"), 0);
    }

    public void RemovePlayer(LocalPlayer player)
    {
        // When removing a player, we want to get rid of all of their items
        PlayerStorage.Remove(player);
        PlayerHotbar.Remove(player);
    }

    public void UnequipItem(LocalPlayer player, int index)
    {
        PlayerHotbar[player].Remove(index);
    }

    public void EquipItem(LocalPlayer player, ItemData itemData, int index)
    {
        UnequipItem(player, index);
        PlayerHotbar[player].Add(index, itemData);
    }

    public int GetItemDataCount(LocalPlayer player, ItemData itemData)
    {
        if (!PlayerStorage.ContainsKey(player))
        {
            return -1;
        }

        int val = -1;
        bool success = PlayerStorage[player].TryGetValue(itemData, out val);
        if (success)
        {
            return val;
        }
        else
        {
            return 0;
        }
    }

    public ItemData GetHotbarItem(LocalPlayer player, int index)
    {
        return PlayerHotbar[player][index];
    }

    public void ActivateItem(LocalPlayer player, ItemData itemData)
    {
        // For this player, we want to add the action attached to the item
        if (itemData.ItemName == null)
        {
            return;
        }

        // player.gameObject.AddComponent<itemData.Action.GetType()>();
    }

    public bool HasItem(LocalPlayer player, ItemData itemData)
    {
        if (!PlayerStorage.ContainsKey(player))
        {
            return false;
        }

        return PlayerStorage[player].ContainsKey(itemData);
    }

    public bool HasItems(LocalPlayer player, Dictionary<ItemData, int> itemDatas)
    {
        foreach (ItemData itemData in itemDatas.Keys)
        {
            int desiredCount = itemDatas[itemData];

            if (GetItemDataCount(player, itemData) < desiredCount)
            {
                return false;
            }
        }
        return true;
    }

    public bool HasItems(LocalPlayer player, List<ItemData> itemDatas)
    {
        foreach (ItemData itemData in itemDatas)
        {
            if (!HasItem(player, itemData))
            {
                return false;
            }
        }
        return true;
    }

    public void ChangeItemCount(LocalPlayer player, ItemData itemData, int change)
    {
        if (!PlayerStorage.ContainsKey(player))
        {
            return;
        }

        int newCount = Mathf.Max(0, GetItemDataCount(player, itemData) + change);

        PlayerStorage[player].Add(itemData, newCount);
    }

    public void AddItem(LocalPlayer player, ItemData itemData, int change)
    {
        ChangeItemCount(player, itemData, change);
    }

    public void AddItems(LocalPlayer player, Dictionary<ItemData, int> itemDatas)
    {
        foreach (ItemData itemData in itemDatas.Keys)
        {
            AddItem(player, itemData, itemDatas[itemData]);
        }
    }

    public void RemoveItem(LocalPlayer player, ItemData itemData, int change)
    {
        ChangeItemCount(player, itemData, -change);
    }

    public void RemoveItems(LocalPlayer player, Dictionary<ItemData, int> itemDatas)
    {
        foreach (ItemData itemData in itemDatas.Keys)
        {
            RemoveItem(player, itemData, itemDatas[itemData]);
        }
    }

    public void LogPlayerStorage(LocalPlayer player)
    {
        foreach (ItemData itemData in PlayerStorage[player].Keys)
        {
            Debug.Log(string.Format("Item <{0}>: {1}x", itemData, PlayerStorage[player][itemData]));
        }
    }

    public bool CanCraft(LocalPlayer player, CraftingRecipe recipe)
    {
        return HasItems(player, recipe.RecipeInput);
    }

    public bool AttemptCraft(LocalPlayer player, CraftingRecipe recipe)
    {
        bool canCraft = CanCraft(player, recipe);

        if (!canCraft)
        {
            return false;
        }

        RemoveItems(player, recipe.RecipeInput);
        AddItems(player, recipe.RecipeOutput);

        return true;
    }
}
