using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerData : MonoBehaviour
{
    [SerializeField]
    private ShopItemData[] shopItemDatas;
     
    // Start is called before the first frame update
    void Start()
    {
        checkEquipInner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Kiem tra cac Inner
    void checkEquipInner()
    {
        foreach(ShopItemData shopItem in shopItemDatas)
        {
            if(shopItem.isEquip == true)
            {
                Debug.Log(shopItem.itemDescription);
            }
        }
    }
}
