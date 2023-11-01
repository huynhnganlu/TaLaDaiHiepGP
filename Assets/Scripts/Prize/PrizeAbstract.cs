using UnityEngine;

public abstract class PrizeAbstract : MonoBehaviour
{
    public int id, cost;
    public string header, headerEng;
    [TextArea]
    public string description, descriptionEng;
    public Sprite icon;
    public float rate;
    public SkillAbstract skillRef;

    public abstract void ProcessPrize();
}
