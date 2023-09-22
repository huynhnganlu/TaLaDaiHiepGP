using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/New Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    public int money, qi, hp, shield;
}
