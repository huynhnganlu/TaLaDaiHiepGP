using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItems : MonoBehaviour, IPointerClickHandler
{
    public string itemName, itemOrigin, itemProperty, itemHistory;
    public int itemLevel, itemID;
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
