using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangCritRate : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.critRate += 10;
    }
   
}
