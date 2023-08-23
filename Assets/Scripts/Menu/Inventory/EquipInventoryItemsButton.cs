using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInventoryItemsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryEquipmentUI;
    public int indexItemClicked;
    public void OpenEquipUI()
    {
        inventoryEquipmentUI.SetActive(true); 
    }

    public void CloseEquipUI()
    {
        inventoryEquipmentUI.SetActive(false);
    }


}
