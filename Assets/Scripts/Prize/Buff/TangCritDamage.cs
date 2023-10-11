using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangCritDamage : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.critDamage += 10;
    }
   
}
