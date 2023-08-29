using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MeridianAbstract : MonoBehaviour
{
    public abstract int hp { get; set; }
    public abstract int level { get ; set; }
    public Dictionary<string, string> propertyData;
    public GameObject objectPropertyData;
    public GameObject parentPropertyData;
    public CharacterData characterData;

    public abstract void levelUpMeridian();
   
    public void GetPropertyData()
    {
        foreach(KeyValuePair<string,string> keyValuePair in propertyData)
        {
            GameObject data = Instantiate(objectPropertyData);
            data.GetComponent<PropertyDataController>().SetData(keyValuePair.Key, keyValuePair.Value);
            data.transform.SetParent(parentPropertyData.transform);
        }
    }

    public void ResetPropertyData()
    {
        foreach(Transform child in parentPropertyData.transform)
        {
            Destroy(child.gameObject);
        }
    }
   

}
