using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoiNoiLuc : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.mpRegen += 1;
    }
  
}
