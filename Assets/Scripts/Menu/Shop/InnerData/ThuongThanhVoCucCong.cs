using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ThuongThanhVoCucCong : ShopDataAbstract
{
    public override void ItemEffect()
    {
        if (MyCharacterController.Instance.currentHealth <= (int)(MyCharacterController.Instance.maxHealth / 100) * 30)
        {
           
             StartCoroutine(HealthPerSecond(0));
        }
    }

    private IEnumerator HealthPerSecond(int time)
    {
        while(time < 6)
        {
            if (MyCharacterController.Instance.currentHealth + (int)(MyCharacterController.Instance.maxHealth / 100) * 5 > MyCharacterController.Instance.maxHealth)
            {
                MyCharacterController.Instance.currentHealth = MyCharacterController.Instance.maxHealth;
                MyCharacterController.Instance.healthBar.value = MyCharacterController.Instance.maxHealth;
                MyCharacterController.Instance.healthText.text = MyCharacterController.Instance.maxHealth.ToString();
                break;
            }
            else
            {
                MyCharacterController.Instance.currentHealth += (int)(MyCharacterController.Instance.maxHealth / 100) * 5;
                MyCharacterController.Instance.healthBar.value = MyCharacterController.Instance.currentHealth;
                MyCharacterController.Instance.healthText.text = MyCharacterController.Instance.currentHealth.ToString();

            }
            time++;
            yield return new WaitForSeconds(1);
        }
    }
}
