using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopController : MonoBehaviour
{

    public int money;
    public TextMeshProUGUI moneyText;
    [SerializeField]
    private InnerHolder innerHolder;
    public ShopTemplate[] shopTemplates;
    private ShopDataAbstract[] itemAddedArray = new ShopDataAbstract[6];
    private JsonPlayerPrefs shopPrefs;
    //Singleton variables
    public static ShopController Instance;
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
        moneyText.text = money.ToString();
        LoadShopItemsData();
        CheckItems();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shopPrefs.DeleteAll();
            shopPrefs.Save();
        }
    }
    //Them tien
    public void AddMoney()
    {
        money++;
        moneyText.text = money.ToString();
    }

    //Kiem tra cac item co the mua
    public void CheckItems()
    {
        for(int i = 0; i < itemAddedArray.Length; i++)
        {
            if(money >= itemAddedArray[i].itemCost && shopPrefs.HasKey(itemAddedArray[i].itemID.ToString()) == false)
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
        shopPrefs.Save();
        CheckItems(); 
    }
    
    //Load du lieu
    public void LoadShopItemsData()
    {
        GameObject[] shuffled = innerHolder.listInner.OrderBy(n => Guid.NewGuid()).ToArray();
        for (int i = 0;i <= 5; i++)
        {
            ShopDataAbstract data = shuffled[i].GetComponent<ShopDataAbstract>();
            shopTemplates[i].id = data.itemID;
            shopTemplates[i].cost.text = data.itemCost.ToString();
            shopTemplates[i].itemImage.sprite = data.itemImage;
            string textShow = "<b>Vaän haønh kích hoaït:</b>\nKhí huyeát +" + data.itemHP + "\nNoäi löïc +" + data.itemMP + "\n<b>Ñaëc hieäu:</b>\n" + data.itemEffect;
            shopTemplates[i].textTooltip = textShow;
            itemAddedArray[i] = data;
        }
        CheckItems();
    }
}
