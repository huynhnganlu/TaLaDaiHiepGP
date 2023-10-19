public class VoVongThanCong : ShopDataAbstract
{
    public override void ItemEffect()
    {
        MyCharacterController.Instance.internalDamage += (int)System.Math.Round(MyCharacterController.Instance.internalDamage / 100f * (10 + 2 * itemLevel));
        MyCharacterController.Instance.externalDamage += (int)System.Math.Round(MyCharacterController.Instance.externalDamage / 100f * (10 + 2 * itemLevel));
        MyCharacterController.Instance.critDamage += (int)System.Math.Round(MyCharacterController.Instance.critDamage / 100f * (10 + 2 * itemLevel));
    }
}
