using System.Collections;
using UnityEngine;

public class TanDuongCongQuyet : ShopDataAbstract
{
    private bool isAvailable = true;
    float timeEffect;
    public override void ItemEffect()
    {
        if (isAvailable)
        {
            timeEffect = (2f + 0.2f * itemLevel);
            StartCoroutine(TakeEffect());
        }
    }

    private IEnumerator Immune(float timeEffect)
    {
        MyCharacterController.Instance.isImmune = true;
        yield return new WaitForSeconds(timeEffect);
        MyCharacterController.Instance.isImmune = false;
    }

    private IEnumerator TakeEffect()
    {
        isAvailable = false;
        StartCoroutine(Immune(timeEffect));
        yield return new WaitForSeconds(20f);
        isAvailable = true;
    }
}
