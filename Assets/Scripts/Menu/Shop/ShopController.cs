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
    private readonly ShopDataAbstract[] itemAddedArray = new ShopDataAbstract[6];
    public JsonPlayerPrefs shopPrefs;
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

        shopPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/ShopData.json");
    }
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = money.ToString();
        LoadShopItemsData();
        CheckItems();
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
    
    //Load du lieu tu script object
    public void LoadShopItemsData()
    {
        GameObject[] shuffled = innerHolder.listInner.OrderBy(n => Guid.NewGuid()).ToArray();
        for (int i = 0;i <= 5; i++)
        {
            shopTemplates[i].id = shuffled[i].GetComponent<ShopDataAbstract>().itemID;
            shopTemplates[i].cost.text = shuffled[i].GetComponent<ShopDataAbstract>().itemCost.ToString();
            shopTemplates[i].itemImage.sprite = shuffled[i].GetComponent<ShopDataAbstract>().itemImage;
            itemAddedArray[i] = shuffled[i].GetComponent<ShopDataAbstract>();
        }
    }
}
