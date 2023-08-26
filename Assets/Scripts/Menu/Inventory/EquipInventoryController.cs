using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventoryController : MonoBehaviour
{
    private List<InventoryItems> equipedInventoryItemsList;
    [SerializeField]
    private InventoryItemController inventoryItemController;
    [SerializeField]
    private EquipInventoryItemsButton equipInventoryItemsButton;
    [SerializeField]
    private EquipInventoryItemsUI equipInventoryItemsUI;
    public void EquipItem(int slot)
    {
        if (equipedInventoryItemsList == null)
        {
            equipedInventoryItemsList = new List<InventoryItems>();
            equipedInventoryItemsList.Add(null);
            equipedInventoryItemsList.Add(null);
            equipedInventoryItemsList.Add(null);
        }
        InventoryItems equipItem = inventoryItemController.inventoryItemsList[equipInventoryItemsButton.indexItemClicked];
        bool alreadyAttached = false;
        int index = 0;
        for (int i = 0; i < equipedInventoryItemsList.Count; i++)
        {
            if (equipedInventoryItemsList[i] != null && equipedInventoryItemsList[i].Equals(equipItem) && (i != slot))
            {
                alreadyAttached = true;
                index = i;
            }
        }

        if (alreadyAttached == true)
            equipedInventoryItemsList[index] = null;
        equipedInventoryItemsList[slot] = equipItem;
        equipInventoryItemsUI.CreateEquipedInventoryItemsUI(equipedInventoryItemsList);
    }

}
