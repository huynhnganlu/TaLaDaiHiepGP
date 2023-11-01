using System.Collections.Generic;
using UnityEngine.Localization.Settings;

public class EmeiMeridian : MeridianAbstract
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
                { "Defense:", defense.ToString()},
                { "Crit damage:", critDamage.ToString()}

            };
        }
        else
        {
            propertyData = new Dictionary<string, string>
            {
                { "Khí huyeát:", hp.ToString()},
                { "Noäi löïc:", mp.ToString()},
                { "Neù traùnh:", evade.ToString()},
                { "Phoøng ngöï:", defense.ToString()},
                { "Saùt thöông baïo kích:", critDamage.ToString()}

            };
        }    
    }

    public override void LevelUpMeridian()
    {
        if (level < 36 && characterPrefs.GetInt("qi") - (level * 5) >= 0)
        {
            level++;
            hp += 2;
            mp += 2;
            evade += 0.5f;
            defense += 1;
            critDamage += 1;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
            AudioManager.Instance.PlaySE("LevelUpSE");

        }
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("emeilevel", level);
        meridianPrefs.SetInt("emeihp", hp);
        meridianPrefs.SetInt("emeimp", mp);
        meridianPrefs.SetFloat("emeievade", evade);
        meridianPrefs.SetInt("emeidefense", defense);
        meridianPrefs.SetInt("emeicritDamage", critDamage);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("emeilevel"))
            level = meridianPrefs.GetInt("emeilevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("emeihp");
        mp = meridianPrefs.GetInt("emeimp");
        evade = meridianPrefs.GetFloat("emeievade");
        defense = meridianPrefs.GetInt("emeidefense");
        critDamage = meridianPrefs.GetInt("emeicritDamage");
    }

    public override void UpdatePropertyData()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData["Health:"] = hp.ToString();
            propertyData["Mana:"] = mp.ToString();
            propertyData["Evade:"] = evade.ToString();
            propertyData["Defense:"] = defense.ToString();
            propertyData["Crit damage:"] = critDamage.ToString();
        }
        else
        {
            propertyData["Khí huyeát:"] = hp.ToString();
            propertyData["Noäi löïc:"] = mp.ToString();
            propertyData["Neù traùnh:"] = evade.ToString();
            propertyData["Phoøng ngöï:"] = defense.ToString();
            propertyData["Saùt thöông baïo kích:"] = critDamage.ToString();
        }         
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (level * 5));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 2);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetFloat("evade", characterPrefs.GetFloat("evade") + 0.5f);
        characterPrefs.SetInt("defense", characterPrefs.GetInt("defense") + 1);
        characterPrefs.SetInt("critDamage", characterPrefs.GetInt("critDamage") + 1);
        characterPrefs.Save();
    }
}
