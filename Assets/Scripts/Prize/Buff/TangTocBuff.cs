public class TangTocBuff : PrizeAbstract
{

    public override void ProcessPrize()
    {
        MyCharacterController.Instance.speed += 1f;
    }


}
