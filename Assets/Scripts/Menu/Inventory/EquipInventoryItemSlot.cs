using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipInventoryItemSlot : MonoBehaviour, IPointerClickHandler
{
  
    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryController.Instance.CloseEquipUI();
        EquipInventoryController.Instance.EquipItem(transform.GetSiblingIndex());
    }
}
