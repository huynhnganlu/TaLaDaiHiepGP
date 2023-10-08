using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinhNgocThanCong : ShopDataAbstract
{

    public override void ItemEffect()
    {
        MyCharacterController.Instance.evade += MyCharacterController.Instance.evade / 100 * (15 + 1 * itemLevel);
        MyCharacterController.Instance.internalDamage += 20;
        MyCharacterController.Instance.externalDamage += 20;
    }

   
}
