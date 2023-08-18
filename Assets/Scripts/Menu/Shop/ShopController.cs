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
    public ShopItemData[] shopItemsData;
    public ShopTemplate[] shopTemplates;

    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = money.ToString();
        CheckItems();
        LoadShopItemsData();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        for(int i = 0; i < shopItemsData.Length; i++)
        {
            if(money >= shopItemsData[i].itemCost && shopItemsData[i].isBuying == false)
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
        money -= shopItemsData[button].itemCost;
        shopItemsData[button].isBuying = true;
        moneyText.text = money.ToString();
        CheckItems();
    }

    //Load du lieu tu script object
    public void LoadShopItemsData()
    {
        ShopItemData[] shuffled = shopItemsData.OrderBy(n => Guid.NewGuid()).ToArray();
        for (int i = 0;i <= 7; i++)
        {
            shopTemplates[i].cost.text = shuffled[i].itemCost.ToString();
            shopTemplates[i].itemImage.sprite = shuffled[i].itemImage;
        }
    }
}
