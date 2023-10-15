public class HoiNoiLuc : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.mpRegen += 1;
    }

}
