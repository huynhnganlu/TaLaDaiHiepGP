using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangPhongNgu : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.defense += 10;
    }

   
}
