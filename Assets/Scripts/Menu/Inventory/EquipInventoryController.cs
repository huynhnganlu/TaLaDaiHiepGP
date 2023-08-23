using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventoryController : MonoBehaviour
{
    private List<InventoryItems> equipedInventoryItems;
    [SerializeField]
    private InventoryItemController inventoryItemController;
    [SerializeField]
    private EquipInventoryItemsButton equipInventoryItemsButton;
    public void EquipItem(int slot)
    {
        if (equipedInventoryItems == null)
        {
            equipedInventoryItems = new List<InventoryItems>();
            equipedInventoryItems.Add(null);
            equipedInventoryItems.Add(null);
            equipedInventoryItems.Add(null);
        }
        InventoryItems equipItem = inventoryItemController.inventoryItemsList[equipInventoryItemsButton.indexItemClicked];
        bool alreadyAttached = false;
        int index = 0;
        for (int i = 0; i < equipedInventoryItems.Count; i++)
        {
            if (equipedInventoryItems[i] != null && equipedInventoryItems[i].Equals(equipItem) && (i != slot))
            {
                alreadyAttached = true;
                index = i;
            }
        }

        if (alreadyAttached == true)
            equipedInventoryItems[index] = null;
        equipedInventoryItems[slot] = equipItem;
    }

}
