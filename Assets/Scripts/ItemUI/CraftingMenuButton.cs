using UnityEngine;

public class CraftingMenuButton : MonoBehaviour
{
    public GameObject CraftingMenu;

    public void ToggleCraftingMenuVisibility()
    {
        CraftingMenu.SetActive(!CraftingMenu.activeSelf);
    }
}
