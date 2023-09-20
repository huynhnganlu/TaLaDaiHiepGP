using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeggarMeridian : MeridianAbstract
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
            { "Noäi phoøng:", internalDefense.ToString()},
            { "Uy löïc noäi coâng:", internalDamage.ToString()},
            { "Noäi coâng baïo kích:", internalCrit.ToString()},

        };
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        internalDamage += 1;
        internalDefense += 1;
        internalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("beggarlevel", level);
        prefs.SetInt("beggarhp", hp);
        prefs.SetInt("beggarmp", mp);
        prefs.SetInt("beggarinternalDamage", internalDamage);
        prefs.SetInt("beggarinternalDefense", internalDefense);
        prefs.SetInt("beggarinternalCrit", internalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("beggarlevel");
        hp = prefs.GetInt("beggarhp");
        mp = prefs.GetInt("beggarmp");
        internalDamage = prefs.GetInt("beggarinternalDamage");
        internalDefense = prefs.GetInt("beggarinternalDefense");
        internalCrit = prefs.GetInt("beggarinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Noäi phoøng:"] = internalDefense.ToString();
        propertyData["Uy löïc noäi coâng:"] = internalDamage.ToString();
        propertyData["Noäi coâng baïo kích:"] = internalCrit.ToString();
    }
}
