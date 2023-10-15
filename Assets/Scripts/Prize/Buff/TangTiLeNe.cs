public class TangTiLeNe : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.evade += 1;
    }
}
