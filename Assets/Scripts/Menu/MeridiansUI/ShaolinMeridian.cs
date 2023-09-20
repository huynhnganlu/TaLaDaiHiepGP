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
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", level.ToString()},
            { "Ngoaïi phoøng:", externalDefense.ToString()},
            { "Hoài khí huyeát:", hpRegen.ToString()},
            { "Boû qua ngoaïi phoøng:", skipExternalDefense.ToString()},
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
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Ngoaïi phoøng:"] = externalDefense.ToString();
        propertyData["Hoài khí huyeát:"] = hpRegen.ToString();
        propertyData["Boû qua ngoaïi phoøng:"] = skipExternalDefense.ToString();
    }
}
