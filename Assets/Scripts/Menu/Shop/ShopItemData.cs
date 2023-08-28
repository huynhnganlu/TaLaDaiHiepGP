using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopData", menuName = "Scriptable Objects/New Shop Item", order = 2)]
public class ShopItemData : ScriptableObject
{ 
     
    public int itemCost;
    public Sprite itemImage;
    public string itemDescription;
    public bool isBuying, isEquip = false;
    
}
