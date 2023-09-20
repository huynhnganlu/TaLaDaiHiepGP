using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValleyMeridian : MeridianAbstract
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
            { "Boû qua noäi phoøng:", skipInternalDefense.ToString()},
            { "Noäi coâng baïo kích:", internalCrit.ToString()},

        };

    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        mpRegen += 1;
        skipInternalDefense += 1;
        internalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("valleylevel", level);
        prefs.SetInt("valleyhp", hp);
        prefs.SetInt("valleymp", mp);
        prefs.SetInt("valleympRegen", mpRegen);
        prefs.SetInt("valleyskipInternalDefense", skipInternalDefense);
        prefs.SetInt("valleyinternalCrit", internalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("valleylevel");
        hp = prefs.GetInt("valleyhp");
        mp = prefs.GetInt("valleymp");
        mpRegen = prefs.GetInt("valleympRegen");
        skipInternalDefense = prefs.GetInt("valleyskipInternalDefense");
        internalCrit = prefs.GetInt("valleyinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Hoài noäi löïc:"] = mpRegen.ToString();
        propertyData["Boû qua noäi phoøng:"] = skipInternalDefense.ToString();
        propertyData["Noäi coâng baïo kích:"] = internalCrit.ToString();
    }
}
