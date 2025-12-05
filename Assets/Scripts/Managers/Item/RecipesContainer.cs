using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipesContainer : MonoBehaviour
{
    public static RecipesContainer Instance;

    [SerializeField] public List<CraftingRecipe> CraftingRecipes = new List<CraftingRecipe>();
    [NonSerialized] public Dictionary<string, CraftingRecipe> CraftingRecipesByName = new Dictionary<string, CraftingRecipe>();

    public GameObject RecipeLinePrefab;
    public GameObject CraftingMenu;

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

        /*
        AddRecipe(new CraftingRecipe("TestRecipe1", RecipeType.Debug)
            .AddInput(items.GetItemData("TestItem1"), 1)
            .AddInput(items.GetItemData("TestItem2"), 2)
            .AddOutput(items.GetItemData("TestItem3"), 1)
            );
        */

        AddRecipe(new CraftingRecipe("Wooden Sword", RecipeType.Assemble, "Wooden Sword")
            .AddInput(items.GetItemData("Wood"), 2)
            );

        AddRecipe(new CraftingRecipe("Flesh Sword", RecipeType.Assemble, "Flesh Sword")
            .AddInput(items.GetItemData("Wood"), 2)
            .AddInput(items.GetItemData("Flesh"), 4)
            );

        /*
        AddRecipe(new CraftingRecipe("TestWeapon", RecipeType.Upgrade)
            .AddInput(items.GetItemData("BareHands"), 1)
            .AddOutput(items.GetItemData("TestWeapon"), 1)
            );
        */
    }

    public GameObject GenerateRecipeLineUI(CraftingRecipe craftingRecipe)
    {
        // Generate the UI
        GameObject recipeLineObject = Instantiate(RecipeLinePrefab);
        RecipeLine recipeLine = recipeLineObject.GetComponent<RecipeLine>();

        // Assign the crafting recipe
        recipeLine.SetCraftingRecipe(craftingRecipe);

        // Return the object itself
        return recipeLineObject;
    }

    public void GenerateAllRecipeLineUIs()
    {
        float yPos = -25;
        float yOffset = -50;
        int index = 0;
        foreach (CraftingRecipe recipe in CraftingRecipes)
        {
            GameObject lineObj = GenerateRecipeLineUI(recipe);

            float currentXPos = lineObj.GetComponent<RectTransform>().rect.position.x;
            currentXPos = 180; // For now, just hardcode it
            float currentYPos = yPos + yOffset * index;

            Vector2 position = new Vector2(currentXPos, currentYPos);

            // Set the position and parent of the line object
            lineObj.transform.SetParent(CraftingMenu.transform);
            lineObj.GetComponent<RectTransform>().anchoredPosition = position;

            index++;
        }
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
