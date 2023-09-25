using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PrizeAbstract : MonoBehaviour
{
    public int id;
    public string header, description;
    public Sprite icon;
    public float rate;

    public abstract void ProcessPrize();
}
