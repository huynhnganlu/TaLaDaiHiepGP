using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WudangMeridian : MeridianAbstract
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
            { "Hoài noäi löïc:", mpRegen.ToString()},
            { "Noäi coâng baïo kích:", internalCrit.ToString()},
            { "Ngoaïi coâng baïo kích:", externalCrit.ToString()},
        };

    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        mpRegen += 1;
        internalCrit += 1;
        externalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("wudanglevel", level);
        prefs.SetInt("wudanghp", hp);
        prefs.SetInt("wudangmp", mp);
        prefs.SetInt("wudangmpRegen", mpRegen);
        prefs.SetInt("wudanginternalCrit", internalCrit);
        prefs.SetInt("wudangexternalCrit", externalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("wudanglevel");
        hp = prefs.GetInt("wudanghp");
        mp = prefs.GetInt("wudangmp");
        mpRegen = prefs.GetInt("wudangmpRegen");
        internalCrit = prefs.GetInt("wudanginternalCrit");
        externalCrit = prefs.GetInt("wudangexternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Hoài noäi löïc:"] = mpRegen.ToString();
        propertyData["Noäi coâng baïo kích:"] = internalCrit.ToString();
        propertyData["Ngoaïi coâng baïo kích:"] = externalCrit.ToString();
    }
}
