using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarMeridian : MeridianAbstract
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
            { "Uy löïc noäi coâng:", internalDamage.ToString()},
            { "Boû qua noäi phoøng:", skipInternalDefense.ToString()},
            { "Noäi coâng baïo kích:", internalCrit.ToString()},
        };   
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        internalDamage += 1;
        skipInternalDefense += 1;
        internalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("scholarlevel", level);
        prefs.SetInt("scholarhp", hp);
        prefs.SetInt("scholarmp", mp);
        prefs.SetInt("scholarinternalDamage", internalDamage);
        prefs.SetInt("scholarskipInternalDefense", skipInternalDefense);
        prefs.SetInt("scholarinternalCrit", internalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("scholarlevel");
        hp = prefs.GetInt("scholarhp");
        mp = prefs.GetInt("scholarmp");
        internalDamage = prefs.GetInt("scholarinternalDamage");
        skipInternalDefense = prefs.GetInt("scholarskipInternalDefense");
        internalCrit = prefs.GetInt("scholarinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Uy löïc noäi coâng:"] = internalDamage.ToString();
        propertyData["Boû qua noäi phoøng:"] = skipInternalDefense.ToString();
        propertyData["Noäi coâng baïo kích:"] = internalCrit.ToString();
    }
}
