public class TangCritRate : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.critRate += 10;
    }

}
