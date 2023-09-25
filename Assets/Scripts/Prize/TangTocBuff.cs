using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangTocBuff : PrizeAbstract
{ 

    public override void ProcessPrize()
    {
        MyCharacterController.Instance.speed += 1f;
    }

   
}
