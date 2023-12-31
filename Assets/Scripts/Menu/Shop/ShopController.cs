using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
public class ShopController : MonoBehaviour
{
    private int money;
    public TextMeshProUGUI moneyText;
    [SerializeField]
    private InnerHolder innerHolder;
    public ShopTemplate[] shopTemplates;
    private ShopDataAbstract[] itemAddedArray = new ShopDataAbstract[6];
    private JsonPlayerPrefs shopPrefs, characterPrefs;
    //Singleton variables
    public static ShopController Instance;
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

        if (!characterPrefs.HasKey("firstShop"))
            MenuController.Instance.SetFirstShop("true");

    }

    private void OnEnable()
    {
        money = characterPrefs.GetInt("money");
        moneyText.text = money.ToString();

        if (characterPrefs.GetString("firstShop").Equals("true"))
        {
            RandomShopItemsData();
            MenuController.Instance.SetFirstShop("false");
        }

        if (MenuController.Instance.timePassed.TotalMinutes >= 60)
        {
            RandomShopItemsData();
        }

        LoadShopItemsData();
        ShopTooltipText();
    }

    //Kiem tra cac item co the mua
    public void CheckItems()
    {
        for (int i = 0; i < itemAddedArray.Length; i++)
        {
            if (money >= itemAddedArray[i].itemCost && shopPrefs.HasKey(itemAddedArray[i].itemID.ToString()) == false)
            {
                shopTemplates[i].gameObject.GetComponentInChildren<Button>().interactable = true;
            }
            else
            {
                shopTemplates[i].gameObject.GetComponentInChildren<Button>().interactable = false;
            }

        }
    }
    //Mua item
    public void BuyItem(int button)
    {
        money -= innerHolder.listInner[shopTemplates[button].id].GetComponent<ShopDataAbstract>().itemCost;
        moneyText.text = money.ToString();
        shopPrefs.SetInt(shopTemplates[button].id.ToString(), 0);
        characterPrefs.SetInt("money", money);
        characterPrefs.Save();
        shopPrefs.Save();
        CheckItems();
    }
    public void RandomShopItemsData()
    {
        GameObject[] shuffled = innerHolder.listInner.OrderBy(n => Guid.NewGuid()).ToArray();
        for(int i = 0; i <= 5; i++)
        {
            ShopDataAbstract data = shuffled[i].GetComponent<ShopDataAbstract>();
            shopPrefs.SetInt("shop" + i, data.itemID);
        }
        shopPrefs.Save();
    }
    //Load du lieu
    public void LoadShopItemsData()
    {
        for (int i = 0; i <= 5; i++)
        {
            ShopDataAbstract data = innerHolder.listInner[shopPrefs.GetInt("shop"+i)].GetComponent<ShopDataAbstract>();
            shopTemplates[i].id = data.itemID;
            shopTemplates[i].cost.text = data.itemCost.ToString();
            shopTemplates[i].itemImage.sprite = data.itemImage;
            itemAddedArray[i] = data;
        }
        CheckItems();
    }
    private void ShopTooltipText()
    {
        string textShow;
        for (int i = 0;i <= 5; i++)
        {
            ShopDataAbstract data = innerHolder.listInner[shopTemplates[i].id].GetComponent<ShopDataAbstract>();
            if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
                textShow = "<b>Type:</b>\n" + data.itemPropertyEng + "\n<b>Inner points:</b>\nHealth +" + data.itemHP + "\nMana +" + data.itemMP + "\n<b>Inner effect:</b>\n" + data.itemEffectEng;
            else
                textShow = "<b>Thuo�c t�nh:</b>\n" + data.itemProperty + "\n<b>Va�n ha�nh k�ch hoa�t:</b>\nKh� huye�t +" + data.itemHP + "\nNo�i l��c +" + data.itemMP + "\n<b>�a�c hie�u:</b>\n" + data.itemEffect;
            shopTemplates[i].textTooltip = textShow;
        }
    }
    public void RerollShopItemsData()
    {
        if(money - 100 >= 0)
        {
            money -= 100;
            characterPrefs.SetInt("money", money);
            characterPrefs.Save();
            moneyText.text = money.ToString();
            RandomShopItemsData();
            LoadShopItemsData();
        }
      
    }
}
