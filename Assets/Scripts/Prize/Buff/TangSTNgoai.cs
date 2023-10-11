using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangSTNgoai : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.externalDamage += 10;
    }

 
}
