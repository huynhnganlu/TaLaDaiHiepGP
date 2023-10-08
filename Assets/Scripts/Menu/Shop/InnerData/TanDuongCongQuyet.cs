using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class TanDuongCongQuyet : ShopDataAbstract
{
    private float lastCall, cooldown = 20f;
    private bool first = false;
    public override void ItemEffect()
    {
        float timeEffect = (2f + 0.2f * itemLevel);
        if(first == false)
        {
            StartCoroutine(Immune(timeEffect));
            first = true;
            lastCall = Time.time;
        }
        if (Time.time - lastCall > cooldown)
        {
            lastCall = Time.time;
            StartCoroutine(Immune(timeEffect));
        }
    }

    private IEnumerator Immune(float timeEffect)
    {
        MyCharacterController.Instance.isImmune = true;
        yield return new WaitForSeconds(timeEffect);
        MyCharacterController.Instance.isImmune = false;

    }
}
