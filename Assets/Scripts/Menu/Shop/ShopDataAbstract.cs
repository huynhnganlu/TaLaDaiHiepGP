using UnityEngine;

public abstract class ShopDataAbstract : MonoBehaviour
{
    public int itemCost, itemID, itemHP, itemMP, itemLevel;
    public Sprite itemImage;
    public string itemName, itemOrigin, itemProperty, itemHistory, itemType, itemElemental;
    [TextArea]
    public string itemEffect, itemEffectEng;

    public abstract void ItemEffect();
}
