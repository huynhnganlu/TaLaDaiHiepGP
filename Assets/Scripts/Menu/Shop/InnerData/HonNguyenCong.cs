using System.Collections;
using UnityEngine;
public class HonNguyenCong : ShopDataAbstract
{
    private bool first = false;
    private float lastCall, cooldown = 20f;

    public override void ItemEffect()
    {
        if(MyCharacterController.Instance.currentHealth < MyCharacterController.Instance.maxHealth)
        {
            if (first == false)
            {
                TakeEffect();
                first = true;
                lastCall = Time.time;
            }
            if (Time.time - lastCall > cooldown)
            {
                lastCall = Time.time;
                TakeEffect();
            }

        }

    }

    private IEnumerator HealthPerSecond(int time)
    {
        while (time < 3)
        {
            //Neu day mau thi break
            if (MyCharacterController.Instance.currentHealth + (int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * ((5 + 1 * itemLevel) / 3f)) >= MyCharacterController.Instance.maxHealth)
            {
                MyCharacterController.Instance.currentHealth = MyCharacterController.Instance.maxHealth;
                MyCharacterController.Instance.healthBar.value = MyCharacterController.Instance.maxHealth;
                MyCharacterController.Instance.healthText.text = MyCharacterController.Instance.maxHealth.ToString();
                break;
            } 
            //Tiep tuc hoi mau neu chua day
            else
            {
                MyCharacterController.Instance.currentHealth += (int)System.Math.Round(((MyCharacterController.Instance.maxHealth / 100f) * ((5 + 1 * itemLevel) / 3f)));
                MyCharacterController.Instance.healthBar.value = MyCharacterController.Instance.currentHealth;
                MyCharacterController.Instance.healthText.text = MyCharacterController.Instance.currentHealth.ToString();
            }
            time++;
            yield return new WaitForSeconds(1);
        }
    }
    private void TakeEffect()
    {
        float randomValue = Random.value;
        float effectRate = 0.2f;
        if (randomValue <= effectRate)
        {
            StartCoroutine(HealthPerSecond(0));
        }
    }
}
