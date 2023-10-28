public class TangCritDamage : PrizeAbstract
{
    public override void ProcessPrize()
    {
        MyCharacterController.Instance.critDamage += 5;
    }

}
