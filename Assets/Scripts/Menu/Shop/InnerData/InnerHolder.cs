using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InnerHolder", menuName = "Scriptable Objects/Inner Holder", order = 2)]
public class InnerHolder : ScriptableObject
{
    public List<GameObject> listInner;
}
