using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI WoodText;
    public TextMeshProUGUI FleshText;
    public TextMeshProUGUI FungusText;
    public TextMeshProUGUI MetalText;

    protected LocalPlayer localPlayer = null;

    protected ItemData woodData;
    protected ItemData fleshData;
    protected ItemData fungusData;
    protected ItemData metalData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (localPlayer == null)
        {
            localPlayer = GameManager.Instance.GetClientLocalPlayer();

            woodData = ItemDatasContainer.Instance.GetItemData("Wood");
            fleshData = ItemDatasContainer.Instance.GetItemData("Flesh");
            fungusData = ItemDatasContainer.Instance.GetItemData("Fungus");
            metalData = ItemDatasContainer.Instance.GetItemData("Metal");
        }
        else
        {
            WoodText.text = ItemManager.Instance.GetItemDataCount(localPlayer, woodData).ToString();
            FleshText.text = ItemManager.Instance.GetItemDataCount(localPlayer, fleshData).ToString();
            FungusText.text = ItemManager.Instance.GetItemDataCount(localPlayer, fungusData).ToString();
            MetalText.text = ItemManager.Instance.GetItemDataCount(localPlayer, metalData).ToString();
        }
    }
}
