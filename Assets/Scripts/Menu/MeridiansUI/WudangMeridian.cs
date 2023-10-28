using System.Collections.Generic;
using UnityEngine.Localization.Settings;

public class WudangMeridian : MeridianAbstract
{

    private void OnEnable()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData = new Dictionary<string, string>
            {
                { "Health:", hp.ToString()},
                { "Mana:", mp.ToString()},
                { "MP regen:", mpRegen.ToString()},
                { "HP regen:", hpRegen.ToString()},
                { "Internal damage:", internalDamage.ToString()},
            };
        }
        else
        {
            propertyData = new Dictionary<string, string>
            {
                { "Khí huyeát:", hp.ToString()},
                { "Noäi löïc:", mp.ToString()},
                { "Hoài noäi löïc:", mpRegen.ToString()},
                { "Hoài khí huyeát:", hpRegen.ToString()},
                { "Uy löïc noäi coâng:", internalDamage.ToString()},
            };
        }
       

    }
    public override void LevelUpMeridian()
    {
        if (level < 36 && characterPrefs.GetInt("qi") - (5 * level) >= 0)
        {
            level++;
            hp += 2;
            mp += 2;
            mpRegen += 1;
            hpRegen += 1;
            internalDamage += 1;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
            AudioManager.Instance.PlaySE("LevelUpSE");

        }
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("wudanglevel", level);
        meridianPrefs.SetInt("wudanghp", hp);
        meridianPrefs.SetInt("wudangmp", mp);
        meridianPrefs.SetInt("wudangmpRegen", mpRegen);
        meridianPrefs.SetInt("wudanghpRegen", hpRegen);
        meridianPrefs.SetInt("wudanginternalDamage", internalDamage);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("wudanglevel"))
            level = meridianPrefs.GetInt("wudanglevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("wudanghp");
        mp = meridianPrefs.GetInt("wudangmp");
        mpRegen = meridianPrefs.GetInt("wudangmpRegen");
        hpRegen = meridianPrefs.GetInt("wudanghpRegen");
        internalDamage = meridianPrefs.GetInt("wudanginternalDamage");
    }

    public override void UpdatePropertyData()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData["Health:"] = hp.ToString();
            propertyData["Mana:"] = mp.ToString();
            propertyData["MP regen:"] = mpRegen.ToString();
            propertyData["HP regen:"] = hpRegen.ToString();
            propertyData["Internal damage:"] = internalDamage.ToString();
        }
        else
        {
            propertyData["Khí huyeát:"] = hp.ToString();
            propertyData["Noäi löïc:"] = mp.ToString();
            propertyData["Hoài noäi löïc:"] = mpRegen.ToString();
            propertyData["Hoài khí huyeát:"] = hpRegen.ToString();
            propertyData["Uy löïc noäi coâng:"] = internalDamage.ToString();
        }
       
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 2);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetInt("mpRegen", characterPrefs.GetInt("mpRegen") + 1);
        characterPrefs.SetInt("hpRegen", characterPrefs.GetInt("hpRegen") + 1);
        characterPrefs.SetInt("internalDamage", characterPrefs.GetInt("internalDamage") + 1);
        characterPrefs.Save();
    }
}
