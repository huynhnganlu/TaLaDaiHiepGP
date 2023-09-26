using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    public JsonPlayerPrefs shopPrefs;
    public JsonPlayerPrefs meridianPrefs;
    public JsonPlayerPrefs characterPrefs;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        meridianPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/MeridianData.json");
        characterPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/CharacterData.json");
        shopPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/ShopData.json");
    }

    private void Start()
    {
        if (!characterPrefs.HasKey("hp"))
        {
            characterPrefs.SetInt("hp", 0);
            characterPrefs.SetInt("mp", 0);
            characterPrefs.SetInt("evade", 0);
            characterPrefs.SetInt("externalDamage", 0);
            characterPrefs.SetInt("externalCrit", 0);
            characterPrefs.SetInt("mpRegen", 0);
            characterPrefs.SetInt("internalCrit", 0);
            characterPrefs.SetInt("internalDamage", 0);
            characterPrefs.SetInt("skipInternalDefense", 0);
            characterPrefs.SetInt("skipExternalDefense", 0);
            characterPrefs.SetInt("internalDefense", 0);
            characterPrefs.SetInt("externalDefense", 0);
        }
    }
}
