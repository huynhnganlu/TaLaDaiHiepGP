using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThienDinhCong : ShopDataAbstract
{
    public override void ItemEffect()
    {
        float rate = Random.value;
        if(rate >= 0.95)
        {
            if(MyCharacterController.Instance.currentHealth + 5 > MyCharacterController.Instance.maxHealth)
            {
                MyCharacterController.Instance.SetHealth(MyCharacterController.Instance.maxHealth);
            }
            else
            {
                MyCharacterController.Instance.SetHealth(MyCharacterController.Instance.currentHealth + 5);
            }
        }
    }

   
}
