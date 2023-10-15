using System.Collections.Generic;
using UnityEngine;

public abstract class MeridianAbstract : MonoBehaviour
{
    [HideInInspector]
    public int hp, hpRegen, mp, mpRegen, level, defense, internalDamage, externalDamage, critDamage;
    [HideInInspector]
    public float movementSpeed, evade, critRate;
    public Dictionary<string, string> propertyData;
    public GameObject objectPropertyData;
    public Sprite merdianImage;
    public JsonPlayerPrefs meridianPrefs, characterPrefs;
    public abstract void LevelUpMeridian();
    public abstract void SaveMeridian();
    public abstract void LoadMeridian();
    public abstract void UpdatePropertyData();
    public abstract void SaveCharacterData();

    private void Awake()
    {
        meridianPrefs = MenuController.Instance.meridianPrefs;
        characterPrefs = MenuController.Instance.characterPrefs;
        LoadMeridian();
    }

    //Chay danh sach property va set data
    public void GetPropertyData()
    {
        ResetPropertyData();
        MeridianController.Instance.qiHolder.text = characterPrefs.GetInt("qi").ToString() + "/" + (5 * level);
        MeridianController.Instance.SetMeridianLevel(level);
        foreach (KeyValuePair<string, string> keyValuePair in propertyData)
        {
            GameObject data = Instantiate(objectPropertyData, MeridianController.Instance.parentPropertyData.transform);
            data.GetComponent<PropertyDataController>().SetData(keyValuePair.Key, keyValuePair.Value);
        }
    }
    //Reset property data moi lan click vao cac button kinh mach khac nhau
    public void ResetPropertyData()
    {
        foreach (Transform child in MeridianController.Instance.parentPropertyData.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
