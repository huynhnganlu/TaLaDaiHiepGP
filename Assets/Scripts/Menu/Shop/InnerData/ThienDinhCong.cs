using UnityEngine;

public class ThienDinhCong : ShopDataAbstract
{
    public override void ItemEffect()
    {
        if (itemLevel >= 0 && (MyCharacterController.Instance.currentHealth < MyCharacterController.Instance.maxHealth))
        {
            float rate = Random.value;
            if (rate >= 0.9)
            {
                if (MyCharacterController.Instance.currentHealth + (int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * (2 + 2 * itemLevel)) >= MyCharacterController.Instance.maxHealth)
                {
                    MyCharacterController.Instance.SetHealth(MyCharacterController.Instance.maxHealth);
                }
                else if (MyCharacterController.Instance.currentHealth + (int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * (2 + 2 * itemLevel)) < MyCharacterController.Instance.maxHealth)
                {
                    MyCharacterController.Instance.SetHealth(MyCharacterController.Instance.currentHealth + (int)System.Math.Round((MyCharacterController.Instance.maxHealth / 100f) * (2 + 2 * itemLevel)));
                }
            }
        }

    }


}
