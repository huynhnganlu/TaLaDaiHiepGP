using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PrizeAbstract : MonoBehaviour
{
    public int id, cost;
    public string header;
    [TextArea]
    public string description;
    public Sprite icon;
    public float rate;
    public SkillAbstract skillRef;

    public abstract void ProcessPrize();
}
