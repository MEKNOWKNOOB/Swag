using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public LocalPlayer LocalPlayer;

    public TextMeshProUGUI Slot1Text;
    public Image Slot1Image;

    public TextMeshProUGUI Slot2Text;
    public Image Slot2Image;

    public TextMeshProUGUI Slot3Text;
    public Image Slot3Image;

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

        ItemData item1 = ItemManager.Instance.GetHotbarItem(LocalPlayer, 0);
        if (item1 != null)
        {
            Slot1Text.text = item1.ItemDisplayName;
            Slot1Image.sprite = item1.ItemSprite;
        }

        ItemManager.Instance.ActivateItem(LocalPlayer, activatedItem);
    }
}
