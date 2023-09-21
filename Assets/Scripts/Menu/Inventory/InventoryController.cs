using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //process item variable
    public GameObject inventoryItem;
    public List<InventoryItems> inventoryItemsList;
    private InventoryItems currentItem, defaultItem;
    [SerializeField]
    private GameObject innerParent;

    //item data holder variable
    [SerializeField]
    private TextMeshProUGUI nameTextUI, levelTextUI, originTextUI, propertyTextUI, historyTextUI, numberInnerUI;
    [SerializeField]
    private Image imageUI;

    //equip item variable
    public int indexItemClicked;

    //Toggle equip item variables
    [SerializeField]
    private GameObject inventoryEquipmentUI;
    [SerializeField]
    private Button closeInventoryUIButton, openInventoryUIButton;

    //Singleton
    public static InventoryController Instance;

    //Inner holder 
    [SerializeField]
    private InnerHolder innerHolder;
    public JsonPlayerPrefs shopPrefs;

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

        shopPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/ShopData.json");

    }


    // Start is called before the first frame update
    void Start()
    {
        indexItemClicked = 0;
      /*  currentItem = transform.GetChild(0).GetComponent<InventoryItems>();
        SetItemValue(currentItem);*/
        openInventoryUIButton.onClick.AddListener(() =>
        {
            OpenEquipUI();
        });
        closeInventoryUIButton.onClick.AddListener(() =>
        {
            CloseEquipUI();
        });
        
    }

    private void OnEnable()
    {
        GetBoughtInner();
        SetItemValue(defaultItem);
    }


    //Danh sach cac noi cong da mua & khoi tao UI & truyen du lieu cua cac Inventory Item
    public void GetBoughtInner()
    {
        bool firstItem = true;
        foreach (GameObject data in innerHolder.listInner)
        {
            ShopDataAbstract itemData = data.GetComponent<ShopDataAbstract>();
            if (shopPrefs.HasKey(itemData.itemID.ToString()) == true)
            {
                GameObject item = Instantiate(inventoryItem);
                item.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemName;
                item.transform.SetParent(innerParent.transform);
                InventoryItems inventoryItemData = item.GetComponent<InventoryItems>();
                inventoryItemData.itemName = itemData.itemName;
                inventoryItemData.itemLevel = shopPrefs.GetInt(itemData.itemID.ToString());
                inventoryItemData.itemImage = itemData.itemImage;
                inventoryItemData.itemOrigin = itemData.itemOrigin;
                inventoryItemData.itemProperty = itemData.itemProperty;
                inventoryItemData.itemHistory = itemData.itemHistory;
                if (firstItem)
                {
                    defaultItem = inventoryItemData;
                    firstItem = false;
                }                   
            }
        }
        numberInnerUI.text = "Soá löôïng noäi coâng: "+  innerParent.transform.childCount.ToString();
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
