using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoVongThanCong : ShopDataAbstract
{
    public override void ItemEffect()
    {
        MyCharacterController.Instance.internalDamage += (int)MyCharacterController.Instance.internalDamage / 100 * (10 + 2 * itemLevel);
        MyCharacterController.Instance.externalDamage += (int)MyCharacterController.Instance.externalDamage / 100 * (10 + 2 * itemLevel);
        MyCharacterController.Instance.critDamage += (int)MyCharacterController.Instance.critDamage / 100 * (10 + 2 * itemLevel);
    }
}
