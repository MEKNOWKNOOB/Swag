using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public LocalPlayer LocalPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ItemData activatedItem = null;

        if (InputManager.Instance.Hotbar1Bool)
        {
            activatedItem = ItemManager.Instance.GetHotbarItem(LocalPlayer, 0);
        }
        else if (InputManager.Instance.Hotbar2Bool)
        {
            activatedItem = ItemManager.Instance.GetHotbarItem(LocalPlayer, 1);
        }
        else if (InputManager.Instance.Hotbar3Bool)
        {
            activatedItem = ItemManager.Instance.GetHotbarItem(LocalPlayer, 2);
        }

        ItemManager.Instance.ActivateItem(LocalPlayer, activatedItem);
    }
}
