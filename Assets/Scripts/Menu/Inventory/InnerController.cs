using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class InnerController : MonoBehaviour
{
    public GameObject inner;
    public GameObject innerParent;
    public ShopController shopController;
    private List<ShopItemData> shopItemData;
    // Start is called before the first frame update
    void Start()
    {
        shopItemData = new List<ShopItemData>();
        getBoughtInner();
        createInnerUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Danh sach cac noi cong da mua
    void getBoughtInner()
    {
        foreach(ShopItemData data in shopController.shopItemsData)
        {
            if(data.isBuying == true)
            {
                shopItemData.Add(data);
            }
        }
    }
    //Khoi tao 2d UI Element inner dua tren data
    void createInnerUI()
    {
        for(int i = 0; i < shopItemData.Count; i++)
        {
            GameObject myInner = Instantiate(inner, transform.position, transform.rotation);
            myInner.GetComponentInChildren<TextMeshProUGUI>().text = shopItemData[i].itemDescription;
            myInner.transform.SetParent(innerParent.transform, false);
        }
    }
}
