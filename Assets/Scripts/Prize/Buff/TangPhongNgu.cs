public class TangPhongNgu : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.defense += 2;
    }


}
