using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmeiMeridian : MeridianAbstract
{
    public Dictionary<string, string> propertyData;
    public override int hp { get; set; }
    public override int level { get; set; }
    public GameObject propertyDataObject;
    public GameObject dataObjectParent;
  

    private void Start()
    {
        propertyData = new Dictionary<string, string>();
        hp = 0;
        level = 0;
        propertyData.Add("Khi huyet", hp.ToString());
        getPropertyData();
    }

    void getPropertyData()
    {
        foreach(KeyValuePair<string, string> kvp in propertyData)
        {
            GameObject gameObject = Instantiate(propertyDataObject);
            gameObject.GetComponent<PropertyDataController>().SetData(kvp.Key, kvp.Value);
            gameObject.transform.SetParent(dataObjectParent.transform);
        }
    }

}
