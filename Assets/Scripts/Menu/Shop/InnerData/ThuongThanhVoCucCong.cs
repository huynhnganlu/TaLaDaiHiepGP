using System.Collections;
using UnityEngine;

public class ThuongThanhVoCucCong : ShopDataAbstract
{
    private int count = 0;
    private bool isHealing = false;

    public override void ItemEffect()
    {
        if (MyCharacterController.Instance.currentHealth <= (int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * (30 + 2 * itemLevel)) && count < 2 && !isHealing && MyCharacterController.Instance.currentHealth > 0)
        {
            count++;
            StartCoroutine(HealthPerSecond(0));
        }
    }


    private IEnumerator HealthPerSecond(int time)
    {
        isHealing = true;
        while (time < 6)
        {
            if (MyCharacterController.Instance.currentHealth > 0)
            {
                //Neu day mau thi break
                if (MyCharacterController.Instance.currentHealth + (int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * ((30 + 2 * itemLevel) / 6f)) >= MyCharacterController.Instance.maxHealth)
                {
                    MyCharacterController.Instance.SetHealth(MyCharacterController.Instance.maxHealth);
                    break;
                }
                //Tiep tuc hoi mau neu chua day
                else
                {
                    MyCharacterController.Instance.SetHealth(MyCharacterController.Instance.currentHealth + ((int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * ((30 + 2 * itemLevel) / 6f))));
                }
                time++;
                if (time == 5)
                    isHealing = false;
                yield return new WaitForSeconds(1);
            }
            else
                break;
        }

    }
}
