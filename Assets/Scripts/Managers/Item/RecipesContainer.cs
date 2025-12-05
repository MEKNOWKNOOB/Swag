using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipesContainer : MonoBehaviour
{
    public static RecipesContainer Instance;

    [SerializeField] public List<CraftingRecipe> CraftingRecipes = new List<CraftingRecipe>();
    [NonSerialized] public Dictionary<string, CraftingRecipe> CraftingRecipesByName = new Dictionary<string, CraftingRecipe>();

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

    public void InitializeRecipes()
    {
        // All recipes go here
        ItemDatasContainer items = ItemDatasContainer.Instance;

        AddRecipe(new CraftingRecipe("TestRecipe1", RecipeType.Debug)
            .AddInput(items.GetItemData("TestItem1"), 1)
            .AddInput(items.GetItemData("TestItem2"), 2)
            .AddOutput(items.GetItemData("TestItem3"), 1)
            );

        AddRecipe(new CraftingRecipe("TestWeapon", RecipeType.Upgrade)
            .AddInput(items.GetItemData("BareHands"), 1)
            .AddOutput(items.GetItemData("TestWeapon"), 1)
            );
    }

    public void AddRecipe(CraftingRecipe craftingRecipe)
    {
        CraftingRecipes.Add(craftingRecipe);
        CraftingRecipesByName.Add(craftingRecipe.RecipeName, craftingRecipe);
    }

    public CraftingRecipe GetRecipe(string recipeName)
    {
        return CraftingRecipesByName.GetValueOrDefault(recipeName, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
