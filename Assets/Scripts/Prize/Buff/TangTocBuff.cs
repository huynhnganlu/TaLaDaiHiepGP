public class TangTocBuff : PrizeAbstract
{

    public override void ProcessPrize()
    {
        MyCharacterController.Instance.speed += 0.02f;
    }


}
