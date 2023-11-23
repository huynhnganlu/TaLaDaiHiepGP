using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    public JsonPlayerPrefs shopPrefs, meridianPrefs, characterPrefs;
    public TimeSpan timePassed;
    private void Awake()
    {
        if (Instance != null && Instance != this)
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
        if (characterPrefs.HasKey("loginTime"))
            timePassed = DateTime.Now - DateTime.Parse(characterPrefs.GetString("loginTime"));

        SetTimeLogin();

        if (!characterPrefs.HasKey("hp"))
        {
            characterPrefs.SetInt("hp", 100);
            characterPrefs.SetInt("hpRegen", 0);
            characterPrefs.SetInt("mp", 100);
            characterPrefs.SetInt("mpRegen", 0);
            characterPrefs.SetFloat("evade", 0);
            characterPrefs.SetInt("externalDamage", 0);
            characterPrefs.SetInt("internalDamage", 0);
            characterPrefs.SetInt("critDamage", 0);
            characterPrefs.SetFloat("critRate", 0);
            characterPrefs.SetInt("defense", 0);
            characterPrefs.SetFloat("movementSpeed", 4f);
            characterPrefs.SetInt("qi", 0);
            characterPrefs.SetInt("dao", 0);
            characterPrefs.SetInt("money", 0);
            characterPrefs.SetInt("map0", 1);
            characterPrefs.SetInt("map1", 0);
            characterPrefs.SetInt("map2", 0);
            characterPrefs.SetString("character", "Sword");
            characterPrefs.SetInt("settingmaster", 100);
            characterPrefs.SetInt("settingbgm", 100);
            characterPrefs.SetInt("settingsfx", 100);
            characterPrefs.SetInt("settingres", 0);
            characterPrefs.SetInt("settingwd", 0);
            characterPrefs.SetInt("settinglg", 0);
            characterPrefs.Save();
        }

        if (!shopPrefs.HasKey("slot0"))
        {
            shopPrefs.SetInt("slot0", -1);
            shopPrefs.SetInt("slot1", -1);
            shopPrefs.SetInt("slot2", -1);
            shopPrefs.Save();
        }

        SettingsMenuController.Instance.LoadSettings();
        AudioManager.Instance.GetSettingAudio(characterPrefs);
        AudioManager.Instance.PlayBG("MenuBGSound");
    }



    private void SetTimeLogin()
    {
        characterPrefs.SetString("loginTime", DateTime.Now.ToString());
        characterPrefs.Save();
    }

    public void SetFirstShop(string status)
    {
        characterPrefs.SetString("firstShop", status);
        characterPrefs.Save();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
