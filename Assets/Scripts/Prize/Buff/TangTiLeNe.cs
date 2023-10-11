using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangTiLeNe : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.evade += 1;
    }
}
