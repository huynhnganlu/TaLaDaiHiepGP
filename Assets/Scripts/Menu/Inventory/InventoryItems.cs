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
    public int itemLevel;
    public Sprite itemImage; 
    void Start()
    {
        InventoryController.Instance.AddInventoryItems(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryController.Instance.OnInventoryItemsClick(this);
    }
}
