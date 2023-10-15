public class DiaNgucHoanHonKinh : ShopDataAbstract
{
    public override void ItemEffect()
    {
        MyCharacterController.Instance.critRate += (10 + 1 * itemLevel);
        MyCharacterController.Instance.internalDamage += (30 + 2 * itemLevel);
        MyCharacterController.Instance.externalDamage += (30 + 2 * itemLevel);
    }


}
