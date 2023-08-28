using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MeridianAbstract : MonoBehaviour
{
    public abstract int hp { get; set; }
    public abstract int level { get ; set; }
    public void levelUpMeridian()
    {
        level++;
        MeridianController.Instance.SetMeridianLevel(level);
    }

}
