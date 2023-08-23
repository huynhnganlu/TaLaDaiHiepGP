using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipInventoryItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private EquipInventoryItemsButton equipInventoryItemsButton;
    [SerializeField]
    private EquipInventoryController equipInventoryController;
    public void OnPointerClick(PointerEventData eventData)
    {
        equipInventoryItemsButton.CloseEquipUI();
        equipInventoryController.EquipItem(transform.GetSiblingIndex());
    }
}
