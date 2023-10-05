using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangKhiHuyetBuff : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.maxHealth += 10;
        MyCharacterController.Instance.healthBar.maxValue += 10;
        MyCharacterController.Instance.healthText.text = MyCharacterController.Instance.maxHealth.ToString();


    }


}
