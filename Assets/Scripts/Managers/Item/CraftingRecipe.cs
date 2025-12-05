using System;
using System.Collections.Generic;
using UnityEngine;

public enum RecipeType
{
    None,
    Debug,
    Assemble,
    Upgrade
}

[Serializable]
public class CraftingRecipe
{
    [SerializeField] public string RecipeName = "UnknownRecipe";
    [SerializeField] public string RecipeDisplayName = "Unknown";
    [SerializeField] public RecipeType RecipeType = RecipeType.None;

    [NonSerialized] public Dictionary<ItemData, int> RecipeInput = new Dictionary<ItemData, int>();
    [NonSerialized] public Dictionary<ItemData, int> RecipeOutput = new Dictionary<ItemData, int>();

    public CraftingRecipe(string recipeName, RecipeType recipeType, string recipeDisplayName)
    {
        this.RecipeName = recipeName;
        this.RecipeType = recipeType;
        RecipeDisplayName = recipeDisplayName;
    }

    public CraftingRecipe AddInput(ItemData itemData, int count)
    {
        RecipeInput.Add(itemData, count);
        return this;
    }

    public CraftingRecipe AddOutput(ItemData itemData, int count)
    {
        RecipeOutput.Add(itemData, count);
        return this;
    }
}

/*
 * 
Item itemA = new Item("Item A");
Item itemB = new Item("Item B");

CraftingRecipe a = new CraftingRecipe("test recipe").AddInput(itemA, 2).AddOutput(itemB, 1);

*/