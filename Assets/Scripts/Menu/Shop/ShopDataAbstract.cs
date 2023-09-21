using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopDataAbstract : MonoBehaviour
{
    public int itemCost, itemID;
    public Sprite itemImage;
    public string itemName, itemOrigin, itemProperty, itemHistory;

    public abstract void ItemEffect();
}
