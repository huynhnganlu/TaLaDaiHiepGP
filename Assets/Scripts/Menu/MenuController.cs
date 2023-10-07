using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    public JsonPlayerPrefs shopPrefs;
    public JsonPlayerPrefs meridianPrefs;
    public JsonPlayerPrefs characterPrefs;
    public Dictionary<string, string> listCharacterProperty;
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
            characterPrefs.SetInt("hpRegen", 0);
            characterPrefs.SetInt("mp", 0);
            characterPrefs.SetInt("mpRegen", 0);
            characterPrefs.SetInt("evade", 0);
            characterPrefs.SetInt("externalDamage", 0);
            characterPrefs.SetInt("internalDamage", 0);
            characterPrefs.SetInt("critDamage", 0);
            characterPrefs.SetInt("critRate", 0);
            characterPrefs.SetInt("defense", 0);
            characterPrefs.SetInt("movementSpeed", 0);
            characterPrefs.Save();
        }
        
        listCharacterProperty ??= new Dictionary<string, string>()
            {
                { "hp","Kh� huye�t"},
                { "hpRegen", "Ho�i kh� huye�t" },
                { "mp", "No�i l��c" },
                { "mpRegen", "Ho�i no�i l��c" },
                { "evade", "Ne� tra�nh" }, 
                { "externalDamage", "Uy l��c ngoa�i co�ng" },
                { "internalDamage", "Uy l��c no�i co�ng" },
                { "critDamage", "Sa�t th��ng ch� ma�ng" },
                { "critRate", "T� le� ch� ma�ng" },
                { "defense", "Pho�ng thu�" },
                { "movementSpeed", "To�c �o� di chuye�n" }
            };
    }
}
