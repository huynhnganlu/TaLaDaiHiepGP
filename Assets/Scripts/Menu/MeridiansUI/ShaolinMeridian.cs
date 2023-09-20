using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaolinMeridian : MeridianAbstract
{
    private JsonPlayerPrefs prefs;

    private void Start()
    {
        prefs = MeridianController.Instance.prefs;
        LoadMeridian();
        propertyData = new Dictionary<string, string>
        {
            { "Kh� huye�t:", hp.ToString()},
            { "No�i l��c:", level.ToString()},
            { "Ngoa�i pho�ng:", externalDefense.ToString()},
            { "Ho�i kh� huye�t:", hpRegen.ToString()},
            { "Bo� qua ngoa�i pho�ng:", skipExternalDefense.ToString()},
        };
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        externalDefense += 1;
        hpRegen += 1;
        skipExternalDefense += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("shaolinlevel", level);
        prefs.SetInt("shaolinhp", hp);
        prefs.SetInt("shaolinmp", mp);
        prefs.SetInt("shaolinexternalDefense", externalDefense);
        prefs.SetInt("shaolinhpRegen", hpRegen);
        prefs.SetInt("shaolinskipExternalDefense", skipExternalDefense);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("shaolinlevel");
        hp = prefs.GetInt("shaolinhp");
        mp = prefs.GetInt("shaolinmp");
        externalDefense = prefs.GetInt("shaolinexternalDefense");
        hpRegen = prefs.GetInt("shaolinhpRegen");
        skipExternalDefense = prefs.GetInt("shaolinskipExternalDefense");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ngoa�i pho�ng:"] = externalDefense.ToString();
        propertyData["Ho�i kh� huye�t:"] = hpRegen.ToString();
        propertyData["Bo� qua ngoa�i pho�ng:"] = skipExternalDefense.ToString();
    }
}
