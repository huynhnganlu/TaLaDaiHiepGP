using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //Xu ly cac item
    public GameObject inventoryItem;
    public ShopController shopController;
    public List<InventoryItems> inventoryItemsList;
    private InventoryItems currentItem;
    public ShopItemData defaultItemData;
    //Xu ly du lieu cua item
    [SerializeField]
    private TextMeshProUGUI nameTextUI, levelTextUI, originTextUI, propertyTextUI, historyTextUI, numberInnerUI;
    [SerializeField]
    private Image imageUI;

    //Bien luu tru danh cho equip item
    public int indexItemClicked;

    //Xu ly toggle equip item
    [SerializeField]
    private GameObject inventoryEquipmentUI;
    [SerializeField]
    private Button closeInventoryUIButton, openInventoryUIButton;

    //Singleton
    public static InventoryController Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        indexItemClicked = 0;
        currentItem = transform.GetChild(0).GetComponent<InventoryItems>();
        SetItemValue(currentItem);
        openInventoryUIButton.onClick.AddListener(() =>
        {
            OpenEquipUI();
        });
        closeInventoryUIButton.onClick.AddListener(() =>
        {
            CloseEquipUI();
        });
        
    }


    //Danh sach cac noi cong da mua & khoi tao UI & truyen du lieu cua cac Inventory Item
    //Luu y phai set Storing = false tat ca sau khi reset application game
    public void GetBoughtInner()
    {
        foreach (ShopItemData data in shopController.shopItemsData)
        {
            if(data.isBuying == true && data.isStoring == false)
            {
                GameObject item = Instantiate(inventoryItem);
                item.GetComponentInChildren<TextMeshProUGUI>().text = data.itemName;
                item.transform.SetParent(this.transform, false);
                InventoryItems itemData = item.GetComponent<InventoryItems>();  
                itemData.itemName = data.itemName;
                itemData.itemLevel = data.itemLevel;
                itemData.itemImage = data.itemImage;
                itemData.itemOrigin = data.itemOrigin;
                itemData.itemProperty = data.itemProperty;
                itemData.itemHistory = data.itemHistory;
                data.isStoring = true;
            }
        }
        numberInnerUI.text = "Soá löôïng noäi coâng: "+ transform.childCount.ToString();
    }
    //Them cac item da khoi tao vao trong list
    public void AddInventoryItems(InventoryItems inventoryItems)
    {
        inventoryItemsList ??= new List<InventoryItems>();
        inventoryItemsList.Add(inventoryItems);
    }
    //Xu ly khi click vao item
    public void OnInventoryItemsClick(InventoryItems inventoryItems)
    {
        if(inventoryItems != currentItem)
        {
            SetItemValue(inventoryItems);
            currentItem = inventoryItems;
           
        }
    }
    //Khi item duoc click gan cac gia tri can thiet vao cac UI
    public void SetItemValue(InventoryItems inventoryItems)
    {
        nameTextUI.text = inventoryItems.itemName;
        levelTextUI.text = inventoryItems.itemLevel + "/36";
        originTextUI.text = inventoryItems.itemOrigin;
        propertyTextUI.text = inventoryItems.itemProperty;
        historyTextUI.text = inventoryItems.itemHistory;
        imageUI.sprite = inventoryItems.itemImage;

        indexItemClicked = inventoryItems.transform.GetSiblingIndex();
    }
    //Toggle on equip UI
    public void OpenEquipUI()
    {
        inventoryEquipmentUI.SetActive(true);
    }
    //Toggle off equip UI
    public void CloseEquipUI()
    {
        inventoryEquipmentUI.SetActive(false);
    }
}
