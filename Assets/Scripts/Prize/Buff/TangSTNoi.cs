public class TangSTNoi : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.internalDamage += 10;
    }


}
