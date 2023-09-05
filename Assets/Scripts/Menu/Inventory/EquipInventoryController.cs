using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipInventoryController : MonoBehaviour
{
    //Danh sach cac item duoc trang bi {Mac dinh se co 3}
    private List<InventoryItems> equipedInventoryItemsList;

    //Reference den xu ly UI o Character
    [SerializeField]
    private EquipInventoryItemsUI equipInventoryItemsUI;

    [SerializeField]
    private CharacterData characterData;

    //Singleton
    public static EquipInventoryController Instance;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //Tao danh sach co kich thuoc la 3 de luu tru item
        if (equipedInventoryItemsList == null)
        {
            equipedInventoryItemsList = new List<InventoryItems>();
            equipedInventoryItemsList.Add(null);
            equipedInventoryItemsList.Add(null);
            equipedInventoryItemsList.Add(null);
        }
    }

    //Ham trang bi item
    public void EquipItem(int slot)
    {
        //Tao item dua tren tab duoc click va xu ly logic sau do tao UI o character
        InventoryItems equipItem = InventoryController.Instance.inventoryItemsList[InventoryController.Instance.indexItemClicked];
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
        SaveEquipedData();
    }

    public List<InventoryItems> GetListEquiped()
    {
        return equipedInventoryItemsList;
    }

    public void SaveEquipedData()
    {
        for(int i = 0;i < equipedInventoryItemsList.Count; i++)
        {
            if (equipedInventoryItemsList[i] != null)
            {
                characterData.innerImage[i] = equipedInventoryItemsList[i].itemImage;
            }
            else
            {
                characterData.innerImage[i] = null;
            }
        }
    }

}
