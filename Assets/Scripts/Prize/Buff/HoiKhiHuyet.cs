using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoiKhiHuyet : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.hpRegen += 1;
    }
 
}
