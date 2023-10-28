using System.Collections.Generic;
using UnityEngine.Localization.Settings;

public class RoyalMeridian : MeridianAbstract
{


    private void OnEnable()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData = new Dictionary<string, string>
            {
                { "Health:", hp.ToString()},
                { "Mana:", mp.ToString()},
                { "External damage:", externalDamage.ToString()},
                { "Crit damage:", critDamage.ToString()},
                { "Crit rate:", critRate.ToString()},

            };
        }
        else
        {
            propertyData = new Dictionary<string, string>
            {
                { "Kh� huye�t:", hp.ToString()},
                { "No�i l��c:", mp.ToString()},
                { "Uy l��c ngoa�i co�ng:", externalDamage.ToString()},
                { "Sa�t th��ng ba�o k�ch:", critDamage.ToString()},
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
            externalDamage += 1;
            critDamage += 1;
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
        meridianPrefs.SetInt("royallevel", level);
        meridianPrefs.SetInt("royalhp", hp);
        meridianPrefs.SetInt("royalmp", mp);
        meridianPrefs.SetInt("royalexternalDamage", externalDamage);
        meridianPrefs.SetInt("royalcritDamage", critDamage);
        meridianPrefs.SetFloat("royalcritRate", critRate);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("royallevel"))
            level = meridianPrefs.GetInt("royallevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("royalhp");
        mp = meridianPrefs.GetInt("royalmp");
        externalDamage = meridianPrefs.GetInt("royalexternalDamage");
        critDamage = meridianPrefs.GetInt("royalcritDamage");
        critRate = meridianPrefs.GetFloat("royalcritRate");
    }

    public override void UpdatePropertyData()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData["Health:"] = hp.ToString();
            propertyData["Mana:"] = mp.ToString();
            propertyData["External damage:"] = externalDamage.ToString();
            propertyData["Crit damage:"] = critDamage.ToString();
            propertyData["Crit rate:"] = critRate.ToString();
        }
        else
        {
            propertyData["Kh� huye�t:"] = hp.ToString();
            propertyData["No�i l��c:"] = mp.ToString();
            propertyData["Uy l��c ngoa�i co�ng:"] = externalDamage.ToString();
            propertyData["Sa�t th��ng ba�o k�ch:"] = critDamage.ToString();
            propertyData["T� le� ba�o k�ch:"] = critRate.ToString();
        }
     
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 2);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetInt("externalDamage", characterPrefs.GetInt("externalDamage") + 1);
        characterPrefs.SetInt("critDamage", characterPrefs.GetInt("critDamage") + 1);
        characterPrefs.SetFloat("critRate", characterPrefs.GetFloat("critRate") + 0.5f);
        characterPrefs.Save();
    }
}
