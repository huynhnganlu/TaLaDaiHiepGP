using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public GameObject inventoryItem;
    public ShopController shopController;
    public List<InventoryItems> inventoryItemsList;
    private InventoryItems currentItem, defaultItem;
    [SerializeField]
    private TextMeshProUGUI nameTextUI, descriptionTextUI;
    [SerializeField]
    private Image imageUI;
    [SerializeField]
    private EquipInventoryItemsButton equipButton;
    // Start is called before the first frame update
    void Start()
    {
        getBoughtInner();
        currentItem = defaultItem;
        SetItemValue(defaultItem);
    }

    //Danh sach cac noi cong da mua & khoi tao UI & truyen du lieu cua cac Inventory Item
    void getBoughtInner()
    {
        foreach(ShopItemData data in shopController.shopItemsData)
        {
            if(data.isBuying == true)
            {
                GameObject item = Instantiate(inventoryItem, transform.position, transform.rotation);
                item.GetComponentInChildren<TextMeshProUGUI>().text = data.itemDescription;
                item.transform.SetParent(this.transform, false);
                InventoryItems itemData = item.GetComponent<InventoryItems>();  
                itemData.itemName = data.name;
                itemData.itemDescription = data.itemDescription;
                itemData.itemImage = data.itemImage;
                if (defaultItem == null)
                    defaultItem = itemData;
            }
        }
    }

    public void AddInventoryItems(InventoryItems inventoryItems)
    {
        if (inventoryItemsList == null)
            inventoryItemsList = new List<InventoryItems>();
        inventoryItemsList.Add(inventoryItems);
    }
  
    public void OnInventoryItemsClick(InventoryItems inventoryItems)
    {
        if(inventoryItems != currentItem)
        {
            SetItemValue(inventoryItems);
            currentItem = inventoryItems;
        }
    }

    public void SetItemValue(InventoryItems inventoryItems)
    {
        nameTextUI.text = inventoryItems.itemName;
        descriptionTextUI.text = inventoryItems.itemDescription;
        imageUI.sprite = inventoryItems.itemImage;
        equipButton.indexItemClicked = inventoryItems.transform.GetSiblingIndex();
    }
}
