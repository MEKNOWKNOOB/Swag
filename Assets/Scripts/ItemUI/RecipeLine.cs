using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeLine : MonoBehaviour
{
    public CraftingRecipe CraftingRecipe;

    public TextMeshProUGUI WoodText;
    public TextMeshProUGUI FleshText;
    public TextMeshProUGUI FungusText;
    public TextMeshProUGUI MetalText;
    public TextMeshProUGUI OutputText;

    public Image RecipeOutputImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCraftingRecipe(CraftingRecipe craftingRecipe)
    {
        CraftingRecipe = craftingRecipe;

        WoodText.text = craftingRecipe.RecipeInput.GetValueOrDefault(ItemDatasContainer.Instance.GetItemData("Wood"), 0).ToString();
        FleshText.text = craftingRecipe.RecipeInput.GetValueOrDefault(ItemDatasContainer.Instance.GetItemData("Flesh"), 0).ToString();
        FungusText.text = craftingRecipe.RecipeInput.GetValueOrDefault(ItemDatasContainer.Instance.GetItemData("Fungus"), 0).ToString();
        MetalText.text = craftingRecipe.RecipeInput.GetValueOrDefault(ItemDatasContainer.Instance.GetItemData("Metal"), 0).ToString();
        OutputText.text = craftingRecipe.RecipeDisplayName;
        RecipeOutputImage.sprite = craftingRecipe.RecipeSprite;
    }

    public void AttemptCraftingRecipe()
    {
        LocalPlayer player = GameManager.Instance.GetClientLocalPlayer();
        ItemManager.Instance.AttemptCraft(player, CraftingRecipe);
    }
}
