using System.Collections.Generic;
using UnityEngine.Localization.Settings;

public class ScholarMeridian : MeridianAbstract
{


    private void OnEnable()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData = new Dictionary<string, string>
            {
                { "Health:", hp.ToString()},
                { "Mana:", mp.ToString()},
                { "Internal damage:", internalDamage.ToString()},
                { "Movement speed:", movementSpeed.ToString()},
                { "Crit rate:", critRate.ToString()},
            };
        }
        else
        {
            propertyData = new Dictionary<string, string>
            {
                { "Khí huyeát:", hp.ToString()},
                { "Noäi löïc:", mp.ToString()},
                { "Uy löïc noäi coâng:", internalDamage.ToString()},
                { "Toác ñoä di chuyeån:", movementSpeed.ToString()},
                { "Tæ leä baïo kích:", critRate.ToString()},
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
            internalDamage += 1;
            movementSpeed += 0.02f;
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
        meridianPrefs.SetInt("scholarlevel", level);
        meridianPrefs.SetInt("scholarlevel", level);
        meridianPrefs.SetInt("scholarhp", hp);
        meridianPrefs.SetInt("scholarmp", mp);
        meridianPrefs.SetInt("scholarinternalDamage", internalDamage);
        meridianPrefs.SetFloat("scholarmovementSpeed", System.MathF.Round(movementSpeed, 2));
        meridianPrefs.SetFloat("scholarcritRate", critRate);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("scholarlevel"))
            level = meridianPrefs.GetInt("scholarlevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("scholarhp");
        mp = meridianPrefs.GetInt("scholarmp");
        internalDamage = meridianPrefs.GetInt("scholarinternalDamage");
        movementSpeed = meridianPrefs.GetFloat("scholarmovementSpeed");
        critRate = meridianPrefs.GetFloat("scholarcritRate");
    }

    public override void UpdatePropertyData()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData["Health:"] = hp.ToString();
            propertyData["Mana:"] = mp.ToString();
            propertyData["Internal damage:"] = internalDamage.ToString();
            propertyData["Movement speed:"] = System.MathF.Round(movementSpeed, 2).ToString();
            propertyData["Crit rate:"] = critRate.ToString();
        }
        else
        {
            propertyData["Khí huyeát:"] = hp.ToString();
            propertyData["Noäi löïc:"] = mp.ToString();
            propertyData["Uy löïc noäi coâng:"] = internalDamage.ToString();
            propertyData["Toác ñoä di chuyeån:"] = System.MathF.Round(movementSpeed, 2).ToString();
            propertyData["Tæ leä baïo kích:"] = critRate.ToString();
        }    
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();
    }


    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 2);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetInt("internalDamage", characterPrefs.GetInt("internalDamage") + 1);
        characterPrefs.SetFloat("movementSpeed", System.MathF.Round(characterPrefs.GetFloat("movementSpeed") + 0.02f, 2));
        characterPrefs.SetFloat("critRate", characterPrefs.GetFloat("critRate") + 0.5f);
        characterPrefs.Save();
    }
}
