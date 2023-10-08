using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ThuongThanhVoCucCong : ShopDataAbstract
{
    private int count = 0;
    private bool isHealing = false;
    public override void ItemEffect()
    {
        if (MyCharacterController.Instance.currentHealth <= (int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * (30 + 2 * itemLevel)))
        {
             if(count < 2 && isHealing == false)
             {
                Debug.Log(count++);
                StartCoroutine(HealthPerSecond(0));
                Debug.Log(isHealing);

            }
        }
    }


    private IEnumerator HealthPerSecond(int time)
    {
        isHealing = true;
        while(time < 6)
        {
            //Neu day mau thi break
            if (MyCharacterController.Instance.currentHealth + (int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * ((30 + 2 * itemLevel) / 6f)) >= MyCharacterController.Instance.maxHealth)
            {
                MyCharacterController.Instance.currentHealth = MyCharacterController.Instance.maxHealth;
                MyCharacterController.Instance.healthBar.value = MyCharacterController.Instance.maxHealth;
                MyCharacterController.Instance.healthText.text = MyCharacterController.Instance.maxHealth.ToString();
                break;
            }
            //Tiep tuc hoi mau neu chua day
            else
            {
                MyCharacterController.Instance.currentHealth += (int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * ((30 + 2 * itemLevel) / 6f));
                MyCharacterController.Instance.healthBar.value = MyCharacterController.Instance.currentHealth;
                MyCharacterController.Instance.healthText.text = MyCharacterController.Instance.currentHealth.ToString();

            }
            time++;
            if (time == 5)
                isHealing = false;
            yield return new WaitForSeconds(1);
           
        }
       
    }
}
