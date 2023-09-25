using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrizeHolder", menuName = "Scriptable Objects/Prize Holder", order = 3)]
public class PrizeHolder : ScriptableObject
{
    public List<PrizeAbstract> prizeList;
}
