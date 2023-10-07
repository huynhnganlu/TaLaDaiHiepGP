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
    private TextMeshProUGUI nameTextUI, levelTextUI, originTextUI, propertyTextUI, historyTextUI, numberInnerUI, effectTextUI;
    [SerializeField]
    private Image imageUI;
    [SerializeField]
    private GameObject itemDetail, itemDetailParent;
   

    //equip item variable
    public int indexItemClicked;

    //Toggle equip item variables
    [SerializeField]
    private GameObject inventoryEquipmentUI;

    //Singleton
    public static InventoryController Instance;

    //Inner holder 
    public InnerHolder innerHolder;
    private JsonPlayerPrefs shopPrefs;
    private int innerCount;

    //Equip items variables
    private List<int> equipedInventoryItemsList;
    [SerializeField]
    private GameObject[] equipSlot;
  
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

        shopPrefs = MenuController.Instance.shopPrefs;

    }


    // Start is called before the first frame update
    void Start()
    {
        indexItemClicked = 0;
    }

    private void OnEnable()
    {
        GetEquipedList();
        ResetInventoryItems();
        GetBoughtInner();
        if(innerParent.transform.childCount != 0)
            SetItemValue(defaultItem);
        foreach(GameObject go in equipSlot)
        {
            GetEquipedItem(go);
        }
    }

    #region inventory control
    //Danh sach cac noi cong da mua & khoi tao UI & truyen du lieu cua cac Inventory Item
    public void GetBoughtInner()
    {
        inventoryItemsList.Clear();
        innerCount = 0;
        bool firstItem = true;
        foreach (GameObject data in innerHolder.listInner)
        {
            ShopDataAbstract itemData = data.GetComponent<ShopDataAbstract>();
            if (shopPrefs.HasKey(itemData.itemID.ToString()) == true)
            {
                GameObject item = Instantiate(inventoryItem, innerParent.transform);
                item.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemName;
                InventoryItems inventoryItemData = item.GetComponent<InventoryItems>();
                inventoryItemData.itemID = itemData.itemID;
                inventoryItemData.itemName = itemData.itemName;
                inventoryItemData.itemLevel = shopPrefs.GetInt(itemData.itemID.ToString());
                inventoryItemData.itemImage = itemData.itemImage;
                inventoryItemData.itemOrigin = itemData.itemOrigin;
                inventoryItemData.itemProperty = itemData.itemProperty;
                inventoryItemData.itemHistory = itemData.itemHistory;
                inventoryItemData.itemHP = itemData.itemHP;
                inventoryItemData.itemMP = itemData.itemMP;
                inventoryItemData.itemEffect = itemData.itemEffect;
                if (firstItem)
                {
                    defaultItem = inventoryItemData;
                    firstItem = false;
                }
                innerCount++;
                inventoryItemsList.Add(inventoryItemData);
            }
        }
        numberInnerUI.text = "Soá löôïng noäi coâng: " + innerCount.ToString();
    }
    //Xoa nhung inventory items hien co
    public void ResetInventoryItems()
    {
        foreach (Transform child in innerParent.transform)
        {
            Destroy(child.gameObject);
        }
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
        if (inventoryItems != currentItem)
        {
            SetItemValue(inventoryItems);
            currentItem = inventoryItems;
        }
    }
    //Khi item duoc click gan cac gia tri can thiet vao cac UI
    public void SetItemValue(InventoryItems inventoryItems)
    {
        ResetItemDetail();
        nameTextUI.text = inventoryItems.itemName;
        levelTextUI.text = inventoryItems.itemLevel + "/36";
        originTextUI.text = inventoryItems.itemOrigin;
        propertyTextUI.text = inventoryItems.itemProperty;
        historyTextUI.text = inventoryItems.itemHistory;
        imageUI.sprite = inventoryItems.itemImage;
        imageUI.color = new Color(1f, 1f, 1f, 1f);
        indexItemClicked = inventoryItems.transform.GetSiblingIndex();
        GameObject hpDetail = Instantiate(itemDetail, itemDetailParent.transform);
        ItemDetail(hpDetail, "Khí huyeát", inventoryItems.itemHP);
        GameObject mpDetail = Instantiate(itemDetail, itemDetailParent.transform);
        ItemDetail(mpDetail, "Noäi löïc", inventoryItems.itemMP);
        effectTextUI.text = inventoryItems.itemEffect;
    }
    public void ItemDetail(GameObject item, string valueName, int value)
    {
        TextMeshProUGUI[] detail = item.GetComponentsInChildren<TextMeshProUGUI>();
        detail[0].text = valueName;
        detail[1].text = value.ToString();
    }
    public void ResetItemDetail()
    {
        foreach (Transform child in itemDetailParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    #endregion

    #region equip item
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

    //Ham trang bi item
    public void EquipItem(int slot)
    {
        InventoryItems equipItem = inventoryItemsList[indexItemClicked];
        bool alreadyAttached = false;
        int index = 0;
        for (int i = 0; i < equipedInventoryItemsList.Count; i++)
        {
            if (equipedInventoryItemsList[i] != -1 && equipedInventoryItemsList[i] == equipItem.itemID && (i != slot))
            {
                alreadyAttached = true;
                index = i;
            }
        }

        if (alreadyAttached == true)
            equipedInventoryItemsList[index] = -1;
        equipedInventoryItemsList[slot] = equipItem.itemID;
        SaveEquipedData();
        CharacterUIController.Instance.CreateEquipedInventoryItemsUI();
    }
    //Cap nhat danh sach equiped do moi lan enable no se tro ve mac dinh
    public void GetEquipedList()
    {
        equipedInventoryItemsList ??= new List<int>
            {
                -1,
                -1,
                -1
            };
        if (shopPrefs.HasKey("slot1"))
        {
            for (int i = 0; i <= 2; i++)
            {
                if (shopPrefs.GetInt("slot" + i) == -1)
                    equipedInventoryItemsList[i] = -1;
                else
                {
                    ShopDataAbstract data = innerHolder.listInner[shopPrefs.GetInt("slot" + i)].GetComponent<ShopDataAbstract>();
                    equipedInventoryItemsList[i] = data.itemID;
                }
            }
        }
    }
    //Luu thong tin trang bi noi cong vao shopPref
    public void SaveEquipedData()
    {
        for (int i = 0; i < equipedInventoryItemsList.Count; i++)
        {
            if (equipedInventoryItemsList[i] != -1)
            {
                shopPrefs.SetInt("slot" + i, equipedInventoryItemsList[i]);
            }
            else
            {
                shopPrefs.SetInt("slot" + i, -1);
            }
        }
        shopPrefs.Save();
    }
    public void GetEquipedItem(GameObject equipSlot)
    {
        Image childImage = equipSlot.transform.GetChild(0).GetComponent<Image>();
        if (shopPrefs.HasKey("slot" + equipSlot.transform.GetSiblingIndex()))
        {
            if (shopPrefs.GetInt("slot" + equipSlot.transform.GetSiblingIndex()) != -1)
            {
                childImage.sprite = innerHolder.listInner[shopPrefs.GetInt("slot" + equipSlot.transform.GetSiblingIndex())].GetComponent<ShopDataAbstract>().itemImage;
                childImage.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                childImage.sprite = null;
                childImage.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
    public void EquipItemSlotProcess(int index)
    {
        CloseEquipUI();
        if(inventoryItemsList.Count > 0)
        {
            EquipItem(index);
        }
    }
  
    #endregion

}
