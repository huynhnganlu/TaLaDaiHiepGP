using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessPrize : MonoBehaviour
{
    public void OnPlayerSelectPrize()
    {

        PrizeController.Instance.bg.SetActive(false);
        PrizeController.Instance.isFreezing = false;
        Time.timeScale = 1;
    }
}
