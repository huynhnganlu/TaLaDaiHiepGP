using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoVongThanCong : ShopDataAbstract
{
    public override void ItemEffect()
    {
        MyCharacterController.Instance.maxHealth += 100;
        MyCharacterController.Instance.maxShield += 100;
        MyCharacterController.Instance.externalDamage += 50;
        MyCharacterController.Instance.internalDamage += 50;
        MyCharacterController.Instance.skipExternalDefense += 50;
        MyCharacterController.Instance.skipInternalDefense += 50;
    }
}
