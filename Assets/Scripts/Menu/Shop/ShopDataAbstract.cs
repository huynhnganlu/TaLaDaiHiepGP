using UnityEngine;

public abstract class ShopDataAbstract : MonoBehaviour
{
    public int itemCost, itemID, itemHP, itemMP, itemLevel;
    public Sprite itemImage;
    public string itemName, itemOrigin, itemProperty, itemHistory, itemType;
    [TextArea]
    public string itemEffect;

    public abstract void ItemEffect();
}
