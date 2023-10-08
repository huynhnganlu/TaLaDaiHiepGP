using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToaVongCong : ShopDataAbstract
{
    public override void ItemEffect()
    {
        MyCharacterController.Instance.defense += (10 + 2 * itemLevel);
    }
}
