using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToaVongCong : ShopDataAbstract
{
  
    public override void ItemEffect()
    {
        Debug.Log("Toa vong");
        MyCharacterController.Instance.internalDefense += 10;
        MyCharacterController.Instance.externalDefense += 10;

    }
}
