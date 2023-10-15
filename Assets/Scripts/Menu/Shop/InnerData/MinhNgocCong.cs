public class MinhNgocCong : ShopDataAbstract
{
    public override void ItemEffect()
    {
        MyCharacterController.Instance.speed += MyCharacterController.Instance.speed * (15 + 1 * itemLevel);
        MyCharacterController.Instance.internalDamage += 10;
        MyCharacterController.Instance.externalDamage += 10;
    }
}
