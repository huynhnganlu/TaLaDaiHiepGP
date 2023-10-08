using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class MeridianAbstract : MonoBehaviour
{
    [HideInInspector]
    public int hp, hpRegen, mp, mpRegen, level, evade, defense, internalDamage, externalDamage, critRate, critDamage, movementSpeed; 
    public Dictionary<string, string> propertyData;
    public GameObject objectPropertyData;
    public CharacterData characterData;
    public Sprite merdianImage;
    public JsonPlayerPrefs meridianPrefs;
    public JsonPlayerPrefs characterPrefs;
    public abstract void LevelUpMeridian();
    public abstract void SaveMeridian();
    public abstract void LoadMeridian();
    public abstract void UpdatePropertyData();
    public abstract void SaveCharacterData();

    //Chay danh sach property va set data
    public void GetPropertyData()
    {
        ResetPropertyData();
        MeridianController.Instance.qiHolder.text = characterData.qi.ToString();
        MeridianController.Instance.SetMeridianLevel(level);
        foreach (KeyValuePair<string,string> keyValuePair in propertyData)
        {
            GameObject data = Instantiate(objectPropertyData, MeridianController.Instance.parentPropertyData.transform);
            data.GetComponent<PropertyDataController>().SetData(keyValuePair.Key, keyValuePair.Value);
        }
    }
    //Reset property data moi lan click vao cac button kinh mach khac nhau
    public void ResetPropertyData()
    {
        foreach(Transform child in MeridianController.Instance.parentPropertyData.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
}
