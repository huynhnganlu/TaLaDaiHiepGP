using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangSTNoi : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.internalDamage += 10;
    }
     
  
}
