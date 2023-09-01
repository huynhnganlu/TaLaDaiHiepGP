using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItems : MonoBehaviour, IPointerClickHandler
{
    private InventoryController inventoryItemController;
    public string itemName, itemDescription;
    public Sprite itemImage; 
    void Start()
    {
        inventoryItemController = GetComponentInParent<InventoryController>();
        inventoryItemController.AddInventoryItems(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryItemController.OnInventoryItemsClick(this);
    }
}
