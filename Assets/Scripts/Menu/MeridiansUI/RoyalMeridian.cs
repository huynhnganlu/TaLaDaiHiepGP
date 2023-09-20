using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalMeridian : MeridianAbstract
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
            { "Uy löïc ngoaïi coâng:", externalDamage.ToString()},
            { "Boû qua ngoaïi phoøng:", skipExternalDefense.ToString()},
            { "Ngoaïi coâng baïo kích:", externalCrit.ToString()},
        };
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        externalDamage += 1;
        skipExternalDefense += 1;
        externalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("royallevel", level);
        prefs.SetInt("royalhp", hp);
        prefs.SetInt("royalmp", mp);
        prefs.SetInt("royalexternalDamage", externalDamage);
        prefs.SetInt("royalskipExternalDefense", skipExternalDefense);
        prefs.SetInt("royalexternalCrit", externalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("royallevel");
        hp = prefs.GetInt("royalhp");
        mp = prefs.GetInt("royalmp");
        externalDamage = prefs.GetInt("royalexternalDamage");
        skipExternalDefense = prefs.GetInt("royalskipExternalDefense");
        externalCrit = prefs.GetInt("royalexternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Uy löïc ngoaïi coâng:"] = externalDamage.ToString();
        propertyData["Boû qua ngoaïi phoøng:"] = skipExternalDefense.ToString();
        propertyData["Ngoaïi coâng baïo kích:"] = externalCrit.ToString();
    }
}
