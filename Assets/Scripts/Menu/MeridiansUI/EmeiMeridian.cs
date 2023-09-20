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
            { "Kh� huye�t:", hp.ToString()},
            { "No�i l��c:", mp.ToString()},
            { "Ne� tra�nh:", evade.ToString()},
            { "No�i pho�ng:", internalDefense.ToString()},
            { "Bo� qua no�i pho�ng:", skipInternalDefense.ToString()}

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
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ne� tra�nh:"] = evade.ToString();
        propertyData["No�i pho�ng:"] = internalDefense.ToString();
        propertyData["Bo� qua no�i pho�ng:"] = skipInternalDefense.ToString();
    }
}
