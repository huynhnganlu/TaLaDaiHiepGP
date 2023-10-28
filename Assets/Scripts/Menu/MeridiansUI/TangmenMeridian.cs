using System.Collections.Generic;
using UnityEngine.Localization.Settings;

public class TangmenMeridian : MeridianAbstract
{

    private void OnEnable()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData = new Dictionary<string, string>
            {
                { "Health:", hp.ToString()},
                { "Mana:", mp.ToString()},
                { "Evade:", evade.ToString()},
                { "External damage:", externalDamage.ToString()},
                { "Crit rate:", critRate.ToString()},
            };
        }
        else
        {
            propertyData = new Dictionary<string, string>
            {
                { "Kh� huye�t:", hp.ToString()},
                { "No�i l��c:", mp.ToString()},
                { "Ne� tra�nh:", evade.ToString()},
                { "Uy l��c ngoa�i co�ng:", externalDamage.ToString()},
                { "T� le� ba�o k�ch:", critRate.ToString()},
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
            evade += 0.5f;
            externalDamage += 1;
            critRate += 0.5f;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
            AudioManager.Instance.PlaySE("LevelUpSE");

        }
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("tangmenlevel", level);
        meridianPrefs.SetInt("tangmenhp", hp);
        meridianPrefs.SetInt("tangmenmp", mp);
        meridianPrefs.SetFloat("tangmenevade", evade);
        meridianPrefs.SetInt("tangmenexternalDamage", externalDamage);
        meridianPrefs.SetFloat("tangmencritRate", critRate);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("tangmenlevel"))
            level = meridianPrefs.GetInt("tangmenlevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("tangmenhp");
        mp = meridianPrefs.GetInt("tangmenmp");
        evade = meridianPrefs.GetFloat("tangmenevade");
        externalDamage = meridianPrefs.GetInt("tangmenexternalDamage");
        critRate = meridianPrefs.GetFloat("tangmencritRate");
    }

    public override void UpdatePropertyData()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData["Health:"] = hp.ToString();
            propertyData["Mana:"] = mp.ToString();
            propertyData["Evade:"] = evade.ToString();
            propertyData["External damage:"] = externalDamage.ToString();
            propertyData["Crit rate:"] = critRate.ToString();
        }
        else
        {
            propertyData["Kh� huye�t:"] = hp.ToString();
            propertyData["No�i l��c:"] = mp.ToString();
            propertyData["Ne� tra�nh:"] = evade.ToString();
            propertyData["Uy l��c ngoa�i co�ng:"] = externalDamage.ToString();
            propertyData["T� le� ba�o k�ch:"] = critRate.ToString();
        }
       
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 2);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetFloat("evade", characterPrefs.GetFloat("evade") + 0.5f);
        characterPrefs.SetInt("externalDamage", characterPrefs.GetInt("externalDamage") + 1);
        characterPrefs.SetFloat("critRate", characterPrefs.GetFloat("critRate") + 0.5f);
        characterPrefs.Save();
    }
}
