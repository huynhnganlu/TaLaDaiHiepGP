using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItems : MonoBehaviour, IPointerClickHandler
{
    public string itemName, itemOrigin, itemProperty, itemHistory, itemEffect;
    public int itemLevel, itemID, itemHP, itemMP;
    public Sprite itemImage;

    void Start()
    {
        InventoryController.Instance.AddInventoryItems(this);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryController.Instance.OnInventoryItemsClick(this);
    }
}
