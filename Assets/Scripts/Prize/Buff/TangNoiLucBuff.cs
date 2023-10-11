using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangNoiLucBuff : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.maxShield += 10;
        MyCharacterController.Instance.shieldBar.maxValue += 10;
    }


}
