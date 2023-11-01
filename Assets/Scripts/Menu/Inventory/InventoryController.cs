using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //process item variable
    public GameObject inventoryItem;
    public List<InventoryItems> inventoryItemsList;
    private InventoryItems currentItem;
    [SerializeField]
    private GameObject innerParent;

    //item data holder variable
    [SerializeField]
    private TextMeshProUGUI nameTextUI, levelTextUI, originTextUI, propertyTextUI, historyTextUI, numberInnerUI, effectTextUI;
    [SerializeField]
    private Image imageUI;
    [SerializeField]
    private GameObject itemDetail, itemDetailParent;

    //Toggle equip item variables
    [SerializeField]
    private GameObject inventoryEquipmentUI;

    //Singleton
    public static InventoryController Instance;

    //Inner holder 
    public InnerHolder innerHolder;
    private JsonPlayerPrefs shopPrefs, characterPrefs;
    private int innerCount, dao, levelCost;
    [SerializeField]
    private TextMeshProUGUI daoText;

    //Equip items variables
    public int indexItemClicked;
    private List<int> equipedInventoryItemsList;
    [SerializeField]
    private GameObject[] equipSlot;

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

        shopPrefs = MenuController.Instance.shopPrefs;
        characterPrefs = MenuController.Instance.characterPrefs;
    }

    private void OnEnable()
    {
        GetEquipedList();
        ResetInventoryItems();
        GetBoughtInner();
        if (innerParent.transform.childCount != 0)
            SetItemValue(currentItem);
        else
            indexItemClicked = -1;
       
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
                InventoryItems inventoryItemData = item.GetComponent<InventoryItems>();
                if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
                {
                    item.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemNameEng;
                    inventoryItemData.itemName = itemData.itemNameEng;
                    inventoryItemData.itemOrigin = itemData.itemOriginEng;
                    inventoryItemData.itemProperty = itemData.itemPropertyEng;
                    inventoryItemData.itemEffect = itemData.itemEffectEng;

                }
                else
                {
                    item.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemName;
                    inventoryItemData.itemName = itemData.itemName;
                    inventoryItemData.itemOrigin = itemData.itemOrigin;
                    inventoryItemData.itemProperty = itemData.itemProperty;
                    inventoryItemData.itemEffect = itemData.itemEffect;

                }
                inventoryItemData.itemID = itemData.itemID;               
                inventoryItemData.itemLevel = shopPrefs.GetInt(itemData.itemID.ToString());
                inventoryItemData.itemImage = itemData.itemImage;         
                inventoryItemData.itemHistory = itemData.itemHistory;
                inventoryItemData.itemHP = itemData.itemHP;
                inventoryItemData.itemMP = itemData.itemMP;
                if (firstItem)
                {
                    currentItem = inventoryItemData;
                    firstItem = false;
                }
                innerCount++;
                inventoryItemsList.Add(inventoryItemData);
            }
        }
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
            numberInnerUI.text = "Total inner: " + innerCount.ToString();
        else
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
            currentItem = inventoryItems;
            SetItemValue(inventoryItems);
        }
    }
    //Khi item duoc click gan cac gia tri can thiet vao cac UI
    public void SetItemValue(InventoryItems inventoryItems)
    {
        ResetItemDetail();
        nameTextUI.text = inventoryItems.itemName;
        levelTextUI.text = inventoryItems.itemLevel + "/5";
        originTextUI.text = inventoryItems.itemOrigin;
        propertyTextUI.text = inventoryItems.itemProperty;
        historyTextUI.text = inventoryItems.itemHistory;
        imageUI.sprite = inventoryItems.itemImage;
        imageUI.color = new Color(1f, 1f, 1f, 1f);
        indexItemClicked = inventoryItems.transform.GetSiblingIndex();
       
        GameObject hpDetail = Instantiate(itemDetail, itemDetailParent.transform);
        GameObject mpDetail = Instantiate(itemDetail, itemDetailParent.transform);
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            ItemDetail(hpDetail, "Health", inventoryItems.itemHP);
            ItemDetail(mpDetail, "Mana", inventoryItems.itemMP);
        }
        else
        {
            ItemDetail(hpDetail, "Khí huyeát", inventoryItems.itemHP);
            ItemDetail(mpDetail, "Noäi löïc", inventoryItems.itemMP);
        }
        effectTextUI.text = inventoryItems.itemEffect;
        dao = characterPrefs.GetInt("dao");
        if (inventoryItems.itemLevel == 0)
            levelCost = 500;
        else
            levelCost = 500 * (inventoryItems.itemLevel + 1);
        daoText.text = dao.ToString() + "/" + levelCost;
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
    public void LevelUpInner()
    {
        if(indexItemClicked != -1)
        {
            if(currentItem.itemLevel < 5)
            {             
                if(dao - levelCost >= 0)
                {
                    currentItem.itemLevel += 1;
                    shopPrefs.SetInt(currentItem.itemID.ToString(), currentItem.itemLevel);
                    characterPrefs.SetInt("dao", dao - levelCost);
                    shopPrefs.Save();
                    characterPrefs.Save();
                    SetItemValue(currentItem);
                    AudioManager.Instance.PlaySE("LevelUpSE");
                }
            }
        }
    }
    #endregion

    #region equip item
    //Toggle on equip UI
    public void OpenEquipUI()
    {
        foreach (GameObject go in equipSlot)
        {
            GetEquipedItem(go);
        }
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
        if (inventoryItemsList.Count > 0)
        {
            EquipItem(index);
        }
    }

    #endregion

}
