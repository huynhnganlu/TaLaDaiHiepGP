using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TangmenMeridian : MeridianAbstract
{
    private JsonPlayerPrefs prefs;

    private void Start()
    {
        prefs = MeridianController.Instance.prefs;
        LoadMeridian();
        propertyData = new Dictionary<string, string>
        {
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", mp.ToString()},
            { "Neù traùnh:", evade.ToString()},
            { "Uy löïc ngoaïi coâng:", externalDamage.ToString()},
            { "Ngoaïi coâng baïo kích:", externalCrit.ToString()},
        };
    }
    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        evade += 1;
        externalDamage += 1;
        externalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("tangmenlevel", level);
        prefs.SetInt("tangmenhp", hp);
        prefs.SetInt("tangmenmp", mp);
        prefs.SetInt("tangmenevade", evade);
        prefs.SetInt("tangmenexternalDamage", externalDamage);
        prefs.SetInt("tangmenexternalCrit", externalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("tangmenlevel");
        hp = prefs.GetInt("tangmenhp");
        mp = prefs.GetInt("tangmenmp");
        evade = prefs.GetInt("tangmenevade");
        externalDamage = prefs.GetInt("tangmenexternalDamage");
        externalCrit = prefs.GetInt("tangmenexternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Neù traùnh:"] = evade.ToString();
        propertyData["Uy löïc ngoaïi coâng:"] = externalDamage.ToString();
        propertyData["Ngoaïi coâng baïo kích:"] = externalCrit.ToString();
    }
}
