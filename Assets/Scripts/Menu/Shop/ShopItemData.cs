using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopData", menuName = "Scriptable Objects/New Shop Item", order = 2)]
public class ShopItemData : ScriptableObject
{ 
     
    public int itemCost, itemLevel;
    public Sprite itemImage;
    public string itemName, itemOrigin, itemProperty, itemHistory;
    public bool isBuying, isEquip, isStoring;
    
}
