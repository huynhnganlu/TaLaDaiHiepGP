using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EmeiMeridian : MeridianAbstract
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
            { "Noäi phoøng:", internalDefense.ToString()},
            { "Boû qua noäi phoøng:", skipInternalDefense.ToString()}

        };
        GetPropertyData();
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        evade += 1;
        internalDefense += 1;
        skipInternalDefense += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("emeilevel", level);
        prefs.SetInt("emeihp", hp);
        prefs.SetInt("emeimp", mp);
        prefs.SetInt("emeievade", evade);
        prefs.SetInt("emeiinternalDefense", internalDefense);
        prefs.SetInt("emeiskipInternalDefense", skipInternalDefense);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("emeilevel");
        hp = prefs.GetInt("emeihp");
        mp = prefs.GetInt("emeimp");
        evade = prefs.GetInt("emeievade");
        internalDefense = prefs.GetInt("emeiinternalDefense");
        skipInternalDefense = prefs.GetInt("emeiskipInternalDefense");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Neù traùnh:"] = evade.ToString();
        propertyData["Noäi phoøng:"] = internalDefense.ToString();
        propertyData["Boû qua noäi phoøng:"] = skipInternalDefense.ToString();
    }
}
