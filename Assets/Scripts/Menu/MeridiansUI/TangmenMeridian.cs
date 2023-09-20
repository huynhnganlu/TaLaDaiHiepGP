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
            { "Kh� huye�t:", hp.ToString()},
            { "No�i l��c:", mp.ToString()},
            { "Ne� tra�nh:", evade.ToString()},
            { "Uy l��c ngoa�i co�ng:", externalDamage.ToString()},
            { "Ngoa�i co�ng ba�o k�ch:", externalCrit.ToString()},
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
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ne� tra�nh:"] = evade.ToString();
        propertyData["Uy l��c ngoa�i co�ng:"] = externalDamage.ToString();
        propertyData["Ngoa�i co�ng ba�o k�ch:"] = externalCrit.ToString();
    }
}
