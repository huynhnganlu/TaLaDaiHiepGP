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
                { "hp","Khí huyeát"},
                { "hpRegen", "Hoài khí huyeát" },
                { "mp", "Noäi löïc" },
                { "mpRegen", "Hoài noäi löïc" },
                { "evade", "Neù traùnh" }, 
                { "externalDamage", "Uy löïc ngoaïi coâng" },
                { "internalDamage", "Uy löïc noäi coâng" },
                { "critDamage", "Saùt thöông chí maïng" },
                { "critRate", "Tæ leä chí maïng" },
                { "defense", "Phoøng thuû" },
                { "movementSpeed", "Toác ñoä di chuyeån" }
            };
    }
}
