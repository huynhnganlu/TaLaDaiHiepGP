public class HoiKhiHuyet : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.hpRegen += 5;
    }

}
