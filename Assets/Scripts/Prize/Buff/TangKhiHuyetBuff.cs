public class TangKhiHuyetBuff : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.maxHealth += 10;
        MyCharacterController.Instance.healthBar.maxValue += 10;
    }


}
